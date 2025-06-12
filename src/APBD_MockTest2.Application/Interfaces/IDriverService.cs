using ClassLibrary1.DTOs;

namespace APBD_MockTest2.Application.Interfaces;

public interface IDriverService
{
    public Task<List<ShortDriverDTO>> GetAllDrivers(string? sortBy);
    public Task<FullDriverDTO?> GetDriverById(int id);
    public Task<Driver> PostDriver(InsertDriverDTO request);
    public Task<DriverCompetition> AssignDriverToCompetition(InsertDriverCompetitionDTO request);
    public Task<List<CompetitionDTO>> GetAllCompetitions();
}