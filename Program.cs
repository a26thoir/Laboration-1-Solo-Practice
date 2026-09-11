namespace TextAdventure_Solo_2;

class Program
{

    
    static void Main(string[] args)
    {
        gamestart:

        Console.Clear();
        
        Console.Write("You awaken in a dark room, with only a sliver of light trickling through a crack in the roof. " +
                      "You don't remember how you got here, or what you were doing before you lost consciousness. " +
                      "The only thing you remember is your name... Right?");
        
        Transition("[Press Enter]"); // Check W3schools if anything f's up


        // Instances of classes
        
        Player player = new Player();
        Enemy minotaur = new Enemy();
        minotaur.Name = "Minotaur";
        
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
            
            else if (player.Location == "winscreen")
            {
                WinScreen(player);
                goto gamestart;
            }
            
            else if (player.Location == "losescreen")

            {
                LoseScreen(player);
                goto gamestart;
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

    static int RollD100() // Percentage Chance
    {
        return new Random().Next() % 100 + 1;
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
        if (RollD100() <= player.Crit)
        {
            critchance = 2;

            Console.WriteLine("A critical hit! ");

        }
        else
        {
            critchance = 1;
        }

        output = player.Damage * critchance;

        return output;
    } // Calculates player damage
    
    static void PlayerAttack(Player player, Enemy minotaur)
    {
        int damage = PlayerAttackCalc(player);
        minotaur.Health -= damage;
        Console.WriteLine($"{player.Name} deals {damage} damage. ");
    }

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

            if (playerdefense == 100)
            {
                Console.Write(
                    $"The {minotaur.Name} hits you with its signature ¤~ SWEEP ATTACK ~¤\n{player.Name} takes [{(minotaur.Damage * playerdefense / 100) * 2}] damage. ");
            }
            
            else if (playerdefense == 50)
            {
                Console.Write(
                    $"The {minotaur.Name} hits you with its signature ¤~ SWEEP ATTACK ~¤\nYou successfully [block] some of the incoming damage" +
                    $"\n{player.Name} takes [{(minotaur.Damage * playerdefense / 100) * 2}] damage. ");
            }
            else if (playerdefense == 150)
            {
                Console.Write(
                    $"You prepare to [parry] the enemy attack...\n... but your timing is way off. The {minotaur.Name} hits you extra hard with its " +
                    $"signature ¤~ SWEEP ATTACK ~¤\n{player.Name} takes [{(minotaur.Damage * playerdefense / 100) * 2}] damage. ");
            }
            else
                Console.Write(
                    $"You prepare to [parry] the enemy attack...\n... and your timing is so on! The {minotaur.Name}'s posture breaks and it's dazed" +
                    $" for the rest of the turn\n{player.Name} takes [{(minotaur.Damage * playerdefense / 100) * 2}] damage. ");

        }
        else// normal attack
        {
            player.Health -= (minotaur.Damage * playerdefense / 100);

            if (playerdefense == 100)
            {
                Console.Write(
                    $"The {minotaur.Name} hits you with an ordinary attack. {player.Name} takes [{minotaur.Damage * playerdefense / 100}] damage. ");
            }
            else if (playerdefense == 50)
            {
                Console.Write(
                    $"The {minotaur.Name} hits you with an ordinary attack.\nYou successfully [block] some of the incoming damage" +
                    $"\n{player.Name} takes [{minotaur.Damage * playerdefense / 100}] damage. ");
            }
            else if (playerdefense == 150)
            {
                Console.Write(
                    $"You prepare to [parry] the enemy attack...\n... but your timing is way off. The {minotaur.Name} hits you extra hard with an ordinary attack." +
                    $" {player.Name} takes [{minotaur.Damage * playerdefense / 100}] damage. ");
            }
            else
                Console.Write(
                    $"You prepare to [parry] the enemy attack...\n... and your timing is so on! The {minotaur.Name}'s posture breaks and it's dazed " +
                    $"for the rest of the turn {player.Name} takes [{minotaur.Damage * playerdefense / 100}] damage. ");
        }

        minotaur.ChargeToken = false;

    }

    

    // Rooms

    
    static void NewGame(Player player)
    {
        Console.Clear();
        string playername = "";
        do
        {
            playername = Ask("What is your name? ");
        } while (!AskYesOrNo($"So, {playername} it is? [yes/ ok], [no] "));

        player.Name = playername;
        player.Location = "tableroom";

    }

