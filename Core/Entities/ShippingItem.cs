using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;

public class ShippingItem
{
    public int Id { get; set; }
    [Required]
    public string? Image { get; set; }
    [Required,MinLength(2)]
    public string Title { get; set; } =null!;
    public string? Description { get; set; }
}
