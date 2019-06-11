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
        public virtual State State { get; set; }
    }
}
