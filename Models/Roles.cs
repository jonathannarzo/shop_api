using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace shop.Models;

public class Roles : IdentityRole
{
    [NotMapped]
    public virtual ICollection<UserRole> UserRoles { get; set; }
}