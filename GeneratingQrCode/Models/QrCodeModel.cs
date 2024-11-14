using System.ComponentModel.DataAnnotations;

namespace GeneratingQrCode.Models;

public class QrCodeModel
{
    [Display(Name = "Enter Qr Code Name")] 
    [Required(ErrorMessage = "Qr Code Name is required")]
    public string? QrCodeName { get; set; }
    
    [Display(Name = "Enter Web Url")] 
    [Required(ErrorMessage = "Web Url is required")]
    [DataType(DataType.Url)]
    
    public string? QrCodeText { get; set; }
}