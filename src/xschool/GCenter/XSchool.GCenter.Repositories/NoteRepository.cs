using System;
using System.Collections.Generic;
using System.Text;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class NoteRepository : Repository<Model.Note>
    {
        public NoteRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}
