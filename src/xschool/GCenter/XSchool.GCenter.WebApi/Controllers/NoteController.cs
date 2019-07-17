using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.Helpers;
using XSchool.Query.Pageing;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class NoteController : ApiBaseController
    {
        private readonly NoteBusinesses _business;
        private readonly BasicInfoWrapper _basicInfoWrapper;
        private readonly RuleRegulationTypeBusiness _ruleRegulationTypeBusiness;
        private readonly RuleRegulationBusiness _ruleRegulationBusiness;
        private readonly NoteReadRangeBusinesses _noteReadRangeBusinesses;
        public NoteController(NoteBusinesses business, RuleRegulationTypeBusiness ruleRegulationTypeBusiness, RuleRegulationBusiness ruleRegulationBusiness, BasicInfoWrapper basicInfoWrapper
            , NoteReadRangeBusinesses noteReadRangeBusinesses)
        {
            this._business = business;
            this._ruleRegulationTypeBusiness = ruleRegulationTypeBusiness;
            this._ruleRegulationBusiness = ruleRegulationBusiness;
            this._noteReadRangeBusinesses = noteReadRangeBusinesses;
            _basicInfoWrapper = basicInfoWrapper;
        }

        #region 通知公告管理
        /// <summary>
        /// 添加通知公告
        /// </summary>
        /// <param name="model">公告详情</param>
        /// <param name="UserList">阅读人员列表</param>
        /// <param name="DepList">阅读部门列表</param>
        /// <param name="ComList">阅读公司列表</param>
        /// <param name="PositionList">阅读职位</param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加通知公告")]
        public Result AddNote([FromForm]Model.Note model, [FromForm]List<Model.User> UserList, [FromForm]List<Model.Dep> DepList, [FromForm]List<Model.Com> ComList, [FromForm]List<Model.Position> PositionList)
        {
            if (model.Id == 0)
            {
                model.CreateDate = DateTime.Now;
                return _business.Add(model, UserList, DepList, ComList, PositionList);
            }
            else
            {
                model.CreateDate = DateTime.Now;
                return _business.Update(model);
            }
        }
        [HttpPost]
        [Description("查询已读/未读公告")]
        public Result<IPageCollection<Model.NoteReadRange>> NoteReadRange([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]int IsRead, [FromForm]int NoteId)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("Id", OrderBy.Desc)
            };
            var condition = new Condition<Model.NoteReadRange>();
            condition.And(p => (p.IsRead==IsRead&&p.NoteId==NoteId));
            var pageList = _noteReadRangeBusinesses.Page(page, limit, condition.Combine(), order);
            return pageList;
        }
        [HttpPost]
        [Description("通知公告列表")]
        public Result<IPageCollection<Model.Note>> GetNotePage([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]Model.NoteSearch search)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("Id", OrderBy.Desc)
            };
            var condition = new Condition<Model.Note>();
            var pageList= _business.Page(page, limit, condition.Combine(), order);
            return pageList;        }
        [HttpGet]
        [Description("查询某个通知公告")]
        public Result<Model.Note> GetSigleNote(int id)
        {
            var noteModel = _business.GetSingle(id);
            noteModel.ReadCount++;
            _business.Update(noteModel);
            return Result.Success(noteModel);
        }
        [HttpGet]
        [Description("删除通知公告")]
        public Result DeleteNote(int id)
        {
            return _business.Delete(id);
        }
        [HttpPost]
        [Description("通知公告附件")]
        public Result UploadUrl(HttpContext context)
        {
            Result model = new Result();
            try
            {
                model.Code = "00";
                model.Message = "上传成功！";
                model.Succeed = true;
            }
            catch (Exception ex)
            {
                model.Code = "11";
                model.Message = "上传失败：" + ex.Message;
                model.Succeed = false;
            }
            return model;
        }
        #endregion

        #region 制度类型管理
        /// <summary>
        /// 获取制度类型列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("制度管理类型列表")]
        public Result GetRuleRegulationTypeList()
        {
            var list = _ruleRegulationTypeBusiness.Query();
            return list;
        }
        /// <summary>
        /// 获取制度类型新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("制度管理类型新增")]
        public Result RuleRegulationTypeAdd([FromForm]Model.RuleRegulationType model)
        {
            return _ruleRegulationTypeBusiness.Add(model);
        }
        /// <summary>
        /// 获取制度修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("制度管理类型修改")]
        public Result RuleRegulationTypeEdit([FromForm]Model.RuleRegulationType model)
        {
            return _ruleRegulationTypeBusiness.Update(model);
        }
        /// <summary>
        /// 制度管理类型删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("制度管理类型删除")]
        public Result RuleRegulationTypeDel(int id)
        {
            return _ruleRegulationTypeBusiness.Delete(id);
        }
        #endregion

        #region 制度管理
        /// <summary>
        /// 制度管理列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("制度管理列表")]
        public Result GetRuleRegulationList()
        {
            var list = _ruleRegulationBusiness.Query();
            return list;
        }
        [HttpPost]
        [Description("通知公告列表")]
        public IPageCollection<Model.RuleRegulationPage> GetRuleRegulationPage([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]Model.RuleRegulationSearch search)
        {            
            var pageList = _ruleRegulationBusiness.GetRuleRegulationList(page, limit,search);
            return pageList;
        }
        /// <summary>
        /// 获取制度新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("制度管理新增")]
        public Result RuleRegulationAdd([FromForm]Model.RuleRegulation model, [FromForm]List<Model.User> UserList, [FromForm]List<Model.Dep> DepList, [FromForm]List<Model.Com> ComList, [FromForm]List<Model.Position> PositionList)
        {
            model.CreateDate = DateTime.Now;
            if (model.Id == 0)
                return _ruleRegulationBusiness.Add(model, UserList, DepList, ComList, PositionList);
            else
                return _ruleRegulationBusiness.Update(model);
        }
        /// <summary>
        /// 获取制度修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("制度管理修改")]
        public Result RuleRegulationEdit([FromForm]Model.RuleRegulation model)
        {
            return _ruleRegulationBusiness.Update(model);
        }
        /// <summary>
        /// 获取制度model
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("制度管理model")]
        public Result RuleRegulationDetail(int id)
        {
            var model= _ruleRegulationBusiness.GetSingle(id);
            return Result.Success(model);
        }
        /// <summary>
        /// 制度管理删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("制度管理删除")]
        public Result RuleRegulationDel(int id)
        {
            return _ruleRegulationBusiness.Delete(id);
        }
        #endregion
    }
}
