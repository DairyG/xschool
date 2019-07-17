using System;
using System.Collections.Generic;
using System.Text;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
   public class NoteReadRangeRepository : Repository<Model.NoteReadRange>
    {
        private GCenterDbContext _dbContent;
        public NoteReadRangeRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContent = dbContext;
        }
    }
}
