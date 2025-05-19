using Tutorial5.Data;
using Tutorial5.DTOs;

namespace Tutorial5.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _context;
    public PrescriptionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddPrescription(CreatePrescriptionRequestDto createPrescriptionRequest)
    {
        
    }
}