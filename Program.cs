namespace TextAdventure_Solo_2;

class Program
{

    
    static void Main(string[] args)
    {
        Console.Write("Welcome to the dark dingy dungeon.");
        
        Transition("[Press Enter]"); // Check W3schools if anything f's up


        // Instances of classes
        
        Player player = new Player();
        Enemy minotaur = new Enemy();
        
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
            else if (player.Location == "backoutside")
            {
                BackOutside(player);
            }
            
            else if (player.Location == "bossfight")
            {
                BossFight(player, minotaur);
            }
            else
            {
                Console.Error.WriteLine(
                    $"You forgot to implement '{player.Location}'!");
            }
        }
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

    static string FourChoices(string question1, string choice1, string choice2, string choice3, string choice4)
    {
        string answer1 = "";
        do
        {
            Console.Write(question1) ;
            answer1 = Console.ReadLine().Trim().ToLower();
        } while (answer1 != choice1 && answer1 != choice2 && answer1 != choice3 && answer1 != choice4);

        return answer1;
    }

    static int RollD6() // Chance in 16, 33, 49, 66 & 83%
    {
        return new Random().Next() % 6 + 1;
    }

    static int RollD4() // Chance in 25, 50 & 75%
    {
        return new Random().Next() % 4 + 1;
    }

    static int PlayerAttackCalc(Player player)
    {
        int output;
        int critchance;
        if (RollD6() <= player.Crit)
        {
            critchance = 2;
        }
        else
        {
            critchance = 1;
        }

        output = player.Damage * critchance;

        return output;
    } // Calculates player damage

    static int PlayerBlockCalc(string action1) // Checks if player uses block or parry, and calculates blockrate
    {
        int blockrate;
        if (action1 == "parry")
        {
            if (RollD4() <= 2)
            {
                blockrate = 100;
            }
            else
            {
                blockrate = -50;
            }
        }
        else if (action1 == "block")
        {
            blockrate = 50;
        }
        else
        {
            blockrate = 0;
        }

        return blockrate;
        
    }

    static int PlayerDefCalc(Player player)
    {
        int playerdefense;
        if (player.Block == 100)
        {
            playerdefense = 0;
        }
        else if (player.Block == 50)
        {
            playerdefense = 50;
        }
        else if (player.Block == 0)
        {
            playerdefense = 100;
        }
        else
        {
            playerdefense = 150;
        }
        return playerdefense;
        

    }

    static bool ChargeCheck(Enemy enemy)
    {
        if (enemy.ChargeToken == true)
        {
            enemy.ChargeToken = false;
            return true;
        }
        else
        {
            return false;
        }
    } // Checks if Minotaur is charging attack

    static bool EnemyAttackSelection() // Checks which out of 2 attacks an enemy will choose
    {
        if (RollD6() <= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    static void EnemyAttack(Enemy minotaur, Player player, int playerdefense)
    {

        if (ChargeCheck(minotaur) == true)//sweep attack
        {
            player.Health -= (minotaur.Damage * playerdefense / 100) * 2;
        }
        else// normal attack
        {
            player.Health -= (minotaur.Damage * playerdefense / 100);
        }
    }

    static void PlayerAttack(Player player, Enemy minotaur)
    {
        int damage = PlayerAttackCalc(player);
        minotaur.Health -= damage;
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
            direction2 = TwoChoices("The door is locked. What will you do? [use key], [go left] ", "use key", "go left");
            
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
                    "to your left. Alas. [-5 health] ");
                player.Health -= 5;

                player.Location = "thirdroom";
            }

            else if (direction2 == "go left" && player.Items.Contains("key"))
            {
                Console.Write("\nWho in their right mind would waste a perfectly fine key on such an unassuming lock? " +
                              "You hold on to your key and head on through the far less consumable-consuming door on your left.");
                player.Location = "thirdroom";
            }

            else
            {
                Console.Write("\nLacking a key, you figure it more reasonable to take the leftmost path instead of " +
                              "wasting time on something impassable.");
                player.Location = "thirdroom";
            }
        } else if (direction == "left")
        {
            Console.Write("\nYou enter the door to your left. ");
            player.Location = "thirdroom";
        }

