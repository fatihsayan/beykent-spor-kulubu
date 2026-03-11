using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SporKulubu.Data;
using SporKulubu.Models;

namespace SporKulubu.Controllers;

public class UyelerController : Controller
{
    private readonly UygulamaDbContext _context;

    public UyelerController(UygulamaDbContext context)
    {
        _context = context;
    }

    // ========== ANA SAYFA - Tüm üyeleri listele (Filtrelemeli) ==========
    public async Task<IActionResult> Index(int? salonId, int? bransId, int? takimId, int? grupId)
    {
        try
        {
            var query = _context.Uyeler
                .Include(u => u.SporSalonu)
                .Include(u => u.Brans)
                .Include(u => u.Takim)
                .Include(u => u.Grup)
                .Include(u => u.Aidatlar)
                .AsQueryable();

            // Filtreleme
            if (salonId.HasValue && salonId > 0)
                query = query.Where(u => u.SporSalonuId == salonId);
            
            if (bransId.HasValue && bransId > 0)
                query = query.Where(u => u.BransId == bransId);
            
            if (takimId.HasValue && takimId > 0)
                query = query.Where(u => u.TakimId == takimId);
            
            if (grupId.HasValue && grupId > 0)
                query = query.Where(u => u.GrupId == grupId);

            var uyeler = await query.OrderBy(u => u.AdSoyad).ToListAsync();

            // Filtre listeleri
            ViewBag.SporSalonlari = new SelectList(await _context.SporSalonlari.ToListAsync(), "Id", "Ad", salonId);
            
            if (salonId.HasValue && salonId > 0)
            {
                ViewBag.Branslar = new SelectList(
                    await _context.Branslar.Where(b => b.SporSalonuId == salonId).ToListAsync(),
                    "Id",
                    "Ad",
                    bransId
                );
            }
            
            if (bransId.HasValue && bransId > 0)
            {
                ViewBag.Takimlar = new SelectList(
                    await _context.Takimlar.Where(t => t.BransId == bransId).ToListAsync(),
                    "Id",
                    "Ad",
                    takimId
                );
            }
            
            if (takimId.HasValue && takimId > 0)
            {
                ViewBag.Gruplar = new SelectList(
                    await _context.Gruplar.Where(g => g.TakimId == takimId).ToListAsync(),
                    "Id",
                    "Ad",
                    grupId
                );
            }

            ViewBag.SecilenSalonId = salonId;
            ViewBag.SecilenBransId = bransId;
            ViewBag.SecilenTakimId = takimId;
            ViewBag.SecilenGrupId = grupId;
            ViewBag.ToplamAidat = uyeler.Sum(u => u.AylikAidat);

            return View(uyeler);
        }
        catch (Exception ex)
        {
            TempData["Hata"] = "Bir hata oluştu: " + ex.Message;
            return View(new List<Uye>());
        }
    }

    // ========== YENİ ÜYE EKLE (GET) ==========
    public async Task<IActionResult> Ekle()
    {
        ViewBag.SporSalonlari = new SelectList(await _context.SporSalonlari.OrderBy(s => s.Ad).ToListAsync(), "Id", "Ad");
        return View();
    }

    // ========== YENİ ÜYE KAYDET (POST) ==========
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(Uye uye)
    {
        if (ModelState.IsValid)
        {
            try
            {
                uye.KayitTarihi = DateTime.Now;
                _context.Add(uye);
                await _context.SaveChangesAsync();
                TempData["Basarili"] = "Üye başarıyla eklendi!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu: " + ex.Message);
            }
        }
        
