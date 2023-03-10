using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Connection
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
    public ActionResult<ServiceResponse<Dictionary<string, int[]>>> GetConnections()
    {
      return Ok(_connectionService.GetConnections());
    }
  }
}
