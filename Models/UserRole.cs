using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace shop.Models;

public class UserRole : IdentityUserRole<string>
{
    [NotMapped]
    public virtual ApiUser User { get; set; }
    [NotMapped]
    public virtual Roles Role { get; set; }
}