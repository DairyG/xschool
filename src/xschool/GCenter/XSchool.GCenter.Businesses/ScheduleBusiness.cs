using Logistics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories;
using XSchool.Helpers;

namespace XSchool.GCenter.Businesses
{
    public class ScheduleBusiness : Business<Schedule>
    {
        private ScheduleRepository _scheduleRepository;
        public ScheduleBusiness(IServiceProvider provider, ScheduleRepository repository) : base(provider, repository)
        {
            _scheduleRepository = repository;
        }
        public override Result Add(Schedule model)
        {
            Check(model);
            model.AddTime = DateTime.Now;
            //新增
            if (model.Id <= 0)
            {
                return base.Add(model);
            }
            else
            {
                return base.Update(model);
            }
        }
        public IList<Schedule> Get()
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("AddTime", OrderBy.Desc)
            };
            return base.Query(p => p.Id > 0, p => p, order);
        }
        public IList<Schedule> Get(int eid, string catalog)
        {
            List<KeyValuePair<string, OrderBy>> order = new List<KeyValuePair<string, OrderBy>>
            {
                new KeyValuePair<string, OrderBy>("AddTime", OrderBy.Desc)
            };
            IList<Schedule> list = new List<Schedule>();
            switch (catalog)
            {
                case "All":
                    list = base.Query(p => p.Executors.Contains("," + eid.ToString() + ",") || p.EmployeeId.Equals(eid) || p.Scribbles.Contains(eid.ToString()), p => p, order);
                    break;
                case "Executors":
                    list = base.Query(p => p.Executors.Contains("," + eid.ToString() + ","), p => p, order);
                    break;
                case "EmployeeId":
                    list = base.Query(p => p.EmployeeId.Equals(eid), p => p, order);
                    break;
                case "Scribbles":
                    list = base.Query(p => p.Scribbles.Contains(eid.ToString()), p => p, order);
                    break;
            }
            return list;
        }
        /// <summary>
        /// 根据日期查询日程（写总结处使用）
        /// </summary>
        /// <param name="eid">人员ID</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public Schedule GetByDate(int eid, string date)
        {
            IList<Schedule> list = base.Query(p => p.Executors.Contains("," + eid.ToString() + ",") && p.BeginTime.ToString("yyyy-MM-dd") == date);
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        public Schedule GetSingle(int id)
        {
            return base.GetSingle(p => p.Id.Equals(id));
        }
        public Result Delete(int id)
        {
            return base.Delete(id);
        }
        public int UpdatePid(int id)
        {
            return _scheduleRepository.UpdatePid(id);
        }
        public Result Check(Schedule model)
        {
            if (string.IsNullOrWhiteSpace(model.Executors))
            {
                return Result.Fail("执行人不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.BeginTime.ToString()))
            {
                return Result.Fail("开始时间不能为空");
            }
            if (!RegexHelper.IsMobilePhone(model.EndTime.ToString()))
            {
                return Result.Fail("截止时间不能为空");
            }
            if (model.Repeat <= 0)
            {
                return Result.Fail("请选择重复");
            }
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                return Result.Fail("标题不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.Content))
            {
                return Result.Fail("任务内容不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.EmployeeId.ToString()) || model.EmployeeId <= 0)
            {
                return Result.Fail("未获取到登录信息");
            }
            return Result.Success();
        }
        #region 处理重复
        public void DoRepeat(Schedule model)
        {
            if (model != null)
            {
                //开始处理重复
                if (model.Repeat == Repeat.Day)
                {
                    //按天重复
                    //查询是否有相同的数据
                    DateTime bTime = model.BeginTime.AddDays(1);
                    DateTime eTime = model.EndTime.AddDays(1);

                    bool has = base.Exist(p => p.Pid == model.Pid && p.BeginTime == bTime && p.EndTime == eTime);
                    int tim = DateTime.Compare(bTime, Convert.ToDateTime(model.RepeatEndTime));
                    //无重复，并且重复日程的开始时间小于或等于重复结束时间
                    if (!has && tim <= 0)
                    {
                        Schedule newModel = new Schedule() {
                            Id = 0,
                            Pid = model.Pid,
                            Executors = model.Executors,
                            ExecutorsJson = model.ExecutorsJson,
                            Scribbles = model.Scribbles,
                            ScribblesJson = model.ScribblesJson,
                            KpiPlan = model.KpiPlan,
                            KpiId = model.KpiId,
                            BeginTime = bTime,
                            EndTime = eTime,
                            RemindTime = model.RemindTime,
                            RemindWay = model.RemindWay,
                            Emergency = model.Emergency,
                            Repeat = model.Repeat,
                            RepeatEndTime = model.RepeatEndTime,
                            Title = model.Title,
                            CompanyId = model.CompanyId,
                            DptId = model.DptId,
                            EmployeeId = model.EmployeeId,
                            EmployeeName = model.EmployeeName,
                            Content = model.Content,
                            FileUrl = model.FileUrl,
                            AddTime = DateTime.Now,
                            AllDay = model.AllDay
                        };
                        //无下一条重复（添加）
                        base.Add(newModel);
                    }
                }
                else if (model.Repeat == Repeat.Week)
                {
                    //按周重复
                    //查询是否有相同的数据
                    DateTime bTime = model.BeginTime.AddDays(7);
                    DateTime eTime = model.EndTime.AddDays(7);

                    bool has = base.Exist(p => p.Pid == model.Pid && p.BeginTime == bTime && p.EndTime == eTime);
                    int tim = DateTime.Compare(bTime, Convert.ToDateTime(model.RepeatEndTime));
                    //无重复，并且重复日程的开始时间小于或等于重复结束时间
                    if (!has && tim <= 0)
                    {
                        Schedule newModel = new Schedule()
                        {
                            Id = 0,
                            Pid = model.Pid,
                            Executors = model.Executors,
                            ExecutorsJson = model.ExecutorsJson,
                            Scribbles = model.Scribbles,
                            ScribblesJson = model.ScribblesJson,
                            KpiPlan = model.KpiPlan,
                            KpiId = model.KpiId,
                            BeginTime = bTime,
                            EndTime = eTime,
                            RemindTime = model.RemindTime,
                            RemindWay = model.RemindWay,
                            Emergency = model.Emergency,
                            Repeat = model.Repeat,
                            RepeatEndTime = model.RepeatEndTime,
                            Title = model.Title,
                            CompanyId = model.CompanyId,
                            DptId = model.DptId,
                            EmployeeId = model.EmployeeId,
                            EmployeeName = model.EmployeeName,
                            Content = model.Content,
                            FileUrl = model.FileUrl,
                            AddTime = DateTime.Now,
                            AllDay = model.AllDay
                        };
                        //无下一条重复（添加）
                        base.Add(newModel);
                    }
                }
                else if (model.Repeat == Repeat.Month)
                {
                    //按月重复
                    //查询是否有相同的数据
                    DateTime bTime = model.BeginTime.AddMonths(1);
                    DateTime eTime = model.EndTime.AddMonths(1);

                    bool has = base.Exist(p => p.Pid == model.Pid && p.BeginTime == bTime && p.EndTime == eTime);
                    int tim = DateTime.Compare(bTime, Convert.ToDateTime(model.RepeatEndTime));
                    //无重复，并且重复日程的开始时间小于或等于重复结束时间
                    if (!has && tim <= 0)
                    {
                        Schedule newModel = new Schedule()
                        {
                            Id = 0,
                            Pid = model.Pid,
                            Executors = model.Executors,
                            ExecutorsJson = model.ExecutorsJson,
                            Scribbles = model.Scribbles,
                            ScribblesJson = model.ScribblesJson,
                            KpiPlan = model.KpiPlan,
                            KpiId = model.KpiId,
                            BeginTime = bTime,
                            EndTime = eTime,
                            RemindTime = model.RemindTime,
                            RemindWay = model.RemindWay,
                            Emergency = model.Emergency,
                            Repeat = model.Repeat,
                            RepeatEndTime = model.RepeatEndTime,
                            Title = model.Title,
                            CompanyId = model.CompanyId,
                            DptId = model.DptId,
                            EmployeeId = model.EmployeeId,
                            EmployeeName = model.EmployeeName,
                            Content = model.Content,
                            FileUrl = model.FileUrl,
                            AddTime = DateTime.Now,
                            AllDay = model.AllDay
                        };
                        //无下一条重复（添加）
                        base.Add(newModel);
                    }
                }
            }
        }
        #endregion
    }
}
