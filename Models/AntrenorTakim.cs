using System.ComponentModel.DataAnnotations;

namespace SporKulubu.Models;

public class AntrenorTakim
{
    public int Id { get; set; }
    
    public int AntrenorId { get; set; }
    public Antrenor? Antrenor { get; set; }
    
    // Hangi salondan sorumlu? (YENİ)
    public int? SporSalonuId { get; set; }
    public SporSalonu? SporSalonu { get; set; }
    
    // Hangi takımdan sorumlu?
    public int? TakimId { get; set; }
    public Takim? Takim { get; set; }
    
    // Hangi gruptan sorumlu?
    public int? GrupId { get; set; }
    public Grup? Grup { get; set; }
    
    [Display(Name = "Atanma Tarihi")]
    public DateTime AtanmaTarihi { get; set; }
}