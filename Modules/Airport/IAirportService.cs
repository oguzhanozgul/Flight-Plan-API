
using FlightPlanApi.Dtos.Airport;

namespace FlightPlanApi.Services.CharacterService
{
  public interface ICharacterService
  {
    Task<ServiceResponse<List<GetAirportsDto>>> GetAllAirports();
    Task<ServiceResponse<GetAirportDto>> GetAirportById(int id);

  }
}
