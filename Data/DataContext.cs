
namespace dotnet_rpg.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Skill>().HasData(
        new Skill { Id = 1, Name = "Fireball", Damage = 30 },
        new Skill { Id = 2, Name = "Magic missile", Damage = 5 },
        new Skill { Id = 3, Name = "Melf's acif arrow", Damage = 15 }
      );
    }

    public DbSet<Character> Characters => Set<Character>(); // Character is the thing we want to keep in the DB, Characters is the name of the table
    public DbSet<User> Users => Set<User>();
    public DbSet<Weapon> Weapons => Set<Weapon>();
    public DbSet<Skill> Skills => Set<Skill>();
  }
}
