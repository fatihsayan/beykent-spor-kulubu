using System.ComponentModel.DataAnnotations;

namespace SporKulubu.Models;

public class AntrenmanProgrami
{
    public int Id { get; set; }
    
    public int TakimId { get; set; }
    public Takim? Takim { get; set; }
    
    public int? GrupId { get; set; }
    public Grup? Grup { get; set; }
    
    [Display(Name = "Gün")]
    public string Gun { get; set; } = string.Empty; // "Pazartesi", "Salı", ...
    
    [Display(Name = "Başlangıç Saati")]
    [DataType(DataType.Time)]
    public TimeSpan Baslangic { get; set; }
    
    [Display(Name = "Bitiş Saati")]
    [DataType(DataType.Time)]
    public TimeSpan Bitis { get; set; }
    
    [Display(Name = "Salon")]
    public string Salon { get; set; } = string.Empty;
}