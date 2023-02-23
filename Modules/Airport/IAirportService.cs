
using FlightPlanApi.Dtos.Airport;

namespace FlightPlanApi.Services.AirportService
{
  public interface IAirportService
  {
    Task<ServiceResponse<List<GetAirportsDto>>> GetAllAirports();
    Task<ServiceResponse<GetAirportDto>> GetAirportById(int id);

  }
}
