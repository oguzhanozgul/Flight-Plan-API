using FlightPlanApi.Dtos.Connection;
using FlightPlanApi.Services.ConnectionService;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ConnectionController : ControllerBase
  {
    private readonly IConnectionService _connectionService;

    public ConnectionController(IConnectionService connectionService)
    {
      _connectionService = connectionService;
    }

    [HttpGet]
    public ActionResult<ServiceResponse<string>> GetConnections()
    {
      return Ok(_connectionService.GetConnections());
    }
  }
}
