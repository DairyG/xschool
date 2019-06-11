using System;
using System.ComponentModel;
using System.Reflection;

namespace XSchool.Helpers
{
    public static class EnumExtension
    {
        private static TAttribute GetEnumAttribute<TAttribute>(this Enum @enum) where TAttribute:Attribute
        {
            return @enum.GetType().GetField(@enum.ToString()).GetCustomAttribute<TAttribute>();
        }

        public static string GetDescription(this Enum @enum)
        {
            return @enum.GetEnumAttribute<DescriptionAttribute>()?.Description;
        }
    }
}
