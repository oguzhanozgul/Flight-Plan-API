using System.Text.Json;
using FlightPlanApi.Image;

namespace FlightPlanApi.Image
{
  public class ImageService : IImageService
  {
    public ServiceResponse<Dictionary<string, int[]>> GetImage()
    {

      var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/connections.json");
      var data = File.ReadAllText(path);
      var retVal = JsonSerializer.Deserialize<Dictionary<string, int[]>>(data)!;
      var serviceResponse = new ServiceResponse<Dictionary<string, int[]>>()
      {
        Data = retVal,
      };

      return serviceResponse;
    }
  }
}
