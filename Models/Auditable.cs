namespace shop.Models;
using System.ComponentModel.DataAnnotations;
public abstract class Auditable
{
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    [DataType(DataType.Date)]
    public DateTimeOffset? DateCreated { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    [DataType(DataType.Date)]
    public DateTimeOffset? DateUpdated { get; set; }
}