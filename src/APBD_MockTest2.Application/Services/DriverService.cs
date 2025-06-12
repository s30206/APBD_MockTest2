using APBD_MockTest2.Application.Interfaces;
using ClassLibrary1.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APBD_MockTest2.Application.Services;

public class DriverService : IDriverService
{
    private readonly TournamentContext _context;

    public DriverService(TournamentContext context)
    {
        _context = context;
    }
    
    public async Task<List<ShortDriverDTO>> GetAllDrivers(string? sortBy)
    {
        var drivers = _context.Drivers.AsQueryable();

        drivers = sortBy?.ToLower() switch
        {
            "lastname" => drivers.OrderBy(o => o.LastName),
            "birthday" => drivers.OrderBy(o => o.Birthday),
            _ => drivers.OrderBy(o => o.FirstName)
        };
        
        var result = await drivers.Select(d => new ShortDriverDTO
        {
            Id = d.Id,
            Name = d.FirstName + " " + d.LastName,
            Birthday = d.Birthday,
        }).ToListAsync();
        
        return result;
    }

    public async Task<FullDriverDTO?> GetDriverById(int id)
    {
        var driver = await _context.Drivers
            .Include(d => d.Car)
            .ThenInclude(c => c.CarManufacturer)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (driver is null)
            return null;
        
        return new FullDriverDTO()
        {
            Driver = new ShortDriverDTO {
                Id = driver.Id,
                Name = driver.FirstName + " " + driver.LastName,
                Birthday = driver.Birthday,
            },
            Car = new FullCarDTO()
            {
                Number = driver.Car.Number,
                Manufacturer = driver.Car.CarManufacturer.Name,
                ModelName = driver.Car.ModelName
            }
        };
    }

    public async Task<Driver> PostDriver(InsertDriverDTO request)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == request.CarId);
        if (car is null)
            throw new KeyNotFoundException("Car with a given id doesn't exist");
        
        var driver = new Driver()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthday = request.Birthday,
            CarId = request.CarId,
        };
        
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();
        return driver;
    }

    public async Task<DriverCompetition> AssignDriverToCompetition(InsertDriverCompetitionDTO request)
    {
        var driver = _context.Drivers.FirstOrDefault(d => d.Id == request.DriverId);
        if (driver is null)
            throw new KeyNotFoundException("Driver with a given id doesn't exist");
        
        var competition = _context.Competitions.FirstOrDefault(c => c.Id == request.CompetitionId);
        if (competition is null)
            throw new KeyNotFoundException("Competition with a given id doesn't exist");

        var result = new DriverCompetition()
        {
            CompetitionId = request.CompetitionId,
            DriverId = request.DriverId,
            Date = request.Date,
        };
        
        _context.DriverCompetitions.Add(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<List<CompetitionDTO>> GetAllCompetitions()
    {
        return await _context.DriverCompetitions
            .Include(dc => dc.Competition)
            .Select(c => new CompetitionDTO()
            {
                Name = c.Competition.Name,
                Date = c.Date
            }).ToListAsync();
    }
}