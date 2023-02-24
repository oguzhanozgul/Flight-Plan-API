using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public interface IConnectionService
  {
    ServiceResponse<List<Dictionary<string, int[]>>> GetConnections();
  }
}
