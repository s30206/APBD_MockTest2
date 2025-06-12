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
}