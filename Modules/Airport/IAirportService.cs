
using FlightPlanApi.Dtos.Airport;

namespace FlightPlanApi.Services.AirportService
{
  public interface IAirportService
  {
    ServiceResponse<List<GetAirportDto>> GetAllAirports();
  }
}
