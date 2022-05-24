namespace sharp_miner.Models
{
  public class Upgrade
  {
    public String Name { get; private set; }
    public String Type { get; private set; }
    public int Price { get; set; }
    public decimal Quantity { get; set; }
    public decimal Multiplier { get; set; }
    public Upgrade(string name, string type, int price, decimal quantity, decimal multiplier)
    {
      Name = name;
      Type = type;
      Price = price;
      Quantity = quantity;
      Multiplier = multiplier;
    }
  }
}