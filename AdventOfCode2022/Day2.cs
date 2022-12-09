using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day2
    {
        public static Dictionary<string, string> GameValues = new Dictionary<string, string>();
        public static Dictionary<string, int> ChoiceValues = new Dictionary<string, int>();
        public static void Output()
        {
            Console.WriteLine("Advent of Code 2022 - Day 2");
            
            GameValues.Add("A", "Rock");
            GameValues.Add("X", "Rock");
            GameValues.Add("B", "Paper");
            GameValues.Add("Y", "Paper");
            GameValues.Add("C", "Scissors");
            GameValues.Add("Z", "Scissors");

            ChoiceValues.Add("A", 1);
            ChoiceValues.Add("B", 2);   
            ChoiceValues.Add("C", 3); 

            //var output = Day2_1();
            //Console.WriteLine("Total Score: " + output);

            var output2 = Day2_2();
            Console.WriteLine("Total Score (strat 2): " + output2);
        }

        static string Day2_1()
        {
            string[] data = File.ReadAllLines(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day2.txt");
            int score = 0;

            foreach(var item in data)
            {
                string[] game = item.Split(' ');
                int outcome = GetGameOutcome(game);
                int player_choice = GetPlayerChoice(game[1]);

                int curr_game = outcome + player_choice;
                score += curr_game;
                Console.WriteLine("Game score: " + curr_game);    
            }

            return score.ToString();
        }

        static int GetGameOutcome (string[] input)
        {
            string p1_value = GameValues.Where(g => g.Key == input[0]).Select(g => g.Value).First();
            string p2_value = GameValues.Where(g => g.Key == input[1]).Select(g => g.Value).First();

            if((p1_value == "Rock" && p2_value == "Scissors") ||
                (p1_value =="Scissors" && p2_value == "Paper") ||
                (p1_value == "Paper" && p2_value == "Rock"))
            {
                //loss
                return 0; 
            }
            else if ((p2_value == "Rock" && p1_value == "Scissors") ||
                (p2_value == "Scissors" && p1_value == "Paper") ||
                (p2_value == "Paper" && p1_value == "Rock"))
            {
                //win
                return 6;
            }
            else
            {
                //tie
                return 3;
            }
        }

        static int GetGameOutcome2(string[] input)
        {
            //string p1_value = GameValues.Where(g => g.Key == input[0]).Select(g => g.Value).First();
            string p2_value = input[1];

            if(p2_value == "X")
            {
                return 0;
            }
            else if (p2_value == "Y")
            {
                return 3;
            }
            else
            {
                return 6;
            }
        }

        static int GetPlayerChoice(string key)
        {
            string choice = GameValues[key];

            if(choice == "Rock")
            {
                return 1;
            }
            else if (choice == "Paper")
            {
                return 2; 
            }
            else
            {
                return 3;
            }
        }

        static int GetPlayerChoice2(string[] game)
        {
            var p1 = game[1];

            if(game[1] == "Y")
            {
                return ChoiceValues[game[0]];
            }
            else if (game[1] == "X")
            {
                //loss
                return Loss(game[0]);
            }
            else  //game[1] == "Z"
            {
                //win
                return Win(game[0]);
            }
        }

        static int Loss(string opponent)
        {
            if(opponent == "A")  //rock beats scissors
            {
                return 3; 
            }
            else if (opponent == "B") //paper beats rock
            {
                return 1;
            }
            else //scissors beats paper
            {
                return 2;
            }
        }

        static int Win(string opponent)
        {
            if (opponent == "A")  //paper beats rock
            {
                return 2;
            }
            else if (opponent == "B") //scissors beats paper
            {
                return 3;
            }
            else //rock beats scissors
            {
                return 1;
            }
        }

        static int Day2_2()
        {
            /* X = lose
             * Y = draw
             * Z = win
             */

            string[] data = File.ReadAllLines(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day2.txt");
            int score = 0;

            foreach (var item in data)
            {
                string[] game = item.Split(' ');
                int outcome = GetGameOutcome2(game);
                int player_choice = GetPlayerChoice2(game);

                int curr_game = outcome + player_choice;
                score += curr_game;
                Console.WriteLine("Game score: " + curr_game);
            }

            return score;
        }
    }
}

/* Game Rules
 * column 0 is opponent choice
 * column 1 is your choice
 * 
 * A - Rock - X
 * B - Paper - Y
 * C - Scissor - Z
 * 
 * Rock > Scissor
 * Scissor > Paper
 * Paper > Rock
 * Both are the same is a tie
 * 
 * win = 6 points
 * loss = 0 points
 * draw = 3 points
 */