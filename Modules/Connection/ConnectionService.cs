using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public class ConnectionService : IConnectionService
  {
    public ServiceResponse<string> GetConnections()
    {

      var serviceResponse = new ServiceResponse<string>()
      {
        Data = File.ReadAllText(@"../../Data/connections.txt"),
      };

      return serviceResponse;
    }
  }
}
