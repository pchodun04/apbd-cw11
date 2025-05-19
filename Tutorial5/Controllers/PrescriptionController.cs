using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tutorial5.Services;

namespace Tutorial5.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PrescriptionController
{
    private readonly IDbService _dbService;
    public PrescriptionController(IDbService dbService)
    {
        _dbService = dbService;
    }
        
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var books = await _dbService.GetBooks();
        return Ok(books);
    }
}