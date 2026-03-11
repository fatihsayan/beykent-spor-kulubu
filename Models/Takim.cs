namespace SporKulubu.Models;

public class Takim
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty;
    public int SporSalonuId { get; set; }
    public SporSalonu? SporSalonu { get; set; }
    public int? BransId { get; set; } // Hangi branşa ait?
    public Brans? Brans { get; set; }
    public string? AntrenorAdi { get; set; }
    public ICollection<Grup>? Gruplar { get; set; }
}