    static void TableRoom(Player player)
    {
        Console.Clear();
        Console.WriteLine("You are equipped with a weathered walking cane, and lacking any grander " +
                          "purpose for the moment, you might as well try having a look around this place.\n" +
                          "In front of you is a stone table with two items on it," +
                          "a knife and a key. " +
                          "You can only pick up one of these items, because, you realize, you've only" +
                          "got one arm.\n");

        string responsetoitems;
        do
        {
            responsetoitems = Ask("Which item do you choose? [knife], [key], [none]: ");
        } while (responsetoitems != "knife" && responsetoitems != "key" && responsetoitems != "none");

        if (responsetoitems == "knife")
        {
            
            Console.WriteLine($"[{player.Equip} unequipped & removed from inventory]\n[knife] was equipped.");
            player.Equip = "knife";
            player.Damage = 75;
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
                          "hallway. You can either approach a door to your right " +
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
        if (AskYesOrNo($"Do you want it instead of your {player.Equip}? [yes], [no] "))
        {
            Console.Write($"[{player.Equip} removed from inventory]\n[shiny sword added to inventory]\n\n" +
                              "You replace your lacking monster-whacker with the seemingly more potent option " +
                              "in front of you. Encouraged by the nice find, you head on with a cute strut ~");
            player.Equip = "shinysword";
            player.Damage = 100;
        }
        else if (player.Equip == "walkingcane")
        {
            Console.Write($"\nYou've journeyed far with your trusty {player.Equip} at your side, and it's never " +
                          "let you down. While tempting, you leave the shinier counterpart in front of you " +
                          "for some other adventurer to be enamored by. You can feel your wooden sword blushing " +
                          "while you trudge on.\n\n" +
                          "Somewhere behind you, you can also feel the gaze of... something.");
        }

        else

            Console.Write($"\nA Shiny Sword would be much to heavy, and you're not sure you know how to use one " +
                          $"anyways. You figure your {player.Equip} can handle any sticky situation you may be " +
                          $"thrown into.\n\n " +
                          $"Somewhere behind you, you can also feel the gaze of... something.");

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
        Console.Write(
            "You finally exit the dungeon, and shift at the harsh, albeit welcome, sunlight washing over you. " +
            "You take a deep breath to take in the fresh air, and rid yourself of any reminder of the " +
            "dungeon's moldy stench. However, your nose catches an odor quite different to what you " +
            "were expecting...");

        Transition("[Press Enter]");
        
        Console.Write("\nOff in the distance you hear a rumbling sound. A terrifying beast with a man's legs, the " +
                      "torso of a bull, and the smell of unmentionables gone unwashed for weeks, approaches you. " +
                      "A Labyrinth Minotaur. It wields a battleaxe of " +
                      "considerable size, and it becomes immediately apparent that your fight is imminent " +
                      "(mostly because the Minotaur screams 'I'LL KILL YOU AND DEVOUR YOUR CORPSE!!!' at the " +
                      "top of its lungs. It also charges at you, battleaxe raised, eyes red with rage, and so on). " +
                      "[Observational skill] +1 ");

        player.Location = "bossfight";
        Transition("[Press Enter]");
    }

    static void BossFight(Player player, Enemy enemy)
    {
        Console.Clear();

        Console.Write("");
        
        enemy.Health = 1000;
        enemy.Damage = 100;
        
        
        while (player.Health > 0 && enemy.Health > 0)
        {
            playerturn:

            Console.Clear();

            player.Defense = 100;
            player.Block = 0;

            Console.Write($"{player.Name} health: [{player.Health}]\n\n{enemy.Name} health: [{enemy.Health}]\n\n");
            
            string action = FourChoices("Choose an action:\n[attack], [block]\n[parry], [jump] ",
                "attack", "block", "parry", "jump");
            if (ChargeCheck(enemy) == true && action == "jump")
            {
                player.Crit = 100; //On a D100ROLL, a 100 crit value = guaranteed crit, which we want for this attack
                Console.WriteLine("At the last second, you swiftly jump over the minotaur's sweeping strike. " +
                                  "In mid-air, you bring your weapon down upon your perplexed assaulter " +
                                  "for a mighty, critical, ¤~ JUMP ATTACK ~¤");
                PlayerAttack(player, enemy);
                if (player.Items.Contains("blessedamulet")) //Reset Crit modifier to respective probabilities depending on items
                {
                    player.Crit = 33;
                }
                else if (player.Items.Contains("cursedamulet"))
                {
                    player.Crit = 10;
                }
                else
                {
                    player.Crit = 15; 
                }
                enemy.ChargeToken = false; //we don't want the minotaur to get stuck in a sweep attack loop now do we?
                Console.Write($"\n" +
                              $"{enemy.Name} health: [{enemy.Health}]\n\n");
                Transition("\n\nThe Minotaur is dazed for the remainder of its round. [Press Enter]");

                goto playerturn;
                
            }
            else if (action == "attack")
            {
                Console.Write("\nYou strike the enemy. ");
                PlayerAttack(player, enemy);
                Console.Write($"\n{enemy.Name} health: [{enemy.Health}]\n\n");

                if (enemy.Health <= 0)
                {
                    break;
                }

            }
            
            else if (action == "block" || action == "parry")
                
            {
                player.Block = PlayerBlockCalc(action);
                player.Defense = PlayerDefCalc(player);
            }

            else
            {
                Console.WriteLine("You jump. Good for you. The Minotaur puts down its battleaxe, applauds you, " +
                              "picks the battleaxe up again, and continues its latest project of dismembering you. \n");
            }
            
            // Enemy Turn

            if (enemy.ChargeToken == true)
            {
                goto enemyattack;
            }

            if (EnemyAttackSelection() == true)
            {
                Console.Write($"The {enemy.Name} lifts its battleaxe backwards and starts preparing for a powerful " +
                                  $"sweeping attack. Watch out! ");
                
                enemy.ChargeToken = true;

                Transition("[Press Enter]");

                goto playerturn;

            }
            
            enemyattack:
            
            EnemyAttack(enemy, player, player.Defense);
            
            Console.Write($"\n{player.Name} health: [{player.Health}]\n\n");
            Transition("[Press Enter]");

            if (player.Health <= 0)
            {
                break;
            }
            

        }

        if (enemy.Health <= 0)
        {
            player.Location = "winscreen";
            Transition("YOU WON! [Press Enter]");
        }

        else player.Location = "losescreen";

        Transition("YOU DIED! [Press Enter]");
    }

    static void WinScreen(Player player)
    {
        Console.Clear();
        Transition("Magnificent! You escaped the dungeon and slew the beast. But it was all just a dream... [Press Enter]");
    }

    static void LoseScreen(Player player)
    {
        Console.Clear();
        Transition("You were defeated by the dungeon, and... Yeah, well, you're dead. But it was all just a dream... [Press Enter]");
    }

    //Classes

    class Player
    {
        public string Name = "";
        public int Health = 500;
        public int Damage = 50;
        public int Block = 0;
        public int Defense = 100;
        public int Crit = 15;
        public string Equip = "weathered cane";
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