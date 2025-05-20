using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;
using Tutorial5.Models;

namespace Tutorial5.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _context;
    public PrescriptionService(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<bool> DoesMedicamentExist(int id)
    {
        return await _context.Medicaments.AnyAsync(m => m.IdMedicament == id);
    }

    public async Task<bool> DoesDoctorExist(int id)
    {
        return await _context.Doctors.AnyAsync(d => d.IdDoctor == id);
    }

    public async Task<Patient?> GetPatient(PatientDto patient)
    {
        return await _context.Patients.FirstOrDefaultAsync(p =>
            p.FirstName == patient.FirstName &&
            p.LastName == patient.LastName &&
            p.Birthdate == patient.Birthdate);
    }

    public async Task<Patient> AddPatient(PatientDto dto)
    {
        var patient = new Patient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Birthdate = dto.Birthdate
        };
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<PrescriptionDto> AddPrescriptionWithMedicaments(CreatePrescriptionDto createPrescription, int patientId)
    {
        var prescription = new Prescription
        {
            Date = createPrescription.Date,
            DueDate = createPrescription.DueDate,
            IdDoctor = createPrescription.IdDoctor,
            IdPatient = patientId,
            PrescriptionMedicaments = createPrescription.Medicaments.Select(m => new PrescriptionMedicaments
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Description = m.Description
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
        
        return new PrescriptionDto
        {
            IdPrescription = prescription.IdPrescription,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            Doctor = new DoctorDto
            {
                IdDoctor = prescription.IdDoctor,
                FirstName = (await _context.Doctors.FindAsync(prescription.IdDoctor))?.FirstName,
                LastName = (await _context.Doctors.FindAsync(prescription.IdDoctor))?.LastName,
                Email = (await _context.Doctors.FindAsync(prescription.IdDoctor))?.Email
            },
            Medicaments = prescription.PrescriptionMedicaments.Select(pm => new MedicamentDetailDto
            {
                IdMedicament = pm.IdMedicament,
                Dose = pm.Dose,
                Description = pm.Description,
                Name = _context.Medicaments.FirstOrDefault(m => m.IdMedicament == pm.IdMedicament)?.Name
            }).ToList()
        };
    }
}