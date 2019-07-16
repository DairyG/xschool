using System;
using System.Collections.Generic;
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
        public NoteBusinesses(IServiceProvider provider, NoteRepository repository, NoteReadRangeRepository readrepository) : base(provider, repository)
        {
            this._repository = repository;
            this._readrepository = readrepository;
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
        public NoteReadRangeBusinesses(IServiceProvider provider, NoteReadRangeRepository readrepository) : base(provider, readrepository)
        {
            this._readrepository = readrepository;
        }
    }
}
