using System.Text.Json;
using AutoMapper;

namespace FlightPlanApi.Airports
{

  public class AirportService : IAirportService
  {
    private readonly IMapper _mapper;

    public AirportService(IMapper mapper)
    {
      _mapper = mapper;
    }

    public ServiceResponse<List<GetAirportDto>> GetAllAirports()
    {
      var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/airports.json");
      var data = File.ReadAllText(path);
      var retVal = JsonSerializer.Deserialize<List<GetAirportDto>>(data)!;
      var serviceResponse = new ServiceResponse<List<GetAirportDto>>
      {
        Data = retVal,
      };
      return serviceResponse;
    }
  }
}


