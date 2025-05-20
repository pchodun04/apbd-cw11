namespace Tutorial5.Models;

public class PrescriptionMedicaments
{
    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; }

    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; }

    public int Dose { get; set; }
    public string Description { get; set; }
}