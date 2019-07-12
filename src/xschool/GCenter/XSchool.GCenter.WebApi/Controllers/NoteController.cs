using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
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
        public NoteController(NoteBusinesses business, RuleRegulationTypeBusiness ruleRegulationTypeBusiness, RuleRegulationBusiness ruleRegulationBusiness, BasicInfoWrapper basicInfoWrapper)
        {
            this._business = business;
            this._ruleRegulationTypeBusiness = ruleRegulationTypeBusiness;
            this._ruleRegulationBusiness = ruleRegulationBusiness;
            _basicInfoWrapper = basicInfoWrapper;
        }

        #region 通知公告管理
        [HttpPost]
        [Description("添加通知公告")]
        public Result AddNote([FromForm]Model.Note model)
        {
            model.CreateDate = DateTime.Now;
            model.PublisherId = 1;
            return _business.Add(model);
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
            condition.And(p =>search.Title==null? 1==1: p.Title == search.Title);
            var pageList= _business.Page(page, limit, condition.Combine(), order);
            return pageList;
        }
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
        public Result<IPageCollection<Model.RuleRegulation>> GetRuleRegulationPage([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]Model.RuleRegulationSearch search)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("Id", OrderBy.Desc)
            };
            var condition = new Condition<Model.RuleRegulation>();
            condition.And(p => search.Title == null ? 1 == 1 : p.Title == search.Title);
            var pageList = _ruleRegulationBusiness.Page(page, limit, condition.Combine(), order);
            return pageList;
        }
        /// <summary>
        /// 获取制度新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("制度管理新增")]
        public Result RuleRegulationAdd([FromForm]Model.RuleRegulation model)
        {
            return _ruleRegulationBusiness.Add(model);
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
