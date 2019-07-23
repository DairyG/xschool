using System;
using System.Collections.Generic;
using System.Linq;
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
        public Result Update(Model.RuleRegulation model, List<Model.User> userList, List<Model.Dep> DepList, List<Model.Com> ComList, List<Model.Position> PositionList)
        {
            using (TransactionScope tr = new TransactionScope())
            {
                Result result = new Result();
                try
                {
                    var res = CheckData(model);
                    base.Update(model);
                    _readRangeRepository.Delete(x => x.RuleTypeId == model.Id);
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
        /// <summary>
        /// 获取阅读范围
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public ChooseUser ChooseUser(int RuleTypeId)
        {
            List<RuleRegulationReadRange> list = _readRangeRepository.Query(x => x.RuleTypeId == RuleTypeId).ToList();
            ChooseUser chooseUser = new ChooseUser();
            RuleRegulation readRange = _repository.GetSingle(x => x.Id == RuleTypeId);
            chooseUser.sel_type = readRange.SelType;
            chooseUser.user = (from a in list
                               where a.TypeId == OrgType.User
                               select new User
                               {
                                   id = a.UserId,
                                   name = a.UserName,
                                   dpt_id = a.DptId,
                                   dpt_name = a.DptName,
                                   company_id = a.CompanyId,
                                   company_name = a.CompanyName
                               }).ToList();
            chooseUser.department = (from a in list
                                     where a.TypeId == OrgType.Dep
                                     select new Dep
                                     {
                                         id = a.DptId,
                                         name = a.DptName,
                                         company_id = a.CompanyId,
                                         company_name = a.CompanyName
                                     }).ToList();
            chooseUser.company = (from a in list
                                  where a.TypeId == OrgType.Com
                                  select new Com
                                  {
                                      id = a.CompanyId,
                                      name = a.CompanyName
                                  }).ToList();
            chooseUser.position = (from a in list
                                   where a.TypeId == OrgType.Position
                                   select new Position
                                   {
                                       id = a.PositionId,
                                       name = a.PositionName,
                                       company_id = a.CompanyId,
                                       company_name = a.CompanyName
                                   }).ToList();
            chooseUser.dpt_position = new List<dpt_position>();
            return chooseUser;
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
    public class RuleRegulationReadBusiness : Business<Model.RuleRegulationRead>
    {
        private readonly RuleRegulationReadRepository _repository;
        public RuleRegulationReadBusiness(IServiceProvider provider, RuleRegulationReadRepository repository) : base(provider, repository)
        {
            this._repository = repository;
        }
    }
}
