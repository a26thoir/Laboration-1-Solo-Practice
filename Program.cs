namespace TextAdventure_Solo_2;

class Program
{
    
    static void Main(string[] args)
    
    {
        Console.Write("Welcome to the world of disorderly conduct! " +
                          "You are an old delinquent who never learnt to remedy their ways, " +
                          "in a world ruled by the terrible 'redeemed'. You are trapped in a rehabilitation room, " +
                          "and must escape into the outside where once more you may wreak havoc upon the world.");
        
        Transition("[Press Enter]"); // Check W3schools if anything f's up


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
    
    static void Transition(string transition) //Adds player prompt [Press Enter] at end of room, so text isn't cleared immediately. 
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

    static string TwoChoices(string question1, string choice1, string choice2)
    {
        string answer1 = "";
        do
        {
            Console.Write(question1) ;
            answer1 = Console.ReadLine().Trim().ToLower();
        } while (answer1 != choice1 && answer1 != choice2);

        return answer1;
    }

    // Rooms

    static void NewGame(Player player)
    {
        Console.Clear();
        string playername = "";
        do
        {
            playername = Ask("What is your name, delinquent? ");
        } while (!AskYesOrNo($"So, {playername} it is? [yes/ ok], [no] "));

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
                          "You can only pick up one of these items.\n");

        string responsetoitems;
        do
        {
            responsetoitems = Ask("Which item do you choose? [knife], [key], [none]: ");
        } while (responsetoitems != "knife" && responsetoitems != "key" && responsetoitems != "none");

        if (responsetoitems == "knife")
        {
            player.Items.Add("knife");
            Console.WriteLine("[knife] was added to inventory. ");
        }
        else if (responsetoitems == "key")
        {
            player.Items.Add("key");
            Console.WriteLine("[key] was added to inventory. ");
        }
        else if (responsetoitems == "none")
        {
            Console.WriteLine("\nYou press on, not paying the items on the table any mind. " +
                              "You feel something watching you intently.");
        }
        
        player.Location = "corridor";
        Transition("\nYou continue towards the corridor. [Press Enter]");
    }

    static void Corridor(Player player)
    {
        Console.Clear();
        Console.WriteLine("You exit the room and find yourself standing in a dark " +
                          "hallway. You can either enter another room on your right " +
                          "side, or continue down the hallway on your left. ");

        string direction = "";
        direction = TwoChoices("\nTo whence do your instincts lead you? [left], [right] ", "left", "right");
        Console.Write("\n");

        if (direction == "right")
        {
            string direction2 = "";
            direction2 = TwoChoices("The door is locked. What will you do? [use key], [go right] ", "use key", "go right");
            
            if (direction2 == "use key" && player.Items.Contains("key"))
            {
                
                player.Location = "lockedroom";
                player.Items.Remove("key");
                Console.Write("[key] removed from inventory.\n\nYou enter through the hitherto unyielding doorway. ");
            }
            else if (direction2 == "use key" && !player.Items.Contains("key"))
            {
                Console.Write(
                    "\nYou attempt to materialize a key into your wanting hands through sheer force of will, " +
                    "but unfortunately the finer mechanisms behind such magics elude you, " +
                    "Defeated, and left with a pulsing headache, you enter the door " +
                    "to your left. Alas. [-5 sanity] ");
                player.Sanity -= 5;

                player.Location = "thirdroom";
            }

            else if (direction2 == "go right" && player.Items.Contains("key"))
            {
                Console.Write("\nWho in their right mind would waste a perfectly fine key on such an unassuming lock? " +
                              "You hold on to your key and head on through the far less consumable-consuming door on your left.");
                player.Location = "thirdroom";
            }

            else
            {
                Console.Write("\nLacking a key, you figure it more reasonable to take the rightmost path instead of " +
                              "wasting time on something impassable.");
                player.Location = "thirdroom";
            }
        } else if (direction == "left")
        {
            Console.Write("\nYou enter the door to your left. ");
        }

        Transition("[Press Enter]");
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