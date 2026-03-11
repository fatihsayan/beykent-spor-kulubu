using System.ComponentModel.DataAnnotations;

namespace SporKulubu.Models;

public class SporSalonu
{
    public int Id { get; set; }
    
    [Display(Name = "Salon Adı")]
    public string Ad { get; set; } = string.Empty;
    
    [Display(Name = "Adres")]
    public string? Adres { get; set; }
    
    [Display(Name = "Telefon")]
    public string? Telefon { get; set; }
    
    public ICollection<Takim>? Takimlar { get; set; }
}