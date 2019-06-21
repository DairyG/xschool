using XSchool.Businesses;
using XSchool.Core;
using XSchool.GCenter.Model;
using XSchool.GCenter.Model.ViewModel;
using XSchool.Helpers;

namespace XSchool.GCenter.Businesses.Wrappers
{
    public class PersonBusinessWrapper : BusinessWrapper
    {
        private PersonBusiness _personBusiness;
        public PersonBusinessWrapper(PersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }
    }
}
