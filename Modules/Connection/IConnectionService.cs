using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public interface IConnectionService
  {
    ServiceResponse<Dictionary<string, int[]>> GetConnections();
  }
}
