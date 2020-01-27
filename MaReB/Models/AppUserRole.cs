using Microsoft.AspNetCore.Identity;

namespace MaReB.Models
{
    public class AppUserRole : IdentityUserRole<string>
    {
        public string RoleAssigner { get; set; }
    }
}
