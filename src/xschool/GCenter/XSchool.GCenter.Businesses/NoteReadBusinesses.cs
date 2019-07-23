using System;
using System.Collections.Generic;
using System.Text;
using XSchool.Businesses;
using XSchool.GCenter.Repositories;

namespace XSchool.GCenter.Businesses
{
    public class NoteReadBusinesses: Business<Model.NoteRead>
    {
        private readonly NoteReadRepository _noteReadRepository;
        public NoteReadBusinesses(IServiceProvider provider, NoteReadRepository noteReadRepository):base(provider,noteReadRepository)
        {
            _noteReadRepository = noteReadRepository;
        }
    }
}
