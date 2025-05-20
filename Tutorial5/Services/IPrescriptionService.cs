using Tutorial5.DTOs;
using Tutorial5.Models;

namespace Tutorial5.Services;

public interface IPrescriptionService
{
    Task<bool> DoesMedicamentExist(int id);
    Task<bool> DoesDoctorExist(int id);
    Task<Patient?> GetPatient(PatientDto dto);
    Task<Patient> AddPatient(PatientDto dto);
    Task<PrescriptionDto> AddPrescriptionWithMedicaments(CreatePrescriptionDto dto, int patientId);}