using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSchool.WorkFlow.Model;
using XSchool.WorkFlow.Model.ViewModel;

namespace XSchool.WorkFlow.WebApi.AutoMapper
{
    /// <summary>
    /// AutoMapper映射类

    /// </summary>
    public class AutoMapperHelper
    {
        /// <summary>
        /// 注入AutoMapper
        /// </summary>
        public static void Start()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.CreateMap<SubjectRuleDto, SubjectRule>();
            cfg.CreateMap<SubjectDto, Subject>();
            cfg.CreateMap<SubjectStepDto, SubjectStep>();
            
            Mapper.Initialize(cfg);
        }
    }
}
