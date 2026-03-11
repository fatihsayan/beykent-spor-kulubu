using System.ComponentModel.DataAnnotations;

namespace SporKulubu.Models;

public class Yoklama
{
    public int Id { get; set; }
    
    public int AntrenorId { get; set; }
    public Antrenor? Antrenor { get; set; }
    
    public int UyeId { get; set; }
    public Uye? Uye { get; set; }
    
    [Display(Name = "Tarih")]
    [DataType(DataType.Date)]
    public DateTime Tarih { get; set; }
    
    [Display(Name = "Durum")]
    public string Durum { get; set; } = string.Empty; // "Katıldı", "Katılmadı", "İzinli", "Raporlu"
    
    [Display(Name = "Not")]
    public string? Not { get; set; }
}