        ViewBag.SporSalonlari = new SelectList(await _context.SporSalonlari.ToListAsync(), "Id", "Ad", uye.SporSalonuId);
        return View(uye);
    }

    // ========== AJAX: Branşları Getir ==========
    [HttpGet]
    public async Task<IActionResult> BranslariGetir(int salonId)
    {
        try
        {
            var branslar = await _context.Branslar
                .Where(b => b.SporSalonuId == salonId)
                .OrderBy(b => b.Ad)
                .Select(b => new { 
                    b.Id, 
                    b.Ad, 
                    b.TakimVarMi, 
                    b.GrupVarMi 
                })
                .ToListAsync();
            return Json(branslar);
        }
        catch
        {
            return Json(new List<object>());
        }
    }

    // ========== AJAX: Takımları Getir (Branş'a göre) ==========
    [HttpGet]
    public async Task<IActionResult> TakimlariGetir(int bransId)
    {
        try
        {
            var takimlar = await _context.Takimlar
                .Where(t => t.BransId == bransId)
                .OrderBy(t => t.Ad)
                .Select(t => new { t.Id, t.Ad })
                .ToListAsync();
            return Json(takimlar);
        }
        catch
        {
            return Json(new List<object>());
        }
    }

    // ========== AJAX: Grupları Getir ==========
    [HttpGet]
    public async Task<IActionResult> GruplariGetir(int takimId)
    {
        try
        {
            var gruplar = await _context.Gruplar
                .Where(g => g.TakimId == takimId)
                .OrderBy(g => g.Ad)
                .Select(g => new { g.Id, g.Ad })
                .ToListAsync();
            return Json(gruplar);
        }
        catch
        {
            return Json(new List<object>());
        }
    }

    // ========== ÜYE DETAY ==========
    public async Task<IActionResult> Detay(int id)
    {
        try
        {
            var uye = await _context.Uyeler
                .Include(u => u.SporSalonu)
                .Include(u => u.Brans)
                .Include(u => u.Takim)
                .Include(u => u.Grup)
                .Include(u => u.Aidatlar)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (uye == null)
                return NotFound();

            return View(uye);
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }

    // ========== ÜYE DÜZENLE (GET) ==========
    public async Task<IActionResult> Duzenle(int id)
    {
        try
        {
            var uye = await _context.Uyeler.FindAsync(id);
            if (uye == null)
                return NotFound();

            ViewBag.SporSalonlari = new SelectList(await _context.SporSalonlari.ToListAsync(), "Id", "Ad", uye.SporSalonuId);
            
            if (uye.SporSalonuId.HasValue)
            {
                ViewBag.Branslar = new SelectList(
                    await _context.Branslar.Where(b => b.SporSalonuId == uye.SporSalonuId).ToListAsync(),
                    "Id",
                    "Ad",
                    uye.BransId
                );
            }
            
            if (uye.BransId.HasValue)
            {
                ViewBag.Takimlar = new SelectList(
                    await _context.Takimlar.Where(t => t.BransId == uye.BransId).ToListAsync(),
                    "Id",
                    "Ad",
                    uye.TakimId
                );
            }
            
            if (uye.TakimId.HasValue)
            {
                ViewBag.Gruplar = new SelectList(
                    await _context.Gruplar.Where(g => g.TakimId == uye.TakimId).ToListAsync(),
                    "Id",
                    "Ad",
                    uye.GrupId
                );
            }

            return View(uye);
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }

    // ========== ÜYE DÜZENLE (POST) ==========
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Duzenle(int id, Uye uye)
    {
        if (id != uye.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(uye);
                await _context.SaveChangesAsync();
                TempData["Basarili"] = "Üye başarıyla güncellendi!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Uyeler.AnyAsync(u => u.Id == uye.Id))
                    return NotFound();
                throw;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
            }
        }
        
        ViewBag.SporSalonlari = new SelectList(await _context.SporSalonlari.ToListAsync(), "Id", "Ad", uye.SporSalonuId);
        return View(uye);
    }

    // ========== ÜYE SİL (GET) ==========
    public async Task<IActionResult> Sil(int id)
    {
        try
        {
            var uye = await _context.Uyeler
                .Include(u => u.SporSalonu)
                .Include(u => u.Brans)
                .Include(u => u.Takim)
                .Include(u => u.Grup)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (uye == null)
                return NotFound();

            return View(uye);
        }
        catch
        {
            return RedirectToAction(nameof(Index));
        }
    }

    // ========== ÜYE SİL (POST) ==========
    [HttpPost, ActionName("Sil")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SilOnay(int id)
    {
        try
        {
            var uye = await _context.Uyeler.FindAsync(id);
            if (uye != null)
            {
                _context.Uyeler.Remove(uye);
                await _context.SaveChangesAsync();
                TempData["Basarili"] = "Üye başarıyla silindi!";
            }
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Hata"] = "Silme sırasında bir hata oluştu: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    // ========== BORÇLU ÜYELER ==========
    public async Task<IActionResult> BorcluUyeler()
    {
        try
        {
            var uyeler = await _context.Uyeler
                .Include(u => u.SporSalonu)
                .Include(u => u.Brans)
                .Include(u => u.Takim)
                .Include(u => u.Grup)
                .Include(u => u.Aidatlar)
                .ToListAsync();

            var borclular = uyeler
                .Where(u => u.Aidatlar != null && u.Aidatlar.Where(a => !a.OdendiMi).Sum(a => a.Tutar) > 0)
                .OrderByDescending(u => u.Aidatlar!.Where(a => !a.OdendiMi).Sum(a => a.Tutar))
                .ToList();

            return View(borclular);
        }
        catch
        {
            return View(new List<Uye>());
        }
    }

    // ========== TOPLU VERİ EKLEME (Kurulum için) ==========
    public async Task<IActionResult> OrnekVeriEkle()
    {
        try
        {
            List<string> mesajlar = new List<string>();

            // 1. SPOR SALONLARI
            if (!await _context.SporSalonlari.AnyAsync())
            {
                var salonlar = new List<SporSalonu>
                {
                    new SporSalonu { Ad = "Emlak Konut Spor Salonu" },
                    new SporSalonu { Ad = "Yakuplu For Life Spor Salonu" },
                    new SporSalonu { Ad = "Neşe Sever Spor Salonu" }
                };
                await _context.SporSalonlari.AddRangeAsync(salonlar);
                await _context.SaveChangesAsync();
                mesajlar.Add("3 salon eklendi.");
            }

            // 2. BRANŞLAR (Voleybol - Tüm salonlar için)
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
            return Content("❌ Hata oluştu: " + ex.Message);
        }
    }
}