using System.ComponentModel.DataAnnotations;

namespace SporKulubu.Models;

public class Aidat
{
    public int Id { get; set; }
    
    public int UyeId { get; set; }
    public Uye? Uye { get; set; }
    
    [Display(Name = "Ödeme Tarihi")]
    [DataType(DataType.Date)]
    public DateTime OdemeTarihi { get; set; }
    
    [Display(Name = "Tutar")]
    [DataType(DataType.Currency)]
    public decimal Tutar { get; set; }
    
    [Display(Name = "Dönem")]
    public string? Ay { get; set; } // "Ocak 2024", "Şubat 2024"
    
    [Display(Name = "Ödendi mi?")]
    public bool OdendiMi { get; set; }
    
    [Display(Name = "Açıklama")]
    public string? Aciklama { get; set; }
}