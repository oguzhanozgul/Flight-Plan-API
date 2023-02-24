using FlightPlanApi.Dtos.Airport;
using FlightPlanApi.Services.AirportService;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Controllers
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
