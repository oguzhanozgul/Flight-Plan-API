using FlightPlanApi.Dtos.Connection;

namespace FlightPlanApi.Services.ConnectionService
{
  public class FightService : IConnectionService
  {
    public async Task<ServiceResponse<string>> GetConnections()
    {
      var characters = await _dataContext.Characters
        .Where(c => c.Fights > 0)
        .OrderByDescending(c => c.Victories)
        .ThenBy(c => c.Defeats)
        .ToListAsync();

      var serviceResponse = new ServiceResponse<List<HighScoreDto>>()
      {
        Data = characters.Select(c => _mapper.Map<HighScoreDto>(c)).ToList(),
      };

      return serviceResponse;
    }
  }
}
