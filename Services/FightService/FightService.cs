using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Fight;

namespace dotnet_rpg.Services.FightService
{
  public class FightService : IFightService
  {
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public FightService(DataContext dataContext, IMapper mapper)
    {
      _dataContext = dataContext;
      _mapper = mapper;
    }


    public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
    {
      var serviceResponse = new ServiceResponse<AttackResultDto>();

      try
      {
        var attacker = await _dataContext.Characters
          .Include(c => c.Weapon)
          .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
        var opponent = await _dataContext.Characters
          .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

        if (attacker is null || opponent is null || attacker.Weapon is null) throw new Exception($"Something fishy is going on...");

        var damage = DoWeaponAttack(attacker, opponent);

        if (opponent.HitPoints <= 0) serviceResponse.Message = $"{opponent.Name} has been defeated!";

        await _dataContext.SaveChangesAsync();

        serviceResponse.Data = new AttackResultDto
        {
          Attacker = attacker.Name,
          Opponent = opponent.Name,
          AttackerHP = attacker.HitPoints,
          OpponentHp = opponent.HitPoints,
          Damage = damage,
        };
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    private static int DoWeaponAttack(Character attacker, Character opponent)
    {
      if (attacker.Weapon is null) throw new Exception("Attacker has no weapon!");
      var damage = attacker.Weapon.Damage + new Random().Next(attacker.Strength);
      damage -= new Random().Next(opponent.Defense);

      if (damage > 0) opponent.HitPoints -= damage;
      return damage;
    }

    public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
    {
      var serviceResponse = new ServiceResponse<AttackResultDto>();

      try
      {
        var attacker = await _dataContext.Characters
          .Include(c => c.Skills)
          .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
        var opponent = await _dataContext.Characters
          .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

        if (attacker is null || opponent is null || attacker.Skills is null) throw new Exception($"Something fishy is going on...");

        var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
        if (skill is null) throw new Exception($"{attacker.Name} doesn't know that skill!");

        var damage = DoSkillAttack(attacker, opponent, skill);

        if (opponent.HitPoints <= 0) serviceResponse.Message = $"{opponent.Name} has been defeated!";

        await _dataContext.SaveChangesAsync();

        serviceResponse.Data = new AttackResultDto
        {
          Attacker = attacker.Name,
          Opponent = opponent.Name,
          AttackerHP = attacker.HitPoints,
          OpponentHp = opponent.HitPoints,
          Damage = damage,
        };
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    private static int DoSkillAttack(Character attacker, Character opponent, Skill skill)
    {
      var damage = skill.Damage + new Random().Next(attacker.Intelligence);
      damage -= new Random().Next(opponent.Defense);

      if (damage > 0) opponent.HitPoints -= damage;
      return damage;
    }

    public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
    {
      var serviceResponse = new ServiceResponse<FightResultDto>
      {
        Data = new FightResultDto()
      };

      try
      {
        var characters = await _dataContext.Characters
          .Include(c => c.Skills)
          .Include(c => c.Weapon)
          .Where(c => request.CharacterIds.Contains(c.Id))
          .ToListAsync();

        bool defeated = false;

        while (!defeated)
        {
          foreach (var attacker in characters)
          {
            var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
            var opponent = opponents[new Random().Next(opponents.Count)];

            int damage = 0;
            string attackUsed = string.Empty;

            bool useWeapon = new Random().Next(2) == 0;
            if (useWeapon && attacker.Weapon is not null)
            {
              attackUsed = attacker.Weapon.Name;
              damage = DoWeaponAttack(attacker, opponent);
            }
            else if (!useWeapon && attacker.Skills is not null)
            {
              var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
              attackUsed = skill.Name;
              damage = DoSkillAttack(attacker, opponent, skill);
            }
            else
            {
              serviceResponse.Data.Log.Add($"{attacker.Name} wasn't able to attack!");
              continue;
            }

            serviceResponse.Data.Log.Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {damage} damage!");
            if (opponent.HitPoints <= 0)
            {
              defeated = true;
              attacker.Victories++;
              opponent.Defeats++;
              serviceResponse.Data.Log.Add($"{opponent.Name} has been defeated!");
              serviceResponse.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} hitpoints left!");
              break;
            }
          }
        }

        characters.ForEach(c =>
        {
          c.Fights++;
          c.HitPoints = 100;
        });

        await _dataContext.SaveChangesAsync();

      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<HighScoreDto>>> GetHighScore()
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
