using FlightPlanApi.Image;

namespace FlightPlanApi.Image
{
  public interface IImageService
  {
    ServiceResponse<Dictionary<string, int[]>> GetImage();
  }
}
