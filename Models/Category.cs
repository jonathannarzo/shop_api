using System.ComponentModel.DataAnnotations;

namespace shop.Models;

public class Category : Auditable
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public ICollection<Products>? Products { get; }
    public Category()
    {

    }
}