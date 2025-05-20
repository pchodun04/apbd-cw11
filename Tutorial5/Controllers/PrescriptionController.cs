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
    public async Task<IActionResult> AddPrescription([FromBody] CreatePrescriptionDto prescription)
    {
        await _prescriptionService.AddPrescription(prescription);
        return Ok("Recepta została pomyślnie dodana.");
    }
}