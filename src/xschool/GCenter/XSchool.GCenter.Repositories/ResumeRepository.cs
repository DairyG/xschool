using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class ResumeRepository : Repository<Resume>
    {
        private GCenterDbContext _dbContext;
        public ResumeRepository(GCenterDbContext dbContext) : base(dbContext) {
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
    }
}
