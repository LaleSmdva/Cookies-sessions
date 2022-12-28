using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class SlideItem
{
    public int Id { get; set; }
    [Required]
    public string? Photo { get; set; }
    public string? Offer { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
