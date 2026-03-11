using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SporKulubu.Models;

public class Uye
{
    public int Id { get; set; }
    
    // ========== KİŞİSEL BİLGİLER ==========
    
    [Display(Name = "Ad Soyad")]
    [Required(ErrorMessage = "Ad Soyad zorunludur")]
    public string AdSoyad { get; set; } = string.Empty;
    
    [Display(Name = "Telefon")]
    public string Telefon { get; set; } = string.Empty;
    
    [Display(Name = "Doğum Tarihi")]
    [DataType(DataType.Date)]
    public DateTime DogumTarihi { get; set; }
    
    [Display(Name = "Branş")]
    public string Branş { get; set; } = string.Empty; // "Voleybol", "Basketbol", "Atletik Performans"
    
    [Display(Name = "Kayıt Tarihi")]
    [DataType(DataType.Date)]
    public DateTime KayitTarihi { get; set; }
    
    [Display(Name = "Aylık Aidat")]
    [DataType(DataType.Currency)]
    public decimal AylikAidat { get; set; }
    
    [Display(Name = "Son Aidat Ödeme Tarihi")]
    [DataType(DataType.Date)]
    public DateTime? SonAidatOdemeTarihi { get; set; }
    
    // ========== VELİ BİLGİLERİ ==========
    
    [Display(Name = "Veli Ad Soyad")]
    public string VeliAdSoyad { get; set; } = string.Empty;
    
    [Display(Name = "Veli Telefon")]
    public string VeliTelefon { get; set; } = string.Empty;
    
    // ========== SPOR SALONU ==========
    
    public int? SporSalonuId { get; set; }
    public SporSalonu? SporSalonu { get; set; }
    
    // ========== BRANŞ (YENİ) ==========
    
   //public int? BransId { get; set; }
   // public Brans? Brans { get; set; }
    public int? BransId { get; set; }
public Brans? Brans { get; set; }
    // ========== TAKIM ==========
    
    public int? TakimId { get; set; }
    public Takim? Takim { get; set; }
    
    // ========== GRUP ==========
    
    public int? GrupId { get; set; }
    public Grup? Grup { get; set; }
    
    // ========== KIYAFET BİLGİLERİ ==========
    
    [Display(Name = "Kıyafet Verildi mi?")]
    public bool KiyafetVerildiMi { get; set; }
    
    [Display(Name = "Kıyafet Veriliş Tarihi")]
    [DataType(DataType.Date)]
    public DateTime? KiyafetVerilisTarihi { get; set; }
    
    [Display(Name = "Kıyafet Notları")]
    public string? KiyafetNotlari { get; set; }
    
    // ========== AİDATLAR ==========
    
    public ICollection<Aidat>? Aidatlar { get; set; }
    
    [NotMapped]
    public decimal ToplamBorc => Aidatlar?.Where(a => !a.OdendiMi).Sum(a => a.Tutar) ?? 0;
}