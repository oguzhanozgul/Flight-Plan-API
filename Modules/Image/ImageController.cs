using Microsoft.AspNetCore.Mvc;

namespace FlightPlanApi.Image
{
  [ApiController]
  [Route("[controller]")]
  public class ImageController : ControllerBase
  {
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
      _imageService = imageService;
    }

    [HttpGet]
    public ActionResult<ServiceResponse<Dictionary<string, int[]>>> GetImage()
    {
      return Ok(_imageService.GetImage());
    }
  }
}
