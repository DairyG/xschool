using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AnnouncementController : ApiBaseController
    {
        private readonly AnnouncementBusiness _business;
        private readonly BasicInfoWrapper _basicInfoWrapper;
        public AnnouncementController(AnnouncementBusiness business, BasicInfoWrapper basicInfoWrapper)
        {
            this._business = business;
            _basicInfoWrapper = basicInfoWrapper;
        }
        [HttpPost]
        [Description("添加基础数据")]
        public Result Add([FromForm]Announcement announcement)
        {
            //workerInField.Type = (BasicInfoType)Enum.Parse(typeof(BasicInfoType), workerInField.Type);
            announcement.IsSystem = IsSystem.No;
            announcement.AcStatus = EDStatus.Enable;
            return _business.Add(announcement);
        }
        [HttpPost]
        [Description("根据Id获取基础数据")]
        public Announcement GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("修改基础数据")]
        public Result Update([FromForm]Announcement announcement)
        {
            return _business.Update(announcement);
        }
        [HttpPost]
        [Description("获取基础数据列表")]
        public IPageCollection<Announcement> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]string search)
        {
            var condition = new Condition<Announcement>();
            condition.And(p => p.AcStatus == EDStatus.Enable);
            return _business.Page(page, limit, condition.Combine());
        }

        [HttpPost]
        [Description("删除基础数据")]
        public Result Delete([FromForm]Announcement announcement)
        {
            announcement.AcStatus = EDStatus.Disable;
            return _business.Update(announcement);
        }

        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <param name="type">类型，多个以,分割</param>
        /// <returns></returns>
        [HttpGet]
        public BasicInfoResultDto GetData(string type)
        {
            return _basicInfoWrapper.GetData(type);
        }
    }
}