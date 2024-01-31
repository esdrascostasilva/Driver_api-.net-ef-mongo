using Microsoft.AspNetCore.Mvc;

namespace Driver.API;

[ApiController]
[Route("api/[controller]")]
public class DriverController : Controller
{
    private readonly DriverService _driverService;

    public DriverController(DriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpGet]
    public async Task<List<Driver>> GetAllDrivers()
    {
        return await _driverService.GetAll();
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Driver>> GetDriverById(string id)
    {
        var driver = await _driverService.GetById(id);
        return Ok(driver);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDriver(Driver driverRequest)
    {
        await _driverService.PostDriver(driverRequest);
        return CreatedAtAction(nameof(GetAllDrivers), new { id = driverRequest.Id }, driverRequest);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> EditDriver(string id, Driver driverRequest)
    {
        await _driverService.PutDriver(id, driverRequest);
        return NoContent();
    }

    [HttpDelete("{id:Length(24)}")]
    public async Task<IActionResult> RemoveDriver(string id)
    {
        await _driverService.DeleteDriver(id);
        return NoContent();
    }
}
