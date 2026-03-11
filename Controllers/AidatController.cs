using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKulubu.Data;
using SporKulubu.Models;

namespace SporKulubu.Controllers;

public class AidatController : Controller
{
    private readonly UygulamaDbContext _context;

    public AidatController(UygulamaDbContext context)
    {
        _context = context;
    }

    // Üyenin aidat geçmişi
    public async Task<IActionResult> Gecmis(int uyeId)
    {
        var uye = await _context.Uyeler.FindAsync(uyeId);
        if (uye == null) return NotFound();

        var aidatlar = await _context.Aidatlar
            .Where(a => a.UyeId == uyeId)
            .OrderByDescending(a => a.OdemeTarihi)
            .ToListAsync();

        ViewBag.UyeAdi = uye.AdSoyad;
        ViewBag.UyeId = uyeId;
        ViewBag.AylikAidat = uye.AylikAidat;
        ViewBag.ToplamBorc = uye.ToplamBorc;

        return View(aidatlar);
    }

    // Yeni aidat ekleme sayfası
    public IActionResult Ekle(int uyeId)
    {
        var uye = _context.Uyeler.Find(uyeId);
        if (uye == null) return NotFound();

        ViewBag.UyeId = uyeId;
        ViewBag.UyeAdi = uye.AdSoyad;
        return View();
    }

    // Yeni aidat ekleme işlemi
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(Aidat aidat)
    {
        if (ModelState.IsValid)
        {
            _context.Add(aidat);
            await _context.SaveChangesAsync();
            
            // Üyenin son ödeme tarihini güncelle
            var uye = await _context.Uyeler.FindAsync(aidat.UyeId);
            if (uye != null)
            {
                uye.SonAidatOdemeTarihi = aidat.OdemeTarihi;
                await _context.SaveChangesAsync();
            }
            
            TempData["Basarili"] = "Aidat ödemesi başarıyla eklendi!";
            return RedirectToAction("Gecmis", new { uyeId = aidat.UyeId });
        }
        return View(aidat);
    }
}