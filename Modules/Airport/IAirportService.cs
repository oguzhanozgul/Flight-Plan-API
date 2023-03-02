namespace FlightPlanApi.Airports
{
  public interface IAirportService
  {
    ServiceResponse<List<GetAirportDto>> GetAllAirports();
  }
}
