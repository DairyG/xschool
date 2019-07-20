using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace XSchool.Helpers
{
    /// <summary>
    /// 枚举类型注释，用于获取枚举的键、值、注释
    /// </summary>
    public class EnumHelper : Attribute
    {
        public EnumHelper(string remark)
        {
            this.Remark = remark;
        }

        private string _Remark;
        public string Remark
        {
            get
            {
                return _Remark;
            }

            set
            {
                _Remark = value;
            }
            //set
            //{
            //    _Remark = LanguageUtil.GetResourceLang(value);//满足多语言切换
            //}
        }

        public static string GetRemark(Object obj)
        {
            Type type = obj.GetType();
            FieldInfo fi = type.GetField(obj.ToString());
            if (fi == null)
                return string.Empty;
            EnumHelper att = Attribute.GetCustomAttribute(fi, typeof(EnumHelper)) as EnumHelper;
            return att.Remark;
        }

        /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
        /// <returns>键值对</returns>
        public static Dictionary<int, string> GetEnumValueNameCollection(Type enumType)
        {
            Dictionary<int, string> param = new Dictionary<int, string>();
            Array array = Enum.GetValues(enumType);
            foreach (int i in array)
            {
                param.Add(i, Enum.GetName(enumType, i));
            }
            return param;
        }


        /// <summary>
        /// 获取值，名称、及备注
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<ValueNameText> GetEnumValueNameTextCollection(Type enumType)
        {
            List<ValueNameText> list = new List<ValueNameText>();
            Array array = Enum.GetValues(enumType);
            foreach (object o in array)
            {
                ValueNameText vnt = new ValueNameText(Convert.ToInt32(o), Enum.GetName(enumType, o), EnumHelper.GetRemark(o));
                list.Add(vnt);


            }
            return list;
        }

        /// <summary>
        /// 获取枚举值的文本信息
        /// </summary>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetEnumText(int status, Type type)
        {
            List<ValueNameText> list = EnumHelper.GetEnumValueNameTextCollection(type);
            for (int i = 0; i < list.Count; i++)
            {
                if (status + "" == list[i].Value + "")
                {
                    return list[i].Text;
                }
            }
            return "";
        }
    }
    /// <summary>
    /// 值名称备注
    /// </summary>
    [Serializable]
    public class ValueNameText
    {
        public object Value { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public ValueNameText(object obj, string name, string memo)
        {
            this.Value = obj;
            this.Name = name;
            this.Text = memo;
        }
    }
}
