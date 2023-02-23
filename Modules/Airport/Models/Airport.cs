namespace FlightPlanApi.Models
{

  public class Images
  {
    public string Small { get; set; } = string.Empty;
    public string Large { get; set; } = string.Empty;
  }
  public class Airport
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
