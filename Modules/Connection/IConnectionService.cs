using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public interface IConnectionService
  {
    Task<ServiceResponse<string>> GetConnections();
  }
}
