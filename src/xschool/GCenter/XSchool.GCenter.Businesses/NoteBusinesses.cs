using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class NoteBusinesses : Business<Model.Note>
    {
        private readonly NoteRepository _repository;
        private readonly NoteReadRangeRepository _readrepository;
        private readonly NoteReadRepository _noteReadRepository;
        public NoteBusinesses(IServiceProvider provider, NoteRepository repository, NoteReadRangeRepository readrepository,
            NoteReadRepository noteReadRepository) : base(provider, repository)
        {
            this._repository = repository;
            this._readrepository = readrepository;
            this._noteReadRepository = noteReadRepository;
        }
        public Result Add(Model.Note model, List<Model.User> userList,List<Model.Dep> DepList,List<Model.Com> ComList,List<Model.Position> PositionList)
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
                        List<Model.NoteReadRange> readModelList = new List<Model.NoteReadRange>();
                        for (var i = 0; i < userList.Count; i++)
                        {
                            var readModel = new Model.NoteReadRange();
                            readModel.NoteId = model.Id;
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
                        _readrepository.AddRange(readModelList);
                    }
                    if (DepList != null)
                    {
                        List<Model.NoteReadRange> readModelList = new List<Model.NoteReadRange>();
                        for (var i = 0; i < DepList.Count; i++)
                        {
                            var readModel = new Model.NoteReadRange();
                            readModel.NoteId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.Dep;
                            readModel.DptId = DepList[i].id;
                            readModel.DptName = DepList[i].name;
                            readModel.CompanyId = DepList[i].company_id;
                            readModel.CompanyName = DepList[i].company_name;
                            readModelList.Add(readModel);
                        }
                        _readrepository.AddRange(readModelList);
                    }
                    if (ComList != null)
                    {
                        List<Model.NoteReadRange> readModelList = new List<Model.NoteReadRange>();
                        for (var i = 0; i < ComList.Count; i++)
                        {
                            var readModel = new Model.NoteReadRange();
                            readModel.NoteId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.Com;
                            readModel.CompanyId = ComList[i].id;
                            readModel.CompanyName = ComList[i].name;
                            readModelList.Add(readModel);
                        }
                        _readrepository.AddRange(readModelList);
                    }
                    if (PositionList != null)
                    {
                        List<Model.NoteReadRange> readModelList = new List<Model.NoteReadRange>();
                        for (var i = 0; i < PositionList.Count; i++)
                        {
                            var readModel = new Model.NoteReadRange();
                            readModel.NoteId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.Position;
                            readModel.CompanyId = PositionList[i].company_id;
                            readModel.CompanyName = PositionList[i].company_name;
                            readModel.PositionId = PositionList[i].id;
                            readModel.PositionName = PositionList[i].name;
                            readModelList.Add(readModel);
                        }
                        _readrepository.AddRange(readModelList);
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
        public Result Update(Model.Note model, List<Model.User> userList, List<Model.Dep> DepList, List<Model.Com> ComList, List<Model.Position> PositionList)
        {
            using (TransactionScope tr = new TransactionScope())
            {
                Result result = new Result();
                try
                {
                    var res = CheckData(model);
                    base.Update(model);
                    _noteReadRepository.Delete(x => x.NoteId == model.Id);
                    _readrepository.Delete(x => x.NoteId == model.Id);
                    if (userList != null)
                    {
                        List<Model.NoteReadRange> readModelList = new List<Model.NoteReadRange>();
                        for (var i = 0; i < userList.Count; i++)
                        {
                            var readModel = new Model.NoteReadRange();
                            readModel.NoteId = model.Id;
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
                        _readrepository.AddRange(readModelList);
                    }
                    if (DepList != null)
                    {
                        List<Model.NoteReadRange> readModelList = new List<Model.NoteReadRange>();
                        for (var i = 0; i < DepList.Count; i++)
                        {
                            var readModel = new Model.NoteReadRange();
                            readModel.NoteId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.Dep;
                            readModel.DptId = DepList[i].id;
                            readModel.DptName = DepList[i].name;
                            readModel.CompanyId = DepList[i].company_id;
                            readModel.CompanyName = DepList[i].company_name;
                            readModelList.Add(readModel);
                        }
                        _readrepository.AddRange(readModelList);
                    }
                    if (ComList != null)
                    {
                        List<Model.NoteReadRange> readModelList = new List<Model.NoteReadRange>();
                        for (var i = 0; i < ComList.Count; i++)
                        {
                            var readModel = new Model.NoteReadRange();
                            readModel.NoteId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.Com;
                            readModel.CompanyId = ComList[i].id;
                            readModel.CompanyName = ComList[i].name;
                            readModelList.Add(readModel);
                        }
                        _readrepository.AddRange(readModelList);
                    }
                    if (PositionList != null)
                    {
                        List<Model.NoteReadRange> readModelList = new List<Model.NoteReadRange>();
                        for (var i = 0; i < PositionList.Count; i++)
                        {
                            var readModel = new Model.NoteReadRange();
                            readModel.NoteId = model.Id;
                            readModel.ReadDate = DateTime.Now;
                            readModel.IsRead = 0;
                            readModel.TypeId = OrgType.Position;
                            readModel.CompanyId = PositionList[i].company_id;
                            readModel.CompanyName = PositionList[i].company_name;
                            readModel.PositionId = PositionList[i].id;
                            readModel.PositionName = PositionList[i].name;
                            readModelList.Add(readModel);
                        }
                        _readrepository.AddRange(readModelList);
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
        private Result CheckData(Model.Note model)
        {
            if (model == null)
            {
                return Result.Fail("数据不能为空");
            }
            return Result.Success();
        }
    }
    public class NoteReadRangeBusinesses : Business<Model.NoteReadRange>
    {
        private readonly NoteReadRangeRepository _readrepository;
        private readonly NoteRepository _repository;
        public NoteReadRangeBusinesses(IServiceProvider provider, NoteReadRangeRepository readrepository, NoteRepository repository) : base(provider, readrepository)
        {
            this._readrepository = readrepository;
            this._repository = repository;
        }
        /// <summary>
        /// 获取阅读范围
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public ChooseUser ChooseUser(int NoteId)
        {
            List<NoteReadRange> list = _readrepository.Query(x => x.NoteId == NoteId).ToList();
            ChooseUser chooseUser = new ChooseUser();
            Note note = _repository.GetSingle(x => x.Id == NoteId);
            chooseUser.sel_type = note.SelType;
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
            chooseUser.department= (from a in list
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
            chooseUser.dpt_position =new List<dpt_position>();
            return chooseUser;
        }
    }
}
