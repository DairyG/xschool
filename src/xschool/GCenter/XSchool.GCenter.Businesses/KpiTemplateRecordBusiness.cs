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
    public class KpiTemplateRecordBusiness : Business<KpiTemplateRecord>
    {
        private readonly KpiTemplateRecordRepository _tplRecordRepository;
        private readonly KpiTemplateRepository _tplRepository;
        private readonly KpiTemplateDetailRepository _tplDetailRepository;
        private readonly KpiTemplateAuditRecordRepository _tplAuditRecordRepository;
        public KpiTemplateRecordBusiness(IServiceProvider provider,
            KpiTemplateRecordRepository tplRecordRepository,
            KpiTemplateRepository tplRepository,
            KpiTemplateDetailRepository tplDetailRepository,
            KpiTemplateAuditRecordRepository tplAuditRecordRepository) : base(provider, tplRecordRepository)
        {
            _tplRecordRepository = tplRecordRepository;
            _tplRepository = tplRepository;
            _tplDetailRepository = tplDetailRepository;
            _tplAuditRecordRepository = tplAuditRecordRepository;
        }

        public Result Add(KpiEvaluationTemplatSubmitDto modelDto)
        {
            try
            {
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    if (modelDto.TemplateRecord.Count == 0)
                    {
                        return Result.Fail("请选择考核对象");
                    }
                    if (modelDto.TemplateAuditRecord.Count == 0)
                    {
                        return Result.Fail("请设置审核人");
                    }
                    if (modelDto.TemplateDetail.Count == 0)
                    {
                        return Result.Fail("请设置考核内容");
                    }

                    //查询考核模板记录 是否存在
                    List<KpiTemplateRecord> lsExistTplRecord = new List<KpiTemplateRecord>();
                    //查询人员模板 是否存在
                    List<KpiTemplate> lsExistTpl = new List<KpiTemplate>();
                    if (modelDto.KpiType == KpiType.Dept) //部门
                    {
                        var dptIds = modelDto.TemplateRecord.Select(p => p.DptId);

                        lsExistTplRecord = _tplRecordRepository.Query(p => p.KpiType == modelDto.KpiType && dptIds.Contains(p.DptId)).ToList();
                        lsExistTpl = _tplRepository.Query(p => p.KpiType == modelDto.KpiType && dptIds.Contains(p.DptId)).ToList();
                    }
                    else //人员
                    {
                        var employeeIds = modelDto.TemplateRecord.Select(p => p.EmployeeId);
                        lsExistTplRecord = _tplRecordRepository.Query(p => p.KpiType == modelDto.KpiType && employeeIds.Contains(p.EmployeeId)).ToList();
                        lsExistTpl = _tplRepository.Query(p => p.KpiType == modelDto.KpiType && employeeIds.Contains(p.EmployeeId)).ToList();
                    }

                    //考核模板记录
                    var addTemplateRecord = _tplRecordRepository.AddRange(modelDto.TemplateRecord);
                    if (addTemplateRecord <= 0)
                    {
                        return Result.Fail("操作失败");
                    }

                    //考核模板明细
                    List<KpiTemplateDetail> listTplDetail = new List<KpiTemplateDetail>();
                    //考核模板审核记录
                    List<KpiTemplateAuditRecord> listTplAuditRecord = new List<KpiTemplateAuditRecord>();
                    //考核模板
                    List<KpiTemplate> listAddTpl = new List<KpiTemplate>();
                    List<KpiTemplate> listEditTpl = new List<KpiTemplate>();
                    foreach (var record in modelDto.TemplateRecord)
                    {
                        foreach (var itemDetail in modelDto.TemplateDetail)
                        {
                            listTplDetail.Add(new KpiTemplateDetail
                            {
                                Id = 0,
                                KpiTemplateRecordId = record.Id,
                                CompanyId = record.CompanyId,
                                DptId = record.DptId,
                                EmployeeId = record.EmployeeId,
                                EvaluationId = itemDetail.EvaluationId,
                                EvaluationName = itemDetail.EvaluationName,
                                EvaluationType = itemDetail.EvaluationType,
                                Weight = itemDetail.Weight,
                                Explain = itemDetail.Explain
                            });
                        }

                        foreach (var itemAuditRecord in modelDto.TemplateAuditRecord)
                        {
                            listTplAuditRecord.Add(new KpiTemplateAuditRecord
                            {
                                Id = 0,
                                KpiTemplateRecordId = record.Id,
                                Steps = itemAuditRecord.Steps,
                                CompanyId = itemAuditRecord.CompanyId,
                                CompanyName = itemAuditRecord.CompanyName,
                                DptId = itemAuditRecord.DptId,
                                DptName = itemAuditRecord.DptName,
                                JobId = itemAuditRecord.JobId,
                                JobName = itemAuditRecord.JobName
                            });
                        }

                        #region 考核模板
                        KpiTemplate modelExistTpl = null;
                        if (modelDto.KpiType == KpiType.Dept) //部门
                        {
                            modelExistTpl = lsExistTpl.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId);
                        }
                        else //人员
                        {
                            modelExistTpl = lsExistTpl.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId && p.EmployeeId == record.EmployeeId);
                        }
                        if (modelExistTpl == null)
                        {
                            listAddTpl.Add(new KpiTemplate()
                            {
                                Id = 0,
                                KpiType = KpiType.User,
                                CompanyId = record.CompanyId,
                                CompanyName = record.CompanyName,
                                DptId = record.DptId,
                                DptName = record.DptName,
                                EmployeeId = record.EmployeeId,
                                UserName = record.UserName,
                                Monthly = modelDto.KpiId == KpiPlan.Monthly ? record.Id : 0,
                                Quarter = modelDto.KpiId == KpiPlan.Quarter ? record.Id : 0,
                                HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? record.Id : 0,
                                Annual = modelDto.KpiId == KpiPlan.Annual ? record.Id : 0,
                            });
                        }
                        else
                        {
                            modelExistTpl.Monthly = modelDto.KpiId == KpiPlan.Monthly ? record.Id : modelExistTpl.Monthly;
                            modelExistTpl.Quarter = modelDto.KpiId == KpiPlan.Quarter ? record.Id : modelExistTpl.Quarter;
                            modelExistTpl.HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? record.Id : modelExistTpl.HalfYear;
                            modelExistTpl.Annual = modelDto.KpiId == KpiPlan.Annual ? record.Id : modelExistTpl.Annual;
                            listEditTpl.Add(modelExistTpl);
                        }

                        #endregion
                    }

                    _tplDetailRepository.AddRange(listTplDetail);
                    _tplAuditRecordRepository.AddRange(listTplAuditRecord);
                    _tplRepository.AddRange(listAddTpl);
                    if (listEditTpl.Count > 0)
                    {
                        _tplRepository.UpdateRange(listEditTpl);
                    }

                    ts.Complete();
                    return Result.Success();
                }
            }
            catch (Exception ex)
            {
                return Result.Fail("添加失败：" + ex.Message);
            }
        }

        public Result Edit(KpiEvaluationTemplatSubmitDto modelDto)
        {
            try
            {
                using (System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope())
                {
                    if (modelDto.TemplateRecord.Count == 0)
                    {
                        return Result.Fail("请选择考核对象");
                    }
                    if (modelDto.TemplateAuditRecord.Count == 0)
                    {
                        return Result.Fail("请设置审核人");
                    }
                    if (modelDto.TemplateDetail.Count == 0)
                    {
                        return Result.Fail("请设置考核内容");
                    }

                    var kpiTemplateRecordIds = modelDto.TemplateRecord.Select(p => p.Id);

                    //查询考核模板记录 是否存在
                    List<KpiTemplateRecord> lsExistTplRecord = new List<KpiTemplateRecord>();
                    //查询人员模板 是否存在
                    List<KpiTemplate> lsExistTpl = new List<KpiTemplate>();
                    if (modelDto.KpiType == KpiType.Dept) //部门
                    {
                        var dptIds = modelDto.TemplateRecord.Select(p => p.DptId);

                        lsExistTpl = _tplRepository.Query(p => p.KpiType == modelDto.KpiType && dptIds.Contains(p.DptId)).ToList();
                    }
                    else //人员
                    {
                        var employeeIds = modelDto.TemplateRecord.Select(p => p.EmployeeId);
                        lsExistTpl = _tplRepository.Query(p => p.KpiType == modelDto.KpiType && employeeIds.Contains(p.EmployeeId)).ToList();
                    }

                    //考核模板明细
                    List<KpiTemplateDetail> listTplDetail = new List<KpiTemplateDetail>();
                    //考核模板审核记录
                    List<KpiTemplateAuditRecord> listTplAuditRecord = new List<KpiTemplateAuditRecord>();
                    //考核模板
                    List<KpiTemplate> listAddTpl = new List<KpiTemplate>();
                    List<KpiTemplate> listEditTpl = new List<KpiTemplate>();
                    foreach (var record in modelDto.TemplateRecord)
                    {
                        foreach (var itemDetail in modelDto.TemplateDetail)
                        {
                            listTplDetail.Add(new KpiTemplateDetail
                            {
                                Id = 0,
                                KpiTemplateRecordId = record.Id,
                                CompanyId = record.CompanyId,
                                DptId = record.DptId,
                                EmployeeId = record.EmployeeId,
                                EvaluationId = itemDetail.EvaluationId,
                                EvaluationName = itemDetail.EvaluationName,
                                EvaluationType = itemDetail.EvaluationType,
                                Weight = itemDetail.Weight,
                                Explain = itemDetail.Explain
                            });
                        }

                        foreach (var itemAuditRecord in modelDto.TemplateAuditRecord)
                        {
                            listTplAuditRecord.Add(new KpiTemplateAuditRecord
                            {
                                Id = 0,
                                KpiTemplateRecordId = record.Id,
                                Steps = itemAuditRecord.Steps,
                                CompanyId = itemAuditRecord.CompanyId,
                                CompanyName = itemAuditRecord.CompanyName,
                                DptId = itemAuditRecord.DptId,
                                DptName = itemAuditRecord.DptName,
                                JobId = itemAuditRecord.JobId,
                                JobName = itemAuditRecord.JobName
                            });
                        }

                        #region 考核模板
                        KpiTemplate modelExistTpl = null;
                        if (modelDto.KpiType == KpiType.Dept) //部门
                        {
                            modelExistTpl = lsExistTpl.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId);
                        }
                        else //人员
                        {
                            modelExistTpl = lsExistTpl.FirstOrDefault(p => p.CompanyId == record.CompanyId && p.DptId == record.DptId && p.EmployeeId == record.EmployeeId);
                        }
                        if (modelExistTpl == null)
                        {
                            listAddTpl.Add(new KpiTemplate()
                            {
                                Id = 0,
                                KpiType = KpiType.User,
                                CompanyId = record.CompanyId,
                                CompanyName = record.CompanyName,
                                DptId = record.DptId,
                                DptName = record.DptName,
                                EmployeeId = record.EmployeeId,
                                UserName = record.UserName,
                                Monthly = modelDto.KpiId == KpiPlan.Monthly ? record.Id : 0,
                                Quarter = modelDto.KpiId == KpiPlan.Quarter ? record.Id : 0,
                                HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? record.Id : 0,
                                Annual = modelDto.KpiId == KpiPlan.Annual ? record.Id : 0,
                            });
                        }
                        else
                        {
                            modelExistTpl.Monthly = modelDto.KpiId == KpiPlan.Monthly ? record.Id : modelExistTpl.Monthly;
                            modelExistTpl.Quarter = modelDto.KpiId == KpiPlan.Quarter ? record.Id : modelExistTpl.Quarter;
                            modelExistTpl.HalfYear = modelDto.KpiId == KpiPlan.HalfYear ? record.Id : modelExistTpl.HalfYear;
                            modelExistTpl.Annual = modelDto.KpiId == KpiPlan.Annual ? record.Id : modelExistTpl.Annual;
                            listEditTpl.Add(modelExistTpl);
                        }

                        #endregion
                    }

                    _tplDetailRepository.Delete(p => kpiTemplateRecordIds.Contains(p.KpiTemplateRecordId));
                    _tplAuditRecordRepository.Delete(p => kpiTemplateRecordIds.Contains(p.KpiTemplateRecordId));

                    _tplDetailRepository.AddRange(listTplDetail);
                    _tplAuditRecordRepository.AddRange(listTplAuditRecord);
                    if (listAddTpl.Count > 0)
                    {
                        _tplRepository.AddRange(listAddTpl);
                    }
                    if (listEditTpl.Count > 0)
                    {
                        _tplRepository.UpdateRange(listEditTpl);
                    }

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
