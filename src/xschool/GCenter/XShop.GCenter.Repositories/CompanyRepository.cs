using System;
using Microsoft.EntityFrameworkCore;
using XSchool.Repositories;
using XShop.GCenter.Model;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using XSchool.Query.Pageing;
using XShop.GCenter.Repositories.Extensions;

namespace XShop.GCenter.Repositories
{
    public class CompanyRepository : Repository<Company>
    {
        public CompanyRepository(GCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}
