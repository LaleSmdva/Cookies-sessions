using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.ViewModels.Slider;

public class SlideCreateVM
{
    
    [Required]
    public IFormFile? Photo { get; set; }
    public string? Offer { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
