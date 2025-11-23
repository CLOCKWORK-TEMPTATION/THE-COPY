using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheCopy.Server.Data;
using TheCopy.Server.Services;

namespace TheCopy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScriptsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly MongoService _mongoService;

    public ScriptsController(ApplicationDbContext context, MongoService mongoService)
    {
        _context = context;
        _mongoService = mongoService;
    }

    [HttpGet("verify-db")]
    public async Task<IActionResult> VerifyDatabaseConnection()
    {
        var postgresStatus = "Not Connected";
        var mongoStatus = "Not Connected";

        try
        {
            if (await _context.Database.CanConnectAsync())
            {
                postgresStatus = "Connected";
            }
        }
        catch (Exception ex)
        {
            postgresStatus = $"Error: {ex.Message}";
        }

        try
        {
            // Simple check to list collections or just get a database reference
            var collection = _mongoService.GetCollection<object>("test");
            // If we can get the collection object, client is likely initialized. 
            // To be sure, we might want to run a command, but for now this checks dependency injection.
            // A better check would be to list collections.
            // But let's keep it simple as per instructions "initialize IMongoClient".
            // If the constructor didn't throw, we are likely good on config format.
            // To test connectivity we can try to count documents in a non-existent collection (should return 0).
            await collection.CountDocumentsAsync(_ => true);
            mongoStatus = "Connected";
        }
        catch (Exception ex)
        {
            mongoStatus = $"Error: {ex.Message}";
        }

        return Ok(new
        {
            PostgreSQL = postgresStatus,
            MongoDB = mongoStatus
        });
    }
}
