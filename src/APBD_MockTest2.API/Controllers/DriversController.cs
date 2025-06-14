using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APBD_MockTest2;
using APBD_MockTest2.Application.Interfaces;
using ClassLibrary1.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace APBD_MockTest2.Controllers;

[Route("api/drivers")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    // GET: api/drivers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShortDriverDTO>>> GetDrivers([FromQuery] string? sortBy)
    {
        try
        {
            var result = await _driverService.GetAllDrivers(sortBy);
            return result.IsNullOrEmpty() ? NoContent() : Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET: api/drivers/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Driver>> GetDriver(int id)
    {
        try
        {
            var result = await _driverService.GetDriverById(id);
            return result is null ? NotFound("Driver with given id is not found") : Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST: api/drivers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Driver>> PostDriver(InsertDriverDTO driver)
    {
        try
        {
            var result = await _driverService.PostDriver(driver);
            return Created("/api/drivers/", result.Id);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPost]
    [Route("competitions")]
    public async Task<ActionResult<DriverCompetition>> AssignDriverToCompetition([FromBody] InsertDriverCompetitionDTO request)
    {
        try
        {
            await _driverService.AssignDriverToCompetition(request);
            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest("Such pair of DriverId and CompetitionId already exists in the database.");
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet]
    [Route("competitions")]
    public async Task<ActionResult<IEnumerable<CompetitionDTO>>> GetCompetitions()
    {
        try
        {
            var result = await _driverService.GetAllCompetitions();
            return result.IsNullOrEmpty() ? NoContent() : Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}