using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
        public NoteController(NoteBusinesses business, BasicInfoWrapper basicInfoWrapper)
        {
            this._business = business;
            _basicInfoWrapper = basicInfoWrapper;
        }
        [HttpPost]
        [Description("添加通知公告")]
        public Result AddNote([FromForm]Model.Note model)
        {
            model.CreateDate = DateTime.Now;
            model.Title = "公告管理测试";
            model.PublisherId = 1;
            model.Content = "功能内容不晓得";
            return _business.Add(model);
        }
        [HttpPost]
        [Description("通知公告列表")]
        public Result<IPageCollection<Model.Note>> GetNotePage([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]string search)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("Sort", OrderBy.Asc)
            };
            var condition = new Condition<Model.Note>();
            condition.And(p =>search==null? 1==1: p.Title == search);
            return _business.Page(page, limit, condition.Combine(),order);
        }
        [HttpGet]
        [Description("添加通知公告")]
        public Result test()
        {
            Result model = new Result();
            model.Code = "00";
            model.Message = "测试成功！";
            model.Succeed = true;
            return model;
        }
    }
}
