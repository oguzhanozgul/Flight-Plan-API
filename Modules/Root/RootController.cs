using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Controllers
{
  [ApiController]
  [Route("")]
  public class RootController : ControllerBase
  {
    [HttpGet()]
    public ActionResult<string> GetAll()
    {
      return Ok($"Running at port {HttpContext.Connection.LocalPort}");
    }

  }
}
