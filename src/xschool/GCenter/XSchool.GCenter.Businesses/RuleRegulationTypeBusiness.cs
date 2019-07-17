using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.Businesses
{
    public class RuleRegulationTypeBusiness : Business<Model.RuleRegulationType>
    {
        private readonly RuleRegulationTypeRepository _repository;
        public RuleRegulationTypeBusiness(IServiceProvider provider, RuleRegulationTypeRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
        public override Result Add(Model.RuleRegulationType model)
        {
            var res = CheckData(model);
            if (!res.Succeed)
            {
                return res;
            }
            if (model.Id != 0)
            {
                return Result.Fail("添加操作主键编号必须为零");
            }
            return base.Add(model);
        }
        private Result CheckData(Model.RuleRegulationType model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            return Result.Success();
        }
    }
    public class RuleRegulationBusiness : Business<Model.RuleRegulation>
    {
        private readonly RuleRegulationRepository _repository;
        private readonly RuleRegulationReadRangeRepository _readRangeRepository;
        public RuleRegulationBusiness(IServiceProvider provider, RuleRegulationRepository repository, RuleRegulationReadRangeRepository readRangeRepository) : base(provider, repository)
        {
            this._repository = repository;
            this._readRangeRepository = readRangeRepository;
        }
        public IPageCollection<Model.RuleRegulationPage> GetRuleRegulationList(int page, int limit, Model.RuleRegulationSearch search)
        {
            return _repository.GetRuleRegulationList(page, limit, search);
        }
        public Result Add(Model.RuleRegulation model, List<Model.User> userList, List<Model.Dep> DepList, List<Model.Com> ComList, List<Model.Position> PositionList)
        {
            using (TransactionScope tr = new TransactionScope())
            {
                Result result = new Result();
                try
                {
                    var res = CheckData(model);
                    base.Add(model);
                    if (userList != null)
                    {
                        List<Model.RuleRegulationReadRange> readModelList = new List<Model.RuleRegulationReadRange>();
                        for (var i = 0; i < userList.Count; i++)
                        {
                            var readModel = new Model.RuleRegulationReadRange();
                            readModel.RuleTypeId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.User;
                            readModel.UserId = userList[i].id;
                            readModel.UserName = userList[i].name;
                            readModel.DptId = userList[i].dpt_id;
                            readModel.DptName = userList[i].dpt_name;
                            readModel.CompanyId = userList[i].company_id;
                            readModel.CompanyName = userList[i].company_name;
                            readModelList.Add(readModel);
                        }
                        _readRangeRepository.AddRange(readModelList);
                    }
                    if (DepList != null)
                    {
                        List<Model.RuleRegulationReadRange> readModelList = new List<Model.RuleRegulationReadRange>();
                        for (var i = 0; i < DepList.Count; i++)
                        {
                            var readModel = new Model.RuleRegulationReadRange();
                            readModel.RuleTypeId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.Dep;
                            readModel.DptId = DepList[i].id;
                            readModel.DptName = DepList[i].name;
                            readModel.CompanyId = DepList[i].company_id;
                            readModel.CompanyName = DepList[i].company_name;
                            readModelList.Add(readModel);
                        }
                        _readRangeRepository.AddRange(readModelList);
                    }
                    if (ComList != null)
                    {
                        List<Model.RuleRegulationReadRange> readModelList = new List<Model.RuleRegulationReadRange>();
                        for (var i = 0; i < ComList.Count; i++)
                        {
                            var readModel = new Model.RuleRegulationReadRange();
                            readModel.RuleTypeId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.Com;
                            readModel.CompanyId = ComList[i].id;
                            readModel.CompanyName = ComList[i].name;
                            readModelList.Add(readModel);
                        }
                        _readRangeRepository.AddRange(readModelList);
                    }
                    if (PositionList != null)
                    {
                        List<Model.RuleRegulationReadRange> readModelList = new List<Model.RuleRegulationReadRange>();
                        for (var i = 0; i < PositionList.Count; i++)
                        {
                            var readModel = new Model.RuleRegulationReadRange();
                            readModel.RuleTypeId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.Position;
                            readModel.CompanyId = PositionList[i].company_id;
                            readModel.CompanyName = PositionList[i].company_name;
                            readModel.PositionId = PositionList[i].id;
                            readModel.PositionName = PositionList[i].name;
                            readModelList.Add(readModel);
                        }
                        _readRangeRepository.AddRange(readModelList);
                    }
                    tr.Complete();
                    result.Code = "00";
                    result.Succeed = true;
                }
                catch (Exception ex)
                {
                    tr.Dispose();
                    result.Succeed = false;
                    result.Message = ex.Message;
                }
                return result;
            }
        }
        private Result CheckData(Model.RuleRegulation model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            return Result.Success();
        }
    }
    public class RuleRegulationReadRangeBusiness : Business<Model.RuleRegulationReadRange>
    {
        private readonly RuleRegulationReadRangeRepository _repository;
        public RuleRegulationReadRangeBusiness(IServiceProvider provider, RuleRegulationReadRangeRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
    }
    }
