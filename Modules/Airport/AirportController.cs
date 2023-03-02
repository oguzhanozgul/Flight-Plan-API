using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Airports
{
  [ApiController]
  [Route("[controller]")]
  public class AirportController : ControllerBase
  {
    private readonly IAirportService _airportService;

    public AirportController(IAirportService airportService)
    {
      _airportService = airportService;
    }


    [HttpGet()]
    public ActionResult<ServiceResponse<List<GetAirportDto>>> GetAll()
    {
      return Ok(_airportService.GetAllAirports());
    }

  }
}
