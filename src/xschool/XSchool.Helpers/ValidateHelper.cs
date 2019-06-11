using System;

namespace XSchool.Helpers
{
    public class ValidateHelper
    {
        public static bool DateValidate(DateTime? start,DateTime? end) {
            if (start == null && end == null) {
                return true;
            }
            if (start == null || end == null) {
                return false;
            }
            return start < end;
        }
    }
}
