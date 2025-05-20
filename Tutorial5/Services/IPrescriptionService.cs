using Tutorial5.DTOs;
using Tutorial5.Models;

namespace Tutorial5.Services;

public interface IPrescriptionService
{
    Task<bool> DoesMedicamentExist(int id);
    Task<bool> DoesDoctorExist(int id);
    Task<Patient?> GetPatient(PatientDto patient);
    Task<Patient> AddPatient(PatientDto patient);
    Task<PrescriptionDto> AddPrescriptionWithMedicaments(CreatePrescriptionDto createPrescription, int patientId);}