using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Models;

public class Products : Auditable
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string Manufacturer { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd}")]
    public DateTime ExpiryDate { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public Products()
    {

    }
}