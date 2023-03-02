using AutoMapper;
using FlightPlanApi.Airports;
namespace FlightPlanApi
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Airport, GetAirportDto>();
    }
  }
}
