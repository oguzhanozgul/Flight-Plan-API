using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanApi.Dtos.Fight;
using FlightPlanApi.Services.FightService;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ConnectionController : ControllerBase
  {
    private readonly IConnectionService _connectionService;

    public ConnectionController(IConnectionService connectionService)
    {
      _connectionService = connectionService;
    }

    [HttpGet("Connections")]
    public async Task<ActionResult<ServiceResponse<List<HighScoreDto>>>> GetConnections()
    {
      return Ok(await _connectionService.GetConnections());
    }
  }
}
