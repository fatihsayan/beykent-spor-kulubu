using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKulubu.Data;
using SporKulubu.Models;

namespace SporKulubu.Controllers;

public class VeriController : Controller
{
    private readonly UygulamaDbContext _context;

    public VeriController(UygulamaDbContext context)
    {
        _context = context;
    }

    // ========== 1. TEMEL VERİLERİ YÜKLE (Tüm salonlar için Voleybol) ==========
    public async Task<IActionResult> OrnekVerileriYukle()
    {
        List<string> mesajlar = new List<string>();

        try
        {
            // 1.1 SPOR SALONLARI
            if (!_context.SporSalonlari.Any())
            {
                var salonlar = new List<SporSalonu>
                {
                    new SporSalonu { Ad = "Emlak Konut Spor Salonu" },
                    new SporSalonu { Ad = "Yakuplu For Life Spor Salonu" },
                    new SporSalonu { Ad = "Neşe Sever Spor Salonu" }
                };
                await _context.SporSalonlari.AddRangeAsync(salonlar);
                await _context.SaveChangesAsync();
                mesajlar.Add("3 spor salonu eklendi.");
            }

            // 1.2 TÜM SALONLARA VOLEYBOL BRANŞI EKLE
            var salonList = await _context.SporSalonlari.ToListAsync();
            foreach (var salon in salonList)
            {
                if (!_context.Branslar.Any(b => b.Ad == "Voleybol" && b.SporSalonuId == salon.Id))
                {
                    var voleybol = new Brans
                    {
                        Ad = "Voleybol",
                        TakimVarMi = true,
                        GrupVarMi = true,
                        SporSalonuId = salon.Id
                    };
                    _context.Branslar.Add(voleybol);
                }
            }
            await _context.SaveChangesAsync();
            mesajlar.Add("Voleybol branşları eklendi.");

            return Content("✅ " + string.Join(" | ", mesajlar));
        }
        catch (Exception ex)
        {
            return Content("❌ Hata: " + ex.Message);
        }
    }

    // ========== 2. VOLEYBOL TAKIMLARINI VE GRUPLARINI EKLE ==========
    public async Task<IActionResult> VoleybolTakimlariniEkle()
    {
        List<string> mesajlar = new List<string>();

        try
        {
            var voleybolBranslari = await _context.Branslar
                .Include(b => b.SporSalonu)
                .Where(b => b.Ad == "Voleybol")
                .ToListAsync();

            foreach (var brans in voleybolBranslari)
            {
                // Voleybol takımları yoksa ekle
                if (!_context.Takimlar.Any(t => t.BransId == brans.Id))
                {
                    var takimlar = new List<Takim>
                    {
                        new Takim { Ad = "Mini Takım", SporSalonuId = brans.SporSalonuId, BransId = brans.Id },
                        new Takim { Ad = "Midi Takım", SporSalonuId = brans.SporSalonuId, BransId = brans.Id },
                        new Takim { Ad = "Küçük Takım", SporSalonuId = brans.SporSalonuId, BransId = brans.Id },
                        new Takim { Ad = "Yıldız Takım", SporSalonuId = brans.SporSalonuId, BransId = brans.Id }
                    };
                    await _context.Takimlar.AddRangeAsync(takimlar);
                    await _context.SaveChangesAsync();
                    
                    mesajlar.Add($"{brans.SporSalonu?.Ad} için Voleybol takımları eklendi.");

                    // Grupları ekle
                    var yeniTakimlar = await _context.Takimlar
                        .Where(t => t.BransId == brans.Id)
                        .ToListAsync();
                    
                    string[] grupHarfleri = { "A", "B", "C", "D", "E" };
                    foreach (var takim in yeniTakimlar)
                    {
                        foreach (var harf in grupHarfleri)
                        {
                            _context.Gruplar.Add(new Grup 
                            { 
                                Ad = $"{harf} Grubu", 
                                TakimId = takim.Id 
                            });
                        }
                    }
                    await _context.SaveChangesAsync();
                }
            }

            if (mesajlar.Any())
                return Content("✅ " + string.Join(" | ", mesajlar));
            else
                return Content("ℹ️ Voleybol takımları zaten mevcut.");
        }
        catch (Exception ex)
        {
            return Content("❌ Hata: " + ex.Message);
        }
    }

