using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Image
{
  [ApiController]
  [Route("[controller]")]
  public class ImageController : ControllerBase
  {
    [HttpGet("small/{name}")]
    public ActionResult<string> GetSmallImage(string name)
    {
      var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, $"data/images/small/{name}");

      return PhysicalFile(path, "image/jpg");
    }

    [HttpGet("full/{name}")]
    public ActionResult<string> GetLargeImage(string name)
    {
      var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, $"data/images/full/{name}");

      return PhysicalFile(path, "image/jpg");
    }
  }
}
