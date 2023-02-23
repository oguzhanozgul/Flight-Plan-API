using System.Security.Claims;
using dotnet_rpg.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")] // localhost:port/api/character
  public class CharacterController : ControllerBase
  {
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
      _characterService = characterService;
    }


    // [AllowAnonymous] Bu attribute hemen sonrasinda gelen endpointi herkese aciyor.
    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
    {
      // var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value); // ControllerBase class bir public ClaimsPrincipal User { get; } sagliyor. Bunu kullanarak userId'yi buluyoruz mesela.
      return Ok(await _characterService.GetAllCharacters());
    }


    // [HttpGet]
    // [Route("GetOne")] // localhost:port/api/Character/GetOne (yukaridaki gibi tek seferde de yapiliyor) Bunu yazmazssan localhost:port/api/Character olur
    [HttpGet("{id}")] // boyle path /get/24 gibi, yani part of url yukaridaki gibi olursa parameter oluyor. 
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
    {
      return Ok(await _characterService.GetCharacterById(id));
    }


    [HttpPost] // yeni karakter body of the requestte.
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
    {
      return Ok(await _characterService.AddCharacter(newCharacter));
    }


    [HttpPut] // yeni karakter body of the requestte.
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      var response = await _characterService.UpdateCharacter(updatedCharacter);
      if (response.Data is null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(int id)
    {
      var response = await _characterService.DeleteCharacter(id);
      if (response.Data is null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }

    [HttpPost("Skill")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
      var response = await _characterService.AddCharacterSkill(newCharacterSkill);

      if (response.Data is null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }
  }
}
