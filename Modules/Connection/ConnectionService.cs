using System.Text.Json;
using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public class ConnectionService : IConnectionService
  {
    public ServiceResponse<List<Dictionary<string, int[]>>> GetConnections()
    {

      var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data/connections.json");
      var data = File.ReadAllText(path);
      var retVal = JsonSerializer.Deserialize<List<Dictionary<string, int[]>>>(data)!;
      var serviceResponse = new ServiceResponse<List<Dictionary<string, int[]>>>()
      {
        Data = retVal,
      };

      return serviceResponse;
    }
  }
}
