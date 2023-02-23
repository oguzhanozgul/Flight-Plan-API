using AutoMapper;
using FlightPlanApi.Dtos.Airport;

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
