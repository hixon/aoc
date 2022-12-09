using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day9
    {
        public static void Output()
        {
            const string test = "Day9_Test.txt";
            const string prod = "Day9.txt";

            Console.WriteLine("Day 9");
            Console.WriteLine("Part 1 - not 1448, that's too low");
            Console.WriteLine(Part1_Test(test));
            Console.WriteLine(Part1(prod));
            //Console.WriteLine("\nPart 2");
            Console.WriteLine(Part2_Test(test));
            //Console.WriteLine(Part2(prod));
        }

        public static string Part1_Test(string file)
        {
            var input = Utils.OpenFile(file);

            var output = WalkBridge(input);

            return "Unique tail positions: " + output;
        }

        public static string Part1(string file)
        {
            var input = Utils.OpenFile(file);

            var output = WalkBridge(input);

            return "Unique tail positions: " + output;
        }

        public static string Part2_Test(string file)
        {
            var input = Utils.OpenFile(file);

            var output = WalkBridge(input, 10);

            return "Unique tail positions: " + output;
        }

        public static string WalkBridge(string [] data, int knots = 0)
        {
            Dictionary<string, Rope> unique_tail_positions = new Dictionary<string, Rope>();
            string origin = "0,0";
            string head_position = origin;
            string tail_position = origin;

            Rope curr = new Rope()
            {
                Head = origin,
                Tail = origin, 
                HeadX = 0, 
                HeadY = 0, 
                TailX = 0, 
                TailY = 0
            };

            unique_tail_positions.Add(origin, curr);
            

            foreach(var line in data)
            {
                List<string> tail_visited = new List<string>();
                (curr, tail_visited) = Move(curr, line);

                foreach(var item in tail_visited)
                {
                    if (!unique_tail_positions.ContainsKey(item))
                    {
                        unique_tail_positions.Add(item, curr);
                    }
                }
            }

            return unique_tail_positions.Count().ToString();
        }

        public static (Rope, List<string>) Move(Rope position, string movement)
        {
            //move head then move tail
            //if tail is more two spaces behind, its snaps behind head
            Rope curr_position = new Rope();
            curr_position = position;

            List<string> tail_visited = new List<string>();

            var parsed_movedment = movement.Split(' ');
            var direction = parsed_movedment[0];
            var steps = int.Parse(parsed_movedment[1]);

            var currX = position.HeadX;
            var currY = position.HeadY;

            if (direction == "R")
            {
                //move head in position X direction
                for(int x = currX; x <= currX + steps; x++)
                {
                    curr_position.HeadX = x;
                    curr_position.Head = x + "," + curr_position.HeadY;
                    //move head then catch up y if needed
                    if(Math.Abs(x - position.TailX) >= 2)
                    {
                        //adjacent snap
                        if (position.TailY > position.HeadY)
                        {
                            position.TailY--;
                        }
                        else if (position.TailY < position.HeadY)
                        {
                            position.TailY++;
                        }

                        //linear snap
                        if (x > position.TailX)
                        {
                            position.TailX++;
                        }
                        else if (x < position.TailX)
                        {
                            position.TailX--;
                        }

                        //move tail then add position
                        tail_visited.Add(position.TailX + "," + position.TailY);
                        curr_position.Tail = curr_position.TailX + "," + curr_position.TailY;
                    }
                }
            }
            else if(direction == "L")
            {
                //move head in negative X direction
                for (int x = currX; x >= currX-steps; x--)
                {
                    curr_position.HeadX = x;
                    curr_position.Head = x + "," + curr_position.HeadY;
                    //move head then catch up y if needed
                    if (Math.Abs(x - position.TailX) >= 2)
                    {
                        //adjacent snap
                        if (position.TailY > position.HeadY)
                        {
                            position.TailY--;
                        }
                        else if (position.TailY < position.HeadY)
                        {
                            position.TailY++;
                        }

                        //linear snap
                        if (x > position.TailX)
                        {
                            position.TailX++;
                        }
                        else if (x < position.TailX)
                        {
                            position.TailX--;
                        }

                        //move tail then add position
                        tail_visited.Add(position.TailX + "," + position.TailY);
                        curr_position.Tail = curr_position.TailX + "," + curr_position.TailY;
                    }
                }

            }
            else if(direction == "U")
            {
                //move head in position Y direction
                for (int y = currY; y <= currY + steps; y++)
                {
                    curr_position.HeadY = y;
                    curr_position.Head = curr_position.HeadX + "," + y;
                    //move head then catch up y if needed
                    if (Math.Abs(y - position.TailY) >= 2)
                    {
                        //adjacent snap
                        if(position.TailX > position.HeadX)
                        {
                            position.TailX--;
                        }
                        else if(position.TailX < position.HeadX)
                        {
                            position.TailX++;
                        }

                        //linear snap
                        if (y > position.TailY)
                        {
                            position.TailY++;
                        }
                        else if (y < position.TailY)
                        {
                            position.TailY--;
                        }

                        //move tail then add position
                        tail_visited.Add(position.TailX + "," + position.TailY);
                        curr_position.Tail = curr_position.TailX + "," + curr_position.TailY;
                    }
                }
            }
            else if(direction == "D")
            {
                //move head in negative Y direction
                for (int y = currY; y >= currY-steps; y--)
                {
                    curr_position.HeadY = y;
                    curr_position.Head = curr_position.HeadX + "," + y;
                    //move head then catch up y if needed
                    if (Math.Abs(y - position.TailY) >= 2)
                    {
                        //adjacent snap
                        if (position.TailX > position.HeadX)
                        {
                            position.TailX--;
                        }
                        else if (position.TailX < position.HeadX)
                        {
                            position.TailX++;
                        }

                        //linear snap
                        if (y > position.TailY)
                        {
                            position.TailY++;
                        }
                        else if(y < position.TailY)
                        {
                            position.TailY--;
                        }

                        //move tail then add position
                        tail_visited.Add(position.TailX + "," + position.TailY);
                        curr_position.Tail = curr_position.TailX + "," + curr_position.TailY;
                    }
                }
            }
            else
            {
                //ERROR
            }

            return (curr_position, tail_visited);
        }
    }

    public class Rope
    {
        public string Head { get; set; }
        public int HeadX { get; set; }
        public int HeadY { get; set; }
        public string Tail { get; set; }
        public int TailX { get; set; }
        public int TailY { get; set; }
    }
}
