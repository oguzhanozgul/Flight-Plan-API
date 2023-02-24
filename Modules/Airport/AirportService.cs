using System.Security.Claims;
using AutoMapper;
using FlightPlanApi.Dtos.Airport;

namespace FlightPlanApi.Services.AirportService
{

  public class AirportService : IAirportService
  {
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AirportService(IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
    }

    public ServiceResponse<List<GetAirportsDto>> GetAllAirports()
    {
      var serviceResponse = new ServiceResponse<List<GetAirportsDto>>();
      // var dbCharacters = await _dataContext.Characters
      //   .Include(c => c.Connection)
      //   .Include(c => c.Skills)
      //   .Where(c => c.User!.Id == GetUserId())
      //   .ToListAsync();
      // serviceResponse.Data = dbCharacters
      //   .Select(c => _mapper.Map<GetCharacterDto>(c))
      //   .ToList();
      return serviceResponse;
    }


    public ServiceResponse<GetAirportDto> GetAirportById(int id)
    {
      var serviceResponse = new ServiceResponse<GetAirportDto>();

      // try
      // {
      //   var dbCharacter = await _dataContext.Characters
      //     .Include(c => c.Connection)
      //     .Include(c => c.Skills)
      //     .Where(c => c.User!.Id == GetUserId())
      //     .FirstOrDefaultAsync(c => c.Id == id); // veya .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId()); de olur.
      //   if (dbCharacter is null) throw new Exception($"Character with Id {id} not found.");
      //   serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
      // }
      // catch (Exception ex)
      // {
      //   serviceResponse.Success = false;
      //   serviceResponse.Message = ex.Message;
      // }
      return serviceResponse;
    }

  }
}

