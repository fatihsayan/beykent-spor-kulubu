using System.ComponentModel.DataAnnotations;

namespace SporKulubu.Models;

public class Grup
{
    public int Id { get; set; }
    
    [Display(Name = "Grup Adı")]
    public string Ad { get; set; } = string.Empty; // A, B, C, D, E Grubu
    
    public int TakimId { get; set; }
    public Takim? Takim { get; set; }
    
    [Display(Name = "Kontenjan")]
    public int? Kontenjan { get; set; }
}