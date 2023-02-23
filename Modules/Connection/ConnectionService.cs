using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public class ConnectionService : IConnectionService
  {
    public async Task<ServiceResponse<string>> GetConnections()
    {
      var airport = await _dataContext.Airports
        .Where(c => c.Connections > 0)
        .OrderByDescending(c => c.Victories)
        .ThenBy(c => c.Defeats)
        .ToListAsync();

      var serviceResponse = new ServiceResponse<List<HighScoreDto>>()
      {
        Data = airports.Select(c => _mapper.Map<HighScoreDto>(c)).ToList(),
      };

      return serviceResponse;
    }
  }
}
