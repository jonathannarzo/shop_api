using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace shop.Models;

public class ApiUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [NotMapped]
    public virtual ICollection<UserRole> UserRoles { get; set; }
}