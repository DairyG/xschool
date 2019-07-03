using System.Collections.Generic;
using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;
using XSchool.Query.Pageing;

namespace XSchool.GCenter.Repositories
{
    public class ResumeRepository : Repository<Resume>
    {
        private GCenterDbContext _dbContext;
        public ResumeRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int Delete(Resume model, int id)
        {
            Resume resume = new Resume();
            resume.Id = id;
            using (_dbContext)
            {
                _dbContext.Resume.Attach(resume);
                resume.Status = ResumeStatus.Invalid;
                return _dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// 根据ID修改Resume的面试状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateInterviewStatus(Resume model, int id,InterviewStatus state)
        {
            Resume resume = new Resume();
            resume.Id = id;
            using (_dbContext)
            {
                _dbContext.Resume.Attach(resume);
                resume.InterviewStatus = state;
                return _dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// 根据面试状态查询简历
        /// </summary>
        /// <param name="state">面试状态</param>
        /// <returns></returns>
        public IPageCollection<Resume> GetListByInterviewStatus(int page, int limit, InterviewStatus state)
        {
            var query = (from r in _dbContext.Resume
                         join rr in _dbContext.ResumeRecord on r.Id equals rr.ResumeId
                         orderby r.Id descending
                         where rr.InterviewStatus == state
                         select r);
            return query.Page(page, limit);
        }
    }
}
