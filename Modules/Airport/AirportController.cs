using System.Security.Claims;
using FlightPlanApi.Dtos.Character;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AirportController : ControllerBase
  {
    private readonly IAirportServiceService _airportService;

    public AirportController(IAirportService airportService)
    {
      _airportServiceService = airportService;
    }


    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
    {
      return Ok(await _characterService.GetAllCharacters());
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
    {
      return Ok(await _characterService.GetCharacterById(id));
    }

  }
}
