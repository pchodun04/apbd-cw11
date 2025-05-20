using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tutorial5.DTOs;
using Tutorial5.Services;

namespace Tutorial5.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;
    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }
        
    [HttpPost]
    public async Task<IActionResult> AddPrescription(CreatePrescriptionDto prescription)
    {
        // Sprawdzenie: maksymalnie 10 leków
        if (prescription.Medicaments.Count > 10)
            return BadRequest("Recepta może zawierać maksymalnie 10 leków.");

        // Sprawdzenie: dueDate >= date
        if (prescription.DueDate < prescription.Date)
            return BadRequest("DueDate musi być późniejszy lub równy Date.");

        // Sprawdzenie: czy istnieją wszystkie leki
        foreach (var med in prescription.Medicaments)
        {
            if (!await _prescriptionRepository.DoesMedicamentExist(med.IdMedicament))
                return NotFound($"Lek o ID {med.IdMedicament} nie istnieje.");
        }

        // Sprawdzenie: czy lekarz istnieje
        if (!await _prescriptionRepository.DoesDoctorExist(prescription.IdDoctor))
            return NotFound($"Lekarz o ID {prescription.IdDoctor} nie istnieje.");

        // Pobierz lub dodaj pacjenta
        var patient = await _prescriptionRepository.GetPatient(prescription.Patient);
        if (patient == null)
        {
            patient = await _prescriptionRepository.AddPatient(prescription.Patient);
        }

        // Dodaj receptę i leki
        var createdPrescription = await _prescriptionRepository.AddPrescriptionWithMedicaments(prescription, patient.IdPatient);

        return Created(Request.Path.Value ?? "/api/prescriptions", createdPrescription);
}