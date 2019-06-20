using System;
using System.Collections.Generic;
using System.Linq;
using XSchool.Businesses;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Helpers;

namespace XSchool.GCenter.Businesses.Wrappers
{
    public class BasicInfoWrapper : BusinessWrapper
    {
        private WorkerInFieldSettingBusiness _workerInFieldSettingBusiness;
        public BasicInfoWrapper(WorkerInFieldSettingBusiness workerInFieldSettingBusiness)
        {
            _workerInFieldSettingBusiness = workerInFieldSettingBusiness;
        }

        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <param name="type">类型，多个以,分割</param>
        /// <returns></returns>
        public BasicInfoResultDto GetData(string type)
        {
            var model = new BasicInfoResultDto();
            List<int> lsType = new List<string>(type.Split(',')).Select(t => Convert.ToInt32(t)).ToList();
            if (lsType.Count() == 0)
            {
                return model;
            }

            //查询全部基础有效数据
            var lsBasic = _workerInFieldSettingBusiness.Query(p => p.WorkinStatus == EDStatus.Enable && lsType.Contains((int)p.Type));

            foreach (var item in lsType)
            {
                switch (item)
                {
                    case (int)BasicInfoType.WorkerInField:
                        model.WorkerInField = lsBasic.Where(p => p.Type == BasicInfoType.WorkerInField).OrderBy(p => p.SortId).MapToList<BasicInfoDto>();
                        break;
                    case (int)BasicInfoType.InterviewMethod:
                        model.InterviewMethod = lsBasic.Where(p => p.Type == BasicInfoType.InterviewMethod).OrderBy(p => p.SortId).MapToList<BasicInfoDto>();
                        break;
                    case (int)BasicInfoType.Education:
                        model.Education = lsBasic.Where(p => p.Type == BasicInfoType.Education).OrderBy(p => p.SortId).MapToList<BasicInfoDto>();
                        break;
                    case (int)BasicInfoType.Properties:
                        model.Properties = lsBasic.Where(p => p.Type == BasicInfoType.Properties).OrderBy(p => p.SortId).MapToList<BasicInfoDto>();
                        break;
                    case (int)BasicInfoType.SocialRelations:
                        model.SocialRelations = lsBasic.Where(p => p.Type == BasicInfoType.SocialRelations).OrderBy(p => p.SortId).MapToList<BasicInfoDto>();
                        break;
                    case (int)BasicInfoType.RecruitmentSource:
                        model.RecruitmentSource = lsBasic.Where(p => p.Type == BasicInfoType.RecruitmentSource).OrderBy(p => p.SortId).MapToList<BasicInfoDto>();
                        break;
                    case (int)BasicInfoType.ContractNature:
                        model.ContractNature = lsBasic.Where(p => p.Type == BasicInfoType.ContractNature).OrderBy(p => p.SortId).MapToList<BasicInfoDto>();
                        break;
                    case (int)BasicInfoType.WagesType:
                        model.WagesType = lsBasic.Where(p => p.Type == BasicInfoType.WagesType).OrderBy(p => p.SortId).MapToList<BasicInfoDto>();
                        break;
                    case (int)BasicInfoType.InsuranceType:
                        model.InsuranceType = lsBasic.Where(p => p.Type == BasicInfoType.InsuranceType).OrderBy(p => p.SortId).MapToList<BasicInfoDto>();
                        break;
                }
            }
            return model;
        }
    }
}
