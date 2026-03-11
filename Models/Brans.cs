namespace SporKulubu.Models;

public class Brans
{
    public int Id { get; set; }
    public string Ad { get; set; } = string.Empty; // "Voleybol", "Basketbol", "Atletik Performans"
    public bool TakimVarMi { get; set; } // Basketbol için true, Atletik için false
    public bool GrupVarMi { get; set; } // Basketbol için true, Atletik için false
    public int SporSalonuId { get; set; }
    public SporSalonu? SporSalonu { get; set; }
}