
namespace sharp_miner.Models
{
  public class SpaceGame
  {
    public bool Running { get; set; }
    public decimal Cheese { get; set; }
    public List<Upgrade> Shop { get; set; }
    public Dictionary<string, decimal> Stats { get; set; }
    public List<Upgrade> ClickUpgrades { get; set; }
    public bool InShop { get; set; }
    public SpaceGame()
    {
      Running = true;
      Shop = new List<Upgrade>() { };
      ClickUpgrades = new List<Upgrade>() { };
      Stats = new Dictionary<string, decimal>() { };
      // Populate upgrades
      Shop.Add(new Upgrade("Harrys Ham", "click", 1, 0, 1));
      Shop.Add(new Upgrade("Jerms Fight Stick", "click", 5, 0, 2));
      Shop.Add(new Upgrade("Sams Milk Shoes", "click", 15, 0, 5));
      Shop.Add(new Upgrade("Micks Hand Sanitizer", "click", 100, 0, 25));

      PlayGame();
    }

    public void PlayGame()
    {
      while (Running)
      {
        // converts user input into the actual key they pressed so we can use a switch on those conditions
        string input = GetPlayerInput().Key.ToString().ToLower();
        switch (input)
        {
          case "spacebar":
            Mine();
            break;
          case "s":
            GoToShop();
            break;
          case "escape":
            Running = false;
            break;
        }
      }
    }

    public void GoToShop()
    {
      InShop = true;
      Console.Clear();
      Console.WriteLine("Welcome to the McShop! BUY SOMETHING");
      // This is drawing the shop
      string message = "";
      for (int i = 0; i < Shop.Count; i++)
      {
        Upgrade item = Shop[i];
        message += $"{i + 1}. {item.Name}: {item.Price}, Multiplier: {item.Multiplier}\n";
      }
      Console.WriteLine(message);
      string? choice = Console.ReadLine();
      // convert user choice from string to int - making sure selection isn't a negative integer, AND that the interger isn't greater than the available shop options
      if (int.TryParse(choice, out int selection) && selection > 0 && selection <= Shop.Count)
      {
        BuyUpgrade(selection - 1);
      }
    }

    private void BuyUpgrade(int shopIndex)
    {
      // grab the actual upgrade object with the index that was passed in from above
      Upgrade item = Shop[shopIndex];
      if (Cheese >= item.Price)
      {
        Cheese -= item.Price;
        item.Price += item.Price;
        // checks to see if user has already purchased this particular upgrade
        // if they haven't, add the item to our upgrade list
        int index = ClickUpgrades.FindIndex(i => i.Name == item.Name);
        if (index == -1)
        {
          ClickUpgrades.Add(item);
          index = ClickUpgrades.Count - 1;
        }
        // if they have purchased it, increment the quantity that already exists
        ClickUpgrades[index].Quantity++;
        Stats[item.Name] = ClickUpgrades[index].Quantity;
        Console.WriteLine($"You purchased {item.Name}! NOW GIT OUT");
      }
      else
      {
        Console.WriteLine($"You dont have enough cheese for this {item.Name}. GIIIITTTTTTTT");
      }
      Console.WriteLine("press any key to exit");
      Console.ReadKey();
      InShop = false;
    }

    public void Mine()
    {
      Cheese++;
      // iterate through click upgrades
      ClickUpgrades.ForEach(m =>
     {
       Cheese += m.Quantity * m.Multiplier;
     });
    }

    public ConsoleKeyInfo GetPlayerInput()
    {
      // re-usable method to grab player input and also redraw the screen everytime that happens
      Console.Clear();
      DrawGameScreen();
      return Console.ReadKey();
    }

    public void DrawGameScreen()
    {
      Console.ForegroundColor = ConsoleColor.DarkYellow;
      string moon = @"
                                                                                                      
                              %%&&&&&&%                                         
                       %%%%%%%&%%%%&&&&&&&&&&&                                  
                   (###(((###%###%%&%&&%&%###(%&&%                              
                 ,***///(####((///(%%%##%%%&&%&&&%&%                            
                 .,*,*/(/#(/*//((/(///(///(%#&(((#%&&&                          
                  ..,,*/////**////////(///(((#(#((#%&&&&                        
                  ....,*/**(/**/*/#(**////(%#%%##(#%%&%&&                       
                    ..,.,,#/***,*/(##%(((%(##%%%%%%%%&&&&&                      
                     ...,,,,,*//((####(#&%%%&%###(%%&&&&&&&                     
                      ...,,..,/*/(((####%#%%%%%&#%%%&&@@&&&                     
                        .....,**////#((##%#%%%%&%&%&&&&&&&%                     
                         ....,,**////((((##%##&%%%%%&&&%%&%                     
                            .....,***//#((###%&%#%%%%&&%&%%                     
                              ......**((#######%#%%%%%%%&%                      
                                .....,*(#(#(##%((#%%%%%&%                       
                                  ...,**/##/(#%###%##%%%                        
                                      * * /#(((((% #%#                          
                                         ..*,@* /(%&                            
                                                                                
        ";
      Console.WriteLine(moon);
      // iterate through stat dictionary and draw to the console
      string message = $@"
        Mine[SpaceBar], Shop[s], Quit[escape]
        Cheese: {Cheese};
        Inventory: ";
      foreach (var stat in Stats)
      {
        message += $"\n {stat.Key}: {stat.Value}";
      }
      Console.WriteLine(message);
      Console.ForegroundColor = ConsoleColor.DarkMagenta;
    }
  }
}