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

    [HttpGet]
    public ActionResult<ServiceResponse<List<Connection>>> GetConnections()
    {
      return Ok(_connectionService.GetConnections());
    }
  }
}
