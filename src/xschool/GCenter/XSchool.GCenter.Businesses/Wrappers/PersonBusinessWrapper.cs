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

        /// <summary>
        /// 添加/修改
        /// </summary>
        public Result AddOrEdit(PersonOperation operation, Person model)
        {
            var result = _personBusiness.CheckBasic(model);
            if (!result.Succeed)
            {
                return result;
            }
            if (operation == PersonOperation.PositionInfo)
            {
                result = _personBusiness.CheckPosition(model);
            }
            return result.Succeed ? _personBusiness.AddOrEdit(model) : result;
        }

    }
}
