using System;
using System.Collections.Generic;
using System.Text;
using XSchool.GCenter.Repositories.Extensions;
using XSchool.Repositories;

namespace XSchool.GCenter.Repositories
{
    public class NoteRepository : Repository<Model.Note>
    {
        private GCenterDbContext _dbContent;
        public NoteRepository(GCenterDbContext dbContext) : base(dbContext)
        {
            _dbContent = dbContext;
        }
        //public Model.NoteDetail GetDetail(int id)
        //{
        //    var model=from a in _dbContent.Note
        //              join b in _dbContent.AspNetUsers
        //}
    }
}
