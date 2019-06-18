using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Linq;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Model;
using XSchool.Query.Pageing;
using System.ComponentModel.DataAnnotations;
using XSchool.Helpers;
using System.Collections.Generic;

namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class EducationInfoController : ApiBaseController
    {
        private readonly EducationInfoBusinesses _business;
        public EducationInfoController(EducationInfoBusinesses business)
        {
            this._business = business;
        }
        [HttpPost]
        [Description("添加学历")]
        public Result Add([FromForm]EducationInfoSetting educationSetting)
        {
            return _business.Add(educationSetting);
        }
        [HttpPost]
        [Description("根据Id获取学历")]
        public EducationInfoSetting GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }
        [HttpPost]
        [Description("添加学历")]
        public Result Update([FromForm]EducationInfoSetting educationSetting)
        {
            return _business.Update(educationSetting);
        }
        [HttpPost]
        [Description("获取学历列表")]
        public IPageCollection<EducationInfoSetting> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit)
        {
            var condition = new Condition<EducationInfoSetting>();
            condition.And(p => p.WorkinStatus == 1);
            return _business.Page(page, limit, condition.Combine());
        }

        [HttpPost]
        [Description("删除学历")]
        public Result Delete([FromForm]EducationInfoSetting educationSetting)
        {
            educationSetting.WorkinStatus = 0;
            return _business.Update(educationSetting);
        }
    }
}