using System.Text.Json;
using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public class ConnectionService : IConnectionService
  {
    public ServiceResponse<Dictionary<string, int[]>> GetConnections()
    {

      var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data/connections.json");
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
