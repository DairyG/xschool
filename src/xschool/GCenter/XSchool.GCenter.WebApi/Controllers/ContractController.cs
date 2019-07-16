using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using XSchool.Core;
using XSchool.GCenter.Businesses;
using XSchool.GCenter.Businesses.Wrappers;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Helpers;
using XSchool.Query.Pageing;


namespace XSchool.GCenter.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class ContractController : ApiBaseController
    {
        private readonly ContractBusiness _business;
        public class Search
        {
            public string Type { get; set; }
            public string No { get; set; }
        };
        public ContractController(ContractBusiness business)
        {
            _business = business;
        }

        [HttpPost]
        [Description("添加、修改合同")]
        public Result Edit([FromForm]Contract model)
        {
            return _business.AddOrEdit(model);
        }

        [HttpPost]
        [Description("根据Id获取基础数据")]
        public Contract GetSingle([FromForm]int Id)
        {
            return _business.GetSingle(Id);
        }

        //[HttpPost]
        //[Description("获取合同列表")]
        //public Result<IPageCollection<Contract>> Get([FromForm]int page, [Range(1, 50)][FromForm]int limit, [FromForm]String search)
        //{
        //    List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
        //    {
        //        new KeyValuePair<string, OrderBy>("Id", OrderBy.Asc)
        //    };
        //    var condition = new Condition<Contract>();
        //    Search ser = new Search();
        //    ser = JsonConvert.DeserializeObject<Search>(search);

        //    if (!string.IsNullOrWhiteSpace(ser.Type.ToString())) {
        //        condition.And(p => p.Type == Convert.ToInt32(ser.Type));
        //    }
        //    if (!string.IsNullOrWhiteSpace(ser.No))
        //    {
        //        condition.And(p => p.No.Contains(ser.No));
        //    }
        //    return _business.Page(page, limit, condition.Combine(),p => new
        //    {
        //        p.Id,
        //        p.Type,
        //        p.No,
        //        p.RelationNo,
        //        p.Title,
        //        p.StartTime,
        //        p.EndTime,
        //        p.Amount,
        //        p.PayNum,
        //        p.PayItems
        //    }, order);
        //}

        [HttpPost]
        [Description("删除合同")]
        public Result Delete([FromForm]Contract id)
        {
            return _business.Update(id);
        }


    }
}