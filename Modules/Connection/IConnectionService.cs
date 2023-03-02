namespace FlightPlanApi.Connection
{
  public interface IConnectionService
  {
    ServiceResponse<Dictionary<string, int[]>> GetConnections();
  }
}
