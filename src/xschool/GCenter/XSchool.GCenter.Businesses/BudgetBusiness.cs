using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
using System.Linq;


namespace XSchool.GCenter.Businesses
{
    public class BudgetBusiness : Business<Budget>
    {
        private readonly BudgetRepository _repository;
        public BudgetBusiness(IServiceProvider provider, BudgetRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        public virtual Result<Budget> GetSingle(string Name)
        {
            return Result.Success(_repository.GetSingle(p => p.Name == Name));
        }

        public IList<Budget> Get()
        {
            return base.Query(p => p.Id > 0);
        }

        public IList<Budget> Get(string type)
        {
            return base.Query(p => p.Type.Equals(Enum.Parse(typeof(BudgetType), type)));
        }

        public override Result Add(Budget model)
        {
            var res = ChkData(model);
            if (!res.Succeed) { return res; }
            if (model.Id != 0)
            {
                return Result.Fail("添加操作主键编号必须为零");
            }
            //model.IsAllowDel = true;
            return base.Add(model);
        }

        public override Result Update(Budget model)
        {
            var res = ChkData(model);
            if (!res.Succeed) { return res; }
            if (model.Id <= 0)
            {
                return Result.Fail("修改操作主键编号必须大于零");
            }
            var oldModel = GetSingle(p => p.Id == model.Id);
            if (oldModel == null)
            {
                return Result.Fail("未找到该条数据，操作失败");
            }
            oldModel.Name = model.Name;
            oldModel.Memo = model.Memo;
            oldModel.SortId = model.SortId;
            oldModel.BgStatus = model.BgStatus;
            oldModel.Type = model.Type;
            oldModel.IsSystem = model.IsSystem;
            return base.Update(oldModel);
        }

        private Result ChkData(Budget model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            if (model.IsSystem.Equals(IsSystem.Yes))
            {
                return Result.Fail("数据为系统数据，无法更改！");
            }
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return Result.Fail("该项不能为空");
            }

            if (GetSingle(p => p.Name == model.Name && p.Id != model.Id && p.Type == model.Type) != null)
            {
                return Result.Fail("数据已存在，不能再次使用");
            }

            if (string.IsNullOrWhiteSpace(model.Memo))
            {
                model.Memo = "";
            }
            if (model.SortId <= 0)
            {
                model.SortId = 10000;
            }
            model.Name = model.Name.Trim();
            model.Memo = model.Memo.Trim();
            return Result.Success();
        }

        private List<Budget> CreateBudGets(List<Budget> lst)
        {

            var dic = new Dictionary<int, bool>();
            foreach (var item in lst)
            {
                if (item.Pid > 0 && !dic.ContainsKey(item.Pid))
                {
                    dic[item.Pid] = false;
                }
                else if (item.Pid > 0 && dic.ContainsKey(item.Pid))
                {
                    dic[item.Pid] = false;
                }

                dic[item.Id] = true;

            }

            foreach (var item in lst)
            {
                item.IsChild = dic[item.Id];
            }
            return lst;
        }
    }
}
