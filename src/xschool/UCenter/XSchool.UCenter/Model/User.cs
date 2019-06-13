using Microsoft.AspNetCore.Identity;

namespace XSchool.UCenter.Model
{
    public enum State
    {
        Enable = 1,

        Disabled = 2
    }

    public class User: IdentityUser<int>
    {
        /// <summary>
        /// 用户状态
        /// </summary>
        public virtual State State { get; set; }

        /// <summary>
        /// 用户身份证
        /// </summary>
        public string IdCard { get; set; }

    }
}