    // ========== 3. YAKUPLU'YA ÖZEL BASKETBOL VE ATLETİK PERFORMANS EKLE ==========
    public async Task<IActionResult> YakupluBransEkle()
    {
        List<string> mesajlar = new List<string>();

        try
        {
            // 3.1 YAKUPLU SPOR SALONUNU BUL
            var yakuplu = await _context.SporSalonlari.FirstOrDefaultAsync(s => s.Ad.Contains("Yakuplu"));
            if (yakuplu == null)
            {
                return Content("❌ Yakuplu Spor Salonu bulunamadı! Önce OrnekVerileriYukle çalıştırın.");
            }

            // ===== 3.2 BASKETBOL BRANŞI =====
            if (!_context.Branslar.Any(b => b.Ad == "Basketbol" && b.SporSalonuId == yakuplu.Id))
            {
                var basketbol = new Brans
                {
                    Ad = "Basketbol",
                    TakimVarMi = true,
                    GrupVarMi = true,
                    SporSalonuId = yakuplu.Id
                };
                _context.Branslar.Add(basketbol);
                await _context.SaveChangesAsync();
                mesajlar.Add("Basketbol branşı eklendi.");

                // 3.2.1 BASKETBOL TAKIMLARI (U8, U10, U12, U14)
                var basketbolTakimlari = new List<Takim>
                {
                    new Takim { Ad = "U8 Takım", SporSalonuId = yakuplu.Id, BransId = basketbol.Id },
                    new Takim { Ad = "U10 Takım", SporSalonuId = yakuplu.Id, BransId = basketbol.Id },
                    new Takim { Ad = "U12 Takım", SporSalonuId = yakuplu.Id, BransId = basketbol.Id },
                    new Takim { Ad = "U14 Takım", SporSalonuId = yakuplu.Id, BransId = basketbol.Id }
                };
                await _context.Takimlar.AddRangeAsync(basketbolTakimlari);
                await _context.SaveChangesAsync();
                mesajlar.Add("4 basketbol takımı eklendi.");

                // 3.2.2 BASKETBOL GRUPLARI (A,B,C,D,E)
                string[] grupHarfleri = { "A", "B", "C", "D", "E" };
                foreach (var takim in basketbolTakimlari)
                {
                    foreach (var harf in grupHarfleri)
                    {
                        _context.Gruplar.Add(new Grup 
                        { 
                            Ad = $"{harf} Grubu", 
                            TakimId = takim.Id 
                        });
                    }
                }
                await _context.SaveChangesAsync();
                mesajlar.Add("20 basketbol grubu eklendi.");
            }
            else
            {
                mesajlar.Add("Basketbol branşı zaten mevcut.");
            }

            // ===== 3.3 ATLETİK PERFORMANS BRANŞI =====
            if (!_context.Branslar.Any(b => b.Ad == "Atletik Performans" && b.SporSalonuId == yakuplu.Id))
            {
                var atletik = new Brans
                {
                    Ad = "Atletik Performans",
                    TakimVarMi = false,
                    GrupVarMi = false,
                    SporSalonuId = yakuplu.Id
                };
                _context.Branslar.Add(atletik);
                await _context.SaveChangesAsync();
                mesajlar.Add("Atletik Performans branşı eklendi.");
            }
            else
            {
                mesajlar.Add("Atletik Performans branşı zaten mevcut.");
            }

            return Content("✅ " + string.Join(" | ", mesajlar));
        }
        catch (Exception ex)
        {
            return Content("❌ Hata: " + ex.Message);
        }
    }

    // ========== 4. TÜM VERİLERİ SİL ==========
    public async Task<IActionResult> TumVerileriSil()
    {
        try
        {
            _context.AntrenorTakimlar.RemoveRange(_context.AntrenorTakimlar);
            _context.Antrenorler.RemoveRange(_context.Antrenorler);
            _context.Yoklamalar.RemoveRange(_context.Yoklamalar);
            _context.AntrenmanProgramlari.RemoveRange(_context.AntrenmanProgramlari);
            _context.Aidatlar.RemoveRange(_context.Aidatlar);
            _context.Uyeler.RemoveRange(_context.Uyeler);
            _context.Gruplar.RemoveRange(_context.Gruplar);
            _context.Takimlar.RemoveRange(_context.Takimlar);
            _context.Branslar.RemoveRange(_context.Branslar);
            _context.SporSalonlari.RemoveRange(_context.SporSalonlari);
            
            await _context.SaveChangesAsync();
            
            return Content("✅ Tüm veriler silindi. Sistem sıfırlandı.");
        }
        catch (Exception ex)
        {
            return Content("❌ Hata: " + ex.Message);
        }
    }

    // ========== 5. BRANŞLARI GÖSTER (Kontrol için) ==========
    public async Task<IActionResult> BranslariGoster()
    {
        var branslar = await _context.Branslar
            .Include(b => b.SporSalonu)
            .ToListAsync();

        string result = "📋 BRANŞ LİSTESİ:\n\n";
        foreach (var b in branslar)
        {
            result += $"ID: {b.Id} | {b.Ad} | Salon: {b.SporSalonu?.Ad} | TakımVar: {b.TakimVarMi} | GrupVar: {b.GrupVarMi}\n";
        }

        return Content(result);
    }

    // ========== 6. TAKIMLARI GÖSTER (Kontrol için) ==========
    public async Task<IActionResult> TakimlariGoster()
    {
        var takimlar = await _context.Takimlar
            .Include(t => t.Brans)
            .Include(t => t.SporSalonu)
            .ToListAsync();

        string result = "📋 TAKIM LİSTESİ:\n\n";
        foreach (var t in takimlar)
        {
            result += $"ID: {t.Id} | {t.Ad} | Branş: {t.Brans?.Ad} | Salon: {t.SporSalonu?.Ad}\n";
        }

        return Content(result);
    }
}