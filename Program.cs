namespace TextAdventure_Solo_2;

class Program
{
    
    static void Main(string[] args)
    
    {
        Console.WriteLine("Welcome to the world of disorderly conduct! " +
                          "You are an old delinquent who never learnt to remedy their ways, " +
                          "in a world ruled by the terrible 'redeemed'. You are trapped in a rehabilitation room, " +
                          "and must escape into the outside where once more you may wreak havoc upon the world.");
        
        Transition("[Enter]");


        Player player = new Player();
        while (player.Location != "quit")
        {
            if (player.Location == "newgame")
            {
                NewGame(player);
            }
            
            else if (player.Location == "tableroom")
            {
                TableRoom(player);
            }
            
            else if (player.Location == "corridor")
            {
                Corridor(player);
            }
            
            else if (player.Location == "lockedroom")
            {
                LockedRoom(player);
            }
            
            else if (player.Location == "thirdroom")
            {
                ThirdRoom(player);
            }

            {
                Console.Error.WriteLine(
                    $"You forgot to implement '{player.Location}'!");
            }
        }


            string action1 = "check";
            string action2 = "pick up";
            string action3 = "use";
            string action4 = "hit";

            Console.WriteLine($"\nYou are surrounded by four walls of the most awful beige hue. " +
                              $"A flickering ceiling-lamp is casting a depressing glow upon you surroundings. " +
                              $"You have no items to your name, and are dressed in naught but a grey one-piece, " +
                              $"spotted by what you assume to be bodily fluids. You have no sense of direction. " +
                              $"One solitary door marked by countless scratches is crammed into a corner of the room. " +
                              $"In the middle of the room, directly under the lamp, is a square wooden table with" +
                              $" a box on top.");
            
            Console.Write($"\nWhat do you do? [{action1}], [{action2}], [{action3}], [{action4}] :");
            
    //Items

    string woodensword = "wooden sword";
    
    string knife = "knife";
    
    string key = "key";
    }

    //Functions
    
    static void Transition(string transition) //Ensures the player can read the text at the end of a room.
    {
        Console.Write(transition);
        Console.ReadLine();
    }

    static string Ask(string question)
    {
        string response;
        do
        {
            Console.Write(question);
            response = Console.ReadLine().Trim();
        } while (response == "");

        return response;
    }

    static bool AskYesOrNo(string question)
    {
        while (true)
        {
            string response = Ask(question).ToLower();
            switch (response) {
                case "yes":
                case "ok":
                    return true;
                case "no":
                    return false;
            }
        }
    }
    
    // static rightanswer(string question)
    
    // Rooms

    static void NewGame(Player player)
    {
        Console.Clear();
        string playername = "";
        do
        {
            playername = Ask("What is your name, delinquent? ");
        } while (!AskYesOrNo($"So, {playername} it is? [yes/ ok] / [no] "));

        player.Name = playername;
        player.Location = "tableroom";

    }

    static void TableRoom(Player player)
    {
        Console.Clear();
        player.Items.Add("WoodenSword");
        Console.WriteLine("You are equipped with one wooden Sword, and your task " +
                          "is to slay the monster at the end of the adventure.\n" +
                          "In front of you is a stone table with two items on it," +
                          "a knife and a key. " +
                          "You can only pick up one of these items.");

        string responsetoitems;
        do
        {
            responsetoitems = Ask("\nWhich item do you choose? [knife], [key], [none]: ");
        } while (responsetoitems != "knife" && responsetoitems != "key" && responsetoitems != "none");

        if (responsetoitems == "knife")
        {
            player.Items.Add("knife");
            Console.WriteLine("[Knife] was added to inventory. ");
        }
        else if (responsetoitems == "key")
        {
            player.Items.Add("key");
            Console.WriteLine("[Key] was added to inventory. ");
        }
        else if (responsetoitems == "none")
        {
            Console.WriteLine("\nYou press on, not paying the items on the table any mind. " +
                              "You feel something watching you intently.");
        }
        
        player.Location = "corridor";
        Transition("\nYou continue towards the corridor. [Enter] ");
    }

    static void Corridor(Player player)
    {
        Console.Clear();
        Console.WriteLine("You exit the room and find yourself standing in a dark " +
                          "hallway. You can either enter another room on your right " +
                          "side, or continue down the hallway on your left. ");

        Console.ReadLine();
        
        
        
        if (player.Items.Contains("key"))
        {
            player.Location = "lockedroom";
            player.Items.Remove("key");
            Console.WriteLine("[Key] removed from inventory.");
        }
        else
        {
            player.Location = "thirdroom";
        }
    }

    static void LockedRoom(Player player)
    {
    }
    
    static void ThirdRoom(Player player)
    {
        Console.Clear();
        Console.ReadLine();
    }



    //Classes

    class Player
    {
        public string Name = "";
        public int Sanity = 100;
        public List<string> Items = new List<string>();
        public string Location = "newgame";
    }
    
}