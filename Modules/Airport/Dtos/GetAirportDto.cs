namespace FlightPlanApi.Dtos.Airport
{
  public class GetAirportDto
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public Images? Images { get; set; }
    public int AverageRating { get; set; } = 0;
  }
}
