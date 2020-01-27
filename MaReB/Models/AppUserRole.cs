using Microsoft.AspNetCore.Identity;

namespace MaReB.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public string RoleAssigner { get; set; }
    }
}
