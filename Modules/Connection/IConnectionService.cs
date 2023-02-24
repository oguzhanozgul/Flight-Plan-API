using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public interface IConnectionService
  {
    ServiceResponse<string> GetConnections();
  }
}
