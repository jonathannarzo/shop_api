using System.ComponentModel.DataAnnotations;

namespace shop.Models;

public class RoleDTO
{
    [Required]
    public string Name { get; set; }
}
