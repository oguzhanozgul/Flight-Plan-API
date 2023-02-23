using System.Security.Claims;
using AutoMapper;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{

  public class CharacterService : ICharacterService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CharacterService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
      _dataContext = dataContext;
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
    }

    private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
      .FindFirstValue(ClaimTypes.NameIdentifier)!);


    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      var character = _mapper.Map<Character>(newCharacter);
      character.User = await _dataContext.Users.FirstOrDefaultAsync(user => user.Id == GetUserId());
      _dataContext.Characters.Add(character);
      await _dataContext.SaveChangesAsync();

      serviceResponse.Data = await _dataContext.Characters
        .Where(c => c.User!.Id == GetUserId())
        .Select(c => _mapper.Map<GetCharacterDto>(c))
        .ToListAsync();
      return serviceResponse;
    }


    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      var dbCharacters = await _dataContext.Characters
        .Include(c => c.Weapon)
        .Include(c => c.Skills)
        .Where(c => c.User!.Id == GetUserId())
        .ToListAsync();
      serviceResponse.Data = dbCharacters
        .Select(c => _mapper.Map<GetCharacterDto>(c))
        .ToList();
      return serviceResponse;
    }


    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();

      try
      {
        var dbCharacter = await _dataContext.Characters
          .Include(c => c.Weapon)
          .Include(c => c.Skills)
          .Where(c => c.User!.Id == GetUserId())
          .FirstOrDefaultAsync(c => c.Id == id); // veya .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId()); de olur.
        if (dbCharacter is null) throw new Exception($"Character with Id {id} not found.");
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }


    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();
      try
      {
        var character = await _dataContext.Characters
          .Include(c => c.User)
          .Where(c => c.User!.Id == GetUserId())
          .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
        if (character is null) throw new Exception($"Character with Id {updatedCharacter.Id} not found.");

        character.Name = updatedCharacter.Name;
        character.HitPoints = updatedCharacter.HitPoints;
        character.Strength = updatedCharacter.Strength;
        character.Defense = updatedCharacter.Defense;
        character.Intelligence = updatedCharacter.Intelligence;
        character.Class = updatedCharacter.Class;

        await _dataContext.SaveChangesAsync();

        // veya:
        // _mapper.Map(updatedCharacter, character)
        // ve AutoMapperProfile.cs'de:
        // CreateMap<UpdateCharacterDto, Character>();
        // character = await _dataContext.Characters
        //   .Where(c => c.User!.Id == GetUserId())
        //   .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }


    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      try
      {
        var character = await _dataContext.Characters
          .Where(c => c.User!.Id == GetUserId())
          .FirstOrDefaultAsync(c => c.Id == id);
        if (character is null) throw new Exception($"Character with Id {id} not found.");

        _dataContext.Characters.Remove(character);

        await _dataContext.SaveChangesAsync();

        serviceResponse.Data = await _dataContext.Characters
          .Where(c => c.User!.Id == GetUserId())
          .Select(c => _mapper.Map<GetCharacterDto>(c))
          .ToListAsync();
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();
      try
      {
        var character = await _dataContext.Characters
          .Include(c => c.Weapon)
          .Include(c => c.Skills)
          .Where(c => c.User!.Id == GetUserId())
          .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId);
        if (character is null) throw new Exception($"Character with Id {newCharacterSkill.CharacterId} not found.");

        var skill = await _dataContext.Skills
          .FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
        if (skill is null) throw new Exception($"Skill with Id {newCharacterSkill.SkillId} not found.");

        character.Skills!.Add(skill);

        await _dataContext.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }
  }
}

