using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public interface IConnectionService
  {
    ServiceResponse<List<Dictionary<int, int[]>>> GetConnections();
  }
}
