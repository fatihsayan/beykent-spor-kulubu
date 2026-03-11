using Microsoft.EntityFrameworkCore;
using SporKulubu.Models;

namespace SporKulubu.Data;

public class UygulamaDbContext : DbContext
{
    public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options)
        : base(options)
    {
    }

    // ========== DbSet'ler ==========
    
    public DbSet<SporSalonu> SporSalonlari { get; set; } = null!;
    public DbSet<Brans> Branslar { get; set; } = null!;
    public DbSet<Takim> Takimlar { get; set; } = null!;
    public DbSet<Grup> Gruplar { get; set; } = null!;
    public DbSet<Uye> Uyeler { get; set; } = null!;
    public DbSet<Aidat> Aidatlar { get; set; } = null!;
    public DbSet<Antrenor> Antrenorler { get; set; } = null!;
    public DbSet<AntrenorTakim> AntrenorTakimlar { get; set; } = null!;
    public DbSet<Yoklama> Yoklamalar { get; set; } = null!;
    public DbSet<AntrenmanProgrami> AntrenmanProgramlari { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ========== İLİŞKİLER ==========

        // SporSalonu - Takim (Bir salonun birden çok takımı olabilir)
        modelBuilder.Entity<Takim>()
            .HasOne(t => t.SporSalonu)
            .WithMany(s => s.Takimlar)
            .HasForeignKey(t => t.SporSalonuId)
            .OnDelete(DeleteBehavior.Cascade);

        // Takim - Grup (Bir takımın birden çok grubu olabilir)
        modelBuilder.Entity<Grup>()
            .HasOne(g => g.Takim)
            .WithMany(t => t.Gruplar)
            .HasForeignKey(g => g.TakimId)
            .OnDelete(DeleteBehavior.Cascade);

        // Brans - SporSalonu (Bir salonun birden çok branşı olabilir)
        modelBuilder.Entity<Brans>()
            .HasOne(b => b.SporSalonu)
            .WithMany()
            .HasForeignKey(b => b.SporSalonuId)
            .OnDelete(DeleteBehavior.Cascade);

        // Takim - Brans (Bir takım bir branşa aittir)
        modelBuilder.Entity<Takim>()
            .HasOne(t => t.Brans)
            .WithMany()
            .HasForeignKey(t => t.BransId)
            .OnDelete(DeleteBehavior.SetNull);

        // Uye - SporSalonu
        modelBuilder.Entity<Uye>()
            .HasOne(u => u.SporSalonu)
            .WithMany()
            .HasForeignKey(u => u.SporSalonuId)
            .OnDelete(DeleteBehavior.SetNull);

        // Uye - Brans
        modelBuilder.Entity<Uye>()
            .HasOne(u => u.Brans)
            .WithMany()
            .HasForeignKey(u => u.BransId)
            .OnDelete(DeleteBehavior.SetNull);

        // Uye - Takim
        modelBuilder.Entity<Uye>()
            .HasOne(u => u.Takim)
            .WithMany()
            .HasForeignKey(u => u.TakimId)
            .OnDelete(DeleteBehavior.SetNull);

        // Uye - Grup
        modelBuilder.Entity<Uye>()
            .HasOne(u => u.Grup)
            .WithMany()
            .HasForeignKey(u => u.GrupId)
            .OnDelete(DeleteBehavior.SetNull);

        // Uye - Aidat
        modelBuilder.Entity<Aidat>()
            .HasOne(a => a.Uye)
            .WithMany(u => u.Aidatlar)
            .HasForeignKey(a => a.UyeId)
            .OnDelete(DeleteBehavior.Cascade);

        // ========== ANTENÖR TAKİM İLİŞKİLERİ (DÜZELTİLMİŞ) ==========
        
        // Antrenor - AntrenorTakim
        modelBuilder.Entity<AntrenorTakim>()
            .HasOne(at => at.Antrenor)
            .WithMany(a => a.AntrenorTakimlar)
            .HasForeignKey(at => at.AntrenorId)
            .OnDelete(DeleteBehavior.Cascade);

        // AntrenorTakim - SporSalonu (YENİ - DÜZGÜN TANIMLANMIŞ)
        modelBuilder.Entity<AntrenorTakim>()
            .HasOne(at => at.SporSalonu)
            .WithMany()
            .HasForeignKey(at => at.SporSalonuId)
            .OnDelete(DeleteBehavior.SetNull);

        // AntrenorTakim - Takim
        modelBuilder.Entity<AntrenorTakim>()
            .HasOne(at => at.Takim)
            .WithMany()
            .HasForeignKey(at => at.TakimId)
            .OnDelete(DeleteBehavior.SetNull);

        // AntrenorTakim - Grup
        modelBuilder.Entity<AntrenorTakim>()
            .HasOne(at => at.Grup)
            .WithMany()
            .HasForeignKey(at => at.GrupId)
            .OnDelete(DeleteBehavior.SetNull);

        // ========== YOKLAMA İLİŞKİLERİ ==========

        // Yoklama - Antrenor
        modelBuilder.Entity<Yoklama>()
            .HasOne(y => y.Antrenor)
            .WithMany()
            .HasForeignKey(y => y.AntrenorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Yoklama - Uye
        modelBuilder.Entity<Yoklama>()
            .HasOne(y => y.Uye)
            .WithMany()
            .HasForeignKey(y => y.UyeId)
            .OnDelete(DeleteBehavior.Cascade);

        // ========== ANTRENMAN PROGRAMI İLİŞKİLERİ ==========

        // AntrenmanProgrami - Takim
        modelBuilder.Entity<AntrenmanProgrami>()
            .HasOne(p => p.Takim)
            .WithMany()
            .HasForeignKey(p => p.TakimId)
            .OnDelete(DeleteBehavior.Cascade);

        // AntrenmanProgrami - Grup
        modelBuilder.Entity<AntrenmanProgrami>()
            .HasOne(p => p.Grup)
            .WithMany()
            .HasForeignKey(p => p.GrupId)
            .OnDelete(DeleteBehavior.SetNull);

        // ========== VARSAYILAN DEĞERLER ==========

        // Kayıt tarihi otomatik
        modelBuilder.Entity<Uye>()
            .Property(u => u.KayitTarihi)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Kıyafet verildi mi varsayılan false
        modelBuilder.Entity<Uye>()
            .Property(u => u.KiyafetVerildiMi)
            .HasDefaultValue(false);

        // Aidat ödendi mi varsayılan false
        modelBuilder.Entity<Aidat>()
            .Property(a => a.OdendiMi)
            .HasDefaultValue(false);

        // ========== İNDEKSLER (Performans için) ==========

        modelBuilder.Entity<Uye>()
            .HasIndex(u => u.AdSoyad)
            .HasDatabaseName("IX_Uyeler_AdSoyad");

        modelBuilder.Entity<Uye>()
            .HasIndex(u => u.Telefon)
            .HasDatabaseName("IX_Uyeler_Telefon");

        modelBuilder.Entity<Uye>()
            .HasIndex(u => u.SporSalonuId)
            .HasDatabaseName("IX_Uyeler_SporSalonuId");

        modelBuilder.Entity<Uye>()
            .HasIndex(u => u.BransId)
            .HasDatabaseName("IX_Uyeler_BransId");

        modelBuilder.Entity<Uye>()
            .HasIndex(u => u.TakimId)
            .HasDatabaseName("IX_Uyeler_TakimId");

        modelBuilder.Entity<Uye>()
            .HasIndex(u => u.GrupId)
            .HasDatabaseName("IX_Uyeler_GrupId");

        modelBuilder.Entity<Takim>()
            .HasIndex(t => t.SporSalonuId)
            .HasDatabaseName("IX_Takimlar_SporSalonuId");

        modelBuilder.Entity<Takim>()
            .HasIndex(t => t.BransId)
            .HasDatabaseName("IX_Takimlar_BransId");

        modelBuilder.Entity<Grup>()
            .HasIndex(g => g.TakimId)
            .HasDatabaseName("IX_Gruplar_TakimId");

        modelBuilder.Entity<Aidat>()
            .HasIndex(a => a.UyeId)
            .HasDatabaseName("IX_Aidatlar_UyeId");

        modelBuilder.Entity<Aidat>()
            .HasIndex(a => a.OdemeTarihi)
            .HasDatabaseName("IX_Aidatlar_OdemeTarihi");

        modelBuilder.Entity<Antrenor>()
            .HasIndex(a => a.Email)
            .HasDatabaseName("IX_Antrenorler_Email")
            .IsUnique();

        modelBuilder.Entity<AntrenorTakim>()
            .HasIndex(at => at.AntrenorId)
            .HasDatabaseName("IX_AntrenorTakimlar_AntrenorId");

        modelBuilder.Entity<AntrenorTakim>()
            .HasIndex(at => at.SporSalonuId)
            .HasDatabaseName("IX_AntrenorTakimlar_SporSalonuId");

        // ========== ALAN UZUNLUKLARI ==========

        modelBuilder.Entity<Uye>()
            .Property(u => u.AdSoyad)
            .HasMaxLength(100);

        modelBuilder.Entity<Uye>()
            .Property(u => u.Telefon)
            .HasMaxLength(20);

        modelBuilder.Entity<Uye>()
            .Property(u => u.VeliAdSoyad)
            .HasMaxLength(100);

        modelBuilder.Entity<Uye>()
            .Property(u => u.VeliTelefon)
            .HasMaxLength(20);

        modelBuilder.Entity<Uye>()
            .Property(u => u.KiyafetNotlari)
            .HasMaxLength(500);

        modelBuilder.Entity<SporSalonu>()
            .Property(s => s.Ad)
            .HasMaxLength(100);

        modelBuilder.Entity<Brans>()
            .Property(b => b.Ad)
            .HasMaxLength(50);

        modelBuilder.Entity<Takim>()
            .Property(t => t.Ad)
            .HasMaxLength(50);

        modelBuilder.Entity<Grup>()
            .Property(g => g.Ad)
            .HasMaxLength(20);

        modelBuilder.Entity<Aidat>()
            .Property(a => a.Ay)
            .HasMaxLength(20);

        modelBuilder.Entity<Aidat>()
            .Property(a => a.Aciklama)
            .HasMaxLength(500);

        modelBuilder.Entity<Antrenor>()
            .Property(a => a.AdSoyad)
            .HasMaxLength(100);

        modelBuilder.Entity<Antrenor>()
            .Property(a => a.Email)
            .HasMaxLength(100);

        modelBuilder.Entity<Antrenor>()
            .Property(a => a.Telefon)
            .HasMaxLength(20);

        modelBuilder.Entity<Antrenor>()
            .Property(a => a.Uzmanlik)
            .HasMaxLength(100);

        // ========== PARA BİRİMİ AYARLARI ==========

        modelBuilder.Entity<Uye>()
            .Property(u => u.AylikAidat)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Aidat>()
            .Property(a => a.Tutar)
            .HasColumnType("decimal(18,2)");
    }
}