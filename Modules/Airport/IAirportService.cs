
using FlightPlanApi.Dtos.Airport;

namespace FlightPlanApi.Services.AirportService
{
  public interface IAirportService
  {
    ServiceResponse<List<GetAirportsDto>> GetAllAirports();
    ServiceResponse<GetAirportDto> GetAirportById(int id);

  }
}
