using FlightPlanApi.Dtos.Airport;
using FlightPlanApi.Services.AirportService;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AirportController : ControllerBase
  {
    private readonly IAirportService _airportService;

    public AirportController(IAirportService airportService)
    {
      _airportService = airportService;
    }


    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetAirportDto>>>> GetAll()
    {
      return Ok(await _airportService.GetAllAirports());
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetAirportDto>>> GetSingle(int id)
    {
      return Ok(await _airportService.GetAirportById(id));
    }

  }
}
