using Microsoft.EntityFrameworkCore;
using XSchool.WorkFlow.Model;

namespace XSchool.WorkFlow.Repositories.Extensions
{
    public class xschool_workflowDbContext : DbContext
    {
        public xschool_workflowDbContext(DbContextOptions<xschool_workflowDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //流程组别
            modelBuilder.Entity<SubjectType>(entry =>
            {
                entry.ToTable("SubjectType");
                entry.HasKey(m => m.Id);

            });

            //流程主表：可视范围，one=>many
            modelBuilder.Entity<SubjectRule>(entry =>
            {
                entry.ToTable("SubjectRule");
                entry.HasKey(m => m.Id);

                //流程主表：可视范围，one=>many
                entry.HasOne(m => m.SubjectObj)
               .WithMany(m=>m.SubjectRuleRangeList)
               .HasForeignKey(m => m.SubjectId);

                //流程主表：节点人员表，one=>many
                entry.HasOne(m => m.SubjectStepObj)
            .WithMany(m => m.SubjectRulePassList)
            .HasForeignKey(m => m.SubjectStepId);
            });

            //流程主表：流程节点，one=>many
            modelBuilder.Entity<SubjectStep>(entry =>
            {
                entry.ToTable("SubjectStep");
                entry.HasKey(m => m.Id);

               // entry.HasOne(m => m.SubjectObj)
               //.WithMany(m => m.SubjectStepFlowList)
               //.HasForeignKey(m => m.SubjectId);
            });

        }


    }
    
}
