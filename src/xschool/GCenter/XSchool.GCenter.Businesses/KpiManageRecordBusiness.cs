using System;
using System.Collections.Generic;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.GCenter.Repositories;
using XSchool.Helpers;
using System.Linq;

namespace XSchool.GCenter.Businesses
{
    public class KpiManageRecordBusiness : Business<KpiManageRecord>
    {
        private readonly KpiManageRecordRepository _repository;
        private readonly KpiManageDetailRepository _detailRepository;
        private readonly KpiManageTemplateRepository _templateRepository;
        private readonly KpiManageAuditRecordRepository _auditRecordRepository;
        public KpiManageRecordBusiness(IServiceProvider provider, KpiManageRecordRepository repository, KpiManageDetailRepository detailRepository, KpiManageTemplateRepository templateRepository, KpiManageAuditRecordRepository auditRecordRepository) : base(provider, repository)
        {
            _repository = repository;
            _detailRepository = detailRepository;
            _templateRepository = templateRepository;
            _auditRecordRepository = auditRecordRepository;
        }

        public Result Add(KpiEvaluationSubmitDto modelDto)
        {
            try
            {
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    DateTime dtNow = DateTime.Now;
                    var year = dtNow.Year;
                    string kpiDate = string.Empty,
                        errorTip = string.Empty;
                    switch (modelDto.KpiId)
                    {
                        case KpiPlan.Monthly:
                            kpiDate = dtNow.Month.ToString();
                            var mmm = TimeHelper.LastDayOfMonth(dtNow);
                            var mmm2 = DateTime.Parse(dtNow.ToString("yyyy-MM-dd"));
                            if (DateTime.Compare(DateTime.Parse(dtNow.ToString("yyyy-MM-dd")), TimeHelper.LastDayOfMonth(dtNow)) >= 0)
                            {
                                errorTip = "月末啦，请勿修改绩效考核方案";
                            }
                            break;
                        case KpiPlan.Quarter:
                            kpiDate = TimeHelper.GetQuarterName(dtNow);
                            if (DateTime.Compare(DateTime.Parse(dtNow.ToString("yyyy-MM-dd")), TimeHelper.ToLastDayOfSeason(dtNow)) >= 0)
                            {
                                errorTip = "季度末啦，请勿修改绩效考核方案";
                            }
                            break;
                        case KpiPlan.HalfYear:
                            kpiDate = "上半年";
                            if (dtNow.Month > 6)
                            {
                                kpiDate = "下半年";
                            }
                            break;
                        case KpiPlan.Annual:
                            kpiDate = year.ToString();
                            if (DateTime.Compare(DateTime.Parse(dtNow.ToString("yyyy-MM-dd")), TimeHelper.ToLastDayOfSeason(DateTime.Parse(year + "-12-01"))) >= 0)
                            {
                                errorTip = "年末啦，请勿修改绩效考核方案";
                            }
                            break;
                    }

                    if (!string.IsNullOrWhiteSpace(errorTip))
                    {
                        return Result.Fail(errorTip);
                    }
                    if (modelDto.ManageRecord.Count == 0)
                    {
                        return Result.Fail("请选择考核对象");
                    }
                    if (modelDto.ManageAuditRecord.Count == 0)
                    {
                        return Result.Fail("请设置审核人");
                    }
                    if (modelDto.ManageDetail.Count == 0)
                    {
                        return Result.Fail("请设置考核内容");
                    }

                    //查询添加的人员是否存在
                    var ids = modelDto.ManageRecord.Select(p => p.EmployeeId);
                    List<KpiManageRecord> lsExistRecord = _repository.Query(p => ids.Contains(p.EmployeeId) && p.KpiId == modelDto.KpiId && p.KpiType == modelDto.KpiType && p.KpiDate == kpiDate && p.Year == year).ToList();

                    //用于修改
                    List<KpiManageRecord> lsEditRecord = new List<KpiManageRecord>();
                    //用于添加
                    List<KpiManageRecord> lsAddRecord = new List<KpiManageRecord>();
                    //考核管理记录
                    foreach (var item in modelDto.ManageRecord)
                    {
                        item.Year = year;
                        item.KpiDate = kpiDate;
                        item.AddDate = DateTime.Now;
                        item.Status = KpiStatus.Zero;
                        item.Steps = KpiSteps.Zero;
                        item.StepsAuditId = item.EmployeeId;
                        item.StepsAuditName = item.UserName;

                        var modelExistRecord = lsExistRecord.FirstOrDefault(p => p.CompanyId == item.CompanyId && p.DptId == item.DptId);
                        if (modelExistRecord != null)
                        {
                            lsEditRecord.Add(modelExistRecord);
                        }
                        else
                        {
                            lsAddRecord.Add(item);
                        }

                        //考核管理模板
                        #region
                        /*
                        var modelTpl = _templateRepository.GetSingle(p => p.Year == year && p.KpiType == KpiType.User && p.EmployeeId == item.EmployeeId);
                        if (modelTpl == null)
                        {
                            modelTpl = new KpiManageTemplate()
                            {
                                KpiType = KpiType.User,
                                CompanyId = item.CompanyId,
                                CompanyName = item.CompanyName,
                                DptId = item.DptId,
                                DptName = item.DptName,
                                EmployeeId = item.EmployeeId,
                                UserName = item.UserName,
                                Year = item.Year,
                                Monthly = modelDto.KpiId == KpiPlan.Monthly ? item.Id : 0,
                                Quarter = modelDto.KpiId == KpiPlan.Quarter ? item.Id : 0,
                                HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? item.Id : 0,
                                Annual = modelDto.KpiId == KpiPlan.Annual ? item.Id : 0,
                            };
                            var addTemplate = _templateRepository.Add(modelTpl);
                            if (addTemplate <= 0)
                            {
                                return Result.Fail("添加失败");
                            }
                        }
                        else
                        {
                            modelTpl.Monthly = modelDto.KpiId == KpiPlan.Monthly ? item.Id : modelTpl.Monthly;
                            modelTpl.Quarter = modelDto.KpiId == KpiPlan.Quarter ? item.Id : modelTpl.Quarter;
                            modelTpl.HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? item.Id : modelTpl.HalfYear;
                            modelTpl.Annual = modelDto.KpiId == KpiPlan.Annual ? item.Id : modelTpl.Annual;
                            var updateTemplate = _templateRepository.Update(modelTpl);
                            if (updateTemplate <= 0)
                            {
                                return Result.Fail("添加失败");
                            }
                        }
                        */
                        #endregion

                        //考核管理审核记录
                        //List<KpiManageAuditRecord> listAuditRecord = new List<KpiManageAuditRecord>();
                        //foreach (var itemAuditRecord in modelDto.ManageAuditRecord)
                        //{
                        //    itemAuditRecord.Id = 0;
                        //    itemAuditRecord.KpiManageRecordId = item.Id;
                        //    listAuditRecord.Add(itemAuditRecord);
                        //}
                        //var addAuditRecord = _auditRecordRepository.AddRange(listAuditRecord);
                        //if (addAuditRecord <= 0)
                        //{
                        //    return Result.Fail("添加失败");
                        //}
                    }

                    //添加操作
                    _repository.AddRange(lsAddRecord);
                    List<KpiManageDetail> listDetail = new List<KpiManageDetail>();
                    foreach (var record in lsAddRecord)
                    {
                        foreach (var itemDetail in modelDto.ManageDetail)
                        {
                            itemDetail.Id = 0;
                            itemDetail.KpiManageRecordId = record.Id;
                            itemDetail.Year = record.Year;
                            itemDetail.KpiDate = record.KpiDate;
                            itemDetail.Status = NomalStatus.Valid;
                            listDetail.Add(itemDetail);
                        }
                    }
                    _detailRepository.AddRange(listDetail);

                    //修改操作


                    ts.Complete();
                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail("添加失败：" + ex.Message);
            }
        }

    }
}
