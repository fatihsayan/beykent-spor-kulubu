using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKulubu.Data;
using SporKulubu.Models;

namespace SporKulubu.Controllers;

public class HomeController : Controller
{
    private readonly UygulamaDbContext _context;

    public HomeController(UygulamaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // İstatistikler
        ViewBag.ToplamUye = await _context.Uyeler.CountAsync();
        ViewBag.ToplamSalon = await _context.SporSalonlari.CountAsync();
        ViewBag.ToplamTakim = await _context.Takimlar.CountAsync();
        ViewBag.BorcluUye = await _context.Uyeler
            .Include(u => u.Aidatlar)
            .CountAsync(u => u.Aidatlar != null && u.Aidatlar.Any(a => !a.OdendiMi));

        // Son eklenen 5 üye
        ViewBag.SonUyeler = await _context.Uyeler
            .Include(u => u.SporSalonu)
            .OrderByDescending(u => u.KayitTarihi)
            .Take(5)
            .ToListAsync();

        // SPOR SALONLARI - BURASI ÖNEMLİ!
        ViewBag.SporSalonlari = await _context.SporSalonlari.ToListAsync();

        return View();
    }
    public async Task<IActionResult> AntrenorEkle()
{
    if (!_context.Antrenorler.Any())
    {
        var antrenorler = new List<Antrenor>
        {
            new Antrenor 
            { 
                AdSoyad = "Burhan Şayan", 
                Email = "burhan@beykentspor.com", 
                Telefon = "0532 111 22 33",
                Uzmanlik = "Tüm Takımlar",
                Sifre = "123456",
                KayitTarihi = DateTime.Now
            },
            new Antrenor 
            { 
                AdSoyad = "Ertan Tuncel", 
                Email = "ertan@beykentspor.com", 
                Telefon = "0533 444 55 66",
                Uzmanlik = "Tüm Takımlar",
                Sifre = "123456",
                KayitTarihi = DateTime.Now
            }
        };

        await _context.Antrenorler.AddRangeAsync(antrenorler);
        await _context.SaveChangesAsync();
        return Content("✅ Burhan ve Ertan eklendi! Giriş yapabilirsiniz.");
    }
    return Content("ℹ️ Antrenörler zaten mevcut.");
}
}