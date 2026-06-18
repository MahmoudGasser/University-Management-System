
using Microsoft.AspNetCore.Identity;

public class AssignRoleModel
{
    public List<IdentityUser> Users { get; set; }
    public List<IdentityRole> Roles { get; set; }
}
