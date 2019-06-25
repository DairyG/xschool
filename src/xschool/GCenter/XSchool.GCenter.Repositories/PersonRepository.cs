using XSchool.GCenter.Model;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;
using System.Linq;
using XSchool.GCenter.Model.ViewModel;

namespace XSchool.GCenter.Repositories
{
    public class PersonRepository : Repository<Person>
    {
        private GCenterDbContext _dbContext;
        public PersonRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public PersonDto GetPerson(int id)
        {
            return (from p in _dbContext.Person
                    join dpt in _dbContext.Department on p.DepartmentId equals dpt.Id into rpd
                    from pd in rpd.DefaultIfEmpty()
                    join job in _dbContext.PositionSetting on p.PositionId equals job.Id into rpj
                    from pj in rpj.DefaultIfEmpty()
                    where p.Id == id
                    select new PersonDto
                    {
                        Id = p.Id,
                        CompanyId = p.CompanyId,
                        UserName = p.UserName,
                        PinYinName = p.PinYinName,
                        EnglishName = p.EnglishName,
                        Gender = p.Gender,
                        LinkPhone = p.LinkPhone,
                        Email = p.Email,
                        Qq = p.Qq,
                        Folk = p.Folk,
                        NativePlace = p.NativePlace,
                        PoliticalStatus = p.PoliticalStatus,
                        IdCard = p.IdCard,
                        Age = p.Age,
                        BirthDay = p.BirthDay,
                        GraduateInstitutions = p.GraduateInstitutions,
                        Professional = p.Professional,
                        Degree = p.Degree,
                        GraduationDate = p.GraduationDate,
                        Stature = p.Stature,
                        Weight = p.Weight,
                        Marriage = p.Marriage,
                        Children = p.Children,
                        RecruitSource = p.RecruitSource,
                        JobCandidates = p.JobCandidates,
                        ExpectSalary = p.ExpectSalary,
                        ArrivalTime = p.ArrivalTime,
                        IdCardProvince = p.IdCardProvince,
                        IdCardCity = p.IdCardCity,
                        IdCardCounty = p.IdCardCounty,
                        IdCardArea = p.IdCardArea,
                        IdCardAddress = p.IdCardAddress,
                        LiveProvince = p.LiveProvince,
                        LiveCity = p.LiveCity,
                        LiveCounty = p.LiveCounty,
                        LiveArea = p.LiveArea,
                        LiveAddress = p.LiveAddress,
                        Hobby = p.Hobby,
                        PhotoPath = p.PhotoPath,
                        CertificatePath = p.CertificatePath,
                        Family = p.Family,
                        Education = p.Education,
                        Work = p.Work,
                        DepartmentId = p.DepartmentId,
                        PositionId = p.PositionId,
                        EmployeeNo = p.EmployeeNo,
                        OfficePhone = p.OfficePhone,
                        OfficeEmail = p.OfficeEmail,
                        FaxNumber = p.FaxNumber,
                        Referees = p.Referees,
                        OfficeAddress = p.OfficeAddress,
                        PositionDescribe = p.PositionDescribe,
                        Status = p.Status,
                        DepartmentName = pd.DptName,
                        PositionName = pj.Name
                    }).FirstOrDefault();
        }
    }
}
