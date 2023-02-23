namespace dotnet_rpg.Models
{
  public class Weapon
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }
    public Character? Character { get; set; }
    public int CharacterId { get; set; } // bu olmazsa hata veriyor 1:1 iliski oldugu icin. Bunu girerecek dependant olan sey Weapon (Character olmadan Weapon olamaz) demis oluyoruz.

  }
}
