﻿namespace TextAdventure_Solo_2;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the world of disorderly conduct! " +
                  "You are an old delinquent who never learnt to remedy their ways, " +
                  "in a world ruled by the terrible 'redeemed'. You are trapped in a rehabilitation room, " +
                  "and must escape into the outside where once more you may wreak havoc upon the world.");

//Adding extra empty lines with the writeline directly below. Purely for aesthetics.
        Console.WriteLine("\nTell me... ");


        string action1 = "check";
        string action2 = "pick up";
        string action3 = "use"; 
        string action4 = "hit";


        string playername = "";
        do
        {
            playername = Ask("What is your name, delinquent? ");
        } while (!AskYesOrNo($"So, {playername} it is? [yes/ ok] / [no] "));

        Console.WriteLine("");
        Console.WriteLine($"{playername}, you are surrounded by four walls of the most awful beige hue. " +
                          $"A flickering ceiling-lamp is casting a depressing glow upon you surroundings. " +
                          $"You have no items to your name, and are dressed in naught but a grey one-piece, " +
                          $"spotted by what you assume to be bodily fluids. You have no sense of direction. " +
                          $"One solitary door marked by countless scratches is crammed into a corner of the room. " +
                          $"In the middle of the room, directly under the lamp, is a square wooden table with" +
                          $" a box on top.");

//Design decision to display the player's available actions in the writeline for efficiency.
        Console.WriteLine("");
        Console.Write($"What do you do? [{action1}], [{action2}], [{action3}], [{action4}] :");

        Console.ReadLine();Console.WriteLine("");
Console.WriteLine($"{playername}, you are surrounded by four walls of the most awful beige hue. " +
                  $"A flickering ceiling-lamp is casting a depressing glow upon you surroundings. " +
                  $"You have no items to your name, and are dressed in naught but a grey one-piece, " +
                  $"spotted by what you assume to be bodily fluids. You have no sense of direction. " +
                  $"One solitary door marked by countless scratches is crammed into a corner of the room. " +
                  $"In the middle of the room, directly under the lamp, is a square wooden table with" +
                  $" a box on top.");

//Design decision to display the player's available actions in the writeline for efficiency.
Console.WriteLine("");
Console.Write($"What do you do? [{action1}], [{action2}], [{action3}], [{action4}] :");

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

    class Hero
    {
        public string Name = "";
        public int Sanity = 100;
        public List<string> Items = new List<string>();
        public string Location = "newgame";
    }
}