        Transition("[Press Enter]");
    }

    static void LockedRoom(Player player)
    {
        Console.Clear();
        Console.WriteLine("Inside the locked room you find a shiny sword!\n");
        if (AskYesOrNo("Do you want it instead of your wooden sword? [yes], [no] "))
        {
            player.Items.Remove("woodensword");
            player.Items.Add("shinysword");
            player.Equip = "shinysword)";
            Console.Write("[wooden sword removed from inventory]\n[shiny sword added to inventory]\n\n" +
                              "You replace your lacking monster-whacker with the seemingly more potent option " +
                              "in front of you. Encouraged by the nice find, you head on with a cute strut ~");
        }
        else
        {
            Console.Write("\nYou've journeyed far with your trusty wooden sword at your side, and it's never " +
                          "let you down. While tempting, you leave the shinier counterpart in front of you " +
                          "for some other adventurer to be enamored by. You can feel your wooden sword blushing " +
                          "while you trudge on.\n\n" +
                          "Somewhere behind you, you can also feel the gaze of... something.");
        }

        Transition("[Press Enter]");
        player.Location = "thirdroom";
    }
    
    static void ThirdRoom(Player player)
    {
        Console.Clear();
        Console.WriteLine("On the floor before you lies a lifeless corpse. \n" +
                          "Its hand is clasped around something shiny. \n");
        if (AskYesOrNo("Do you loot the corpse or leave it? [yes], [no] "))
        {
            Console.WriteLine("You pick up an old silver necklace.");
            if (RollD6() >= 3)
            {
                Console.WriteLine("[blessed amulet] added to inventory. \n\nA warm feeling spreads over your body. " +
                                  "You can feel your fortune increasing! ");
                player.Items.Add("blessed amulet");
            }
            else
            {
                    Console.WriteLine("[cursed amulet] added to inventory.\n\nA cold shiver runs down your spine. " +
                                      "You feel as if your luck has left you... ");
                    player.Items.Add("cursed amulet");
                    
            }
        }

        player.Location = "backoutside";

        Console.WriteLine("You leave the corpse and continue into the next room.");
        Transition("[Press Enter]");
        
    }

    static void BackOutside(Player player)
    {
        Console.Clear();
        Console.Write("You finally exit the dungeon. A whiff of fresh air runs along your cheeks. However, " +
                      "off in the distance you hear a rumbling sound, and a far more sinister air soon " +
                      "assaults your senses. You see the image of a hulking minotaur approach.");

        player.Location = "bossfight";
        Transition("[Press Enter]");
    }

    static void BossFight(Player player, Enemy enemy)
    {
        Console.Clear();
        enemy.Health = 1000;
        enemy.Damage = 100;
        
        

        while (player.Health > 0 && enemy.Health > 0)
        {
            string action = FourChoices("[attack], [block], [parry], [jump]",
                "attack", "block", "parry", "jump");
            if (ChargeCheck(enemy) == true)
            {
                if (action == "jump")
                {
                    player.Crit = 6;
                    PlayerAttack(player, enemy);
                    player.Crit = 1;
                    Console.WriteLine("");
                }
                
                else if (action == "attack")
                {
                    PlayerAttack(player, enemy);
                    Console.WriteLine("");
                }
                else if (action == "block")
                    

            }

            EnemyAttack(enemy, player, );
            
                
            

        }

        Transition("[Press Enter]");
    }

    //Classes

    class Player
    {
        public string Name = "";
        public int Health = 500;
        public int Damage = 100;
        public int Block = 0; 
        public int Crit = 1;
        public string Equip = "woodensword";
        public List<string> Items = new List<string>();
        public string Location = "newgame";
    }

    class Enemy
    {
        public string Name = "";
        public int Health = 500;
        public int Damage = 50;
        public int Block = 0;
        public bool ChargeToken = false;
    }

}