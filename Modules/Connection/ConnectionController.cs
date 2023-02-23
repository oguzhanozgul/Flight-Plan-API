using FlightPlanApi.Dtos.Connection;
using FlightPlanApi.Services.ConnectionService;
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
    public async Task<ActionResult<ServiceResponse<string>>> GetConnections()
    {
      return Ok(await _connectionService.GetConnections());
    }
  }
}
