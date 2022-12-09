using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day8
    {
        public static void Output()
        {
            const string test = "Day8_Test.txt";
            const string prod = "Day8.txt";

            Console.WriteLine("Day 8");
            Console.WriteLine("Part 1");
            Console.WriteLine(Part1_Test(test));
            Console.WriteLine(Part1(prod));
            Console.WriteLine("\nPart 2");
            Console.WriteLine(Part2_Test(test));
            Console.WriteLine(Part2(prod));
        }

        public static string Part1_Test(string file)
        {
            var input = Utils.OpenFile(file);

            var output = FindValidTrees(input);

            return "# Visible Spots: " + output;
        }

        public static string Part1(string file)
        {
            var input = Utils.OpenFile(file);

            var output = FindValidTrees(input);

            return "# Visible Spots: " + output;
        }

        public static string Part2_Test(string file)
        {
            var input = Utils.OpenFile(file);

            var output = GetHighestScenicScore(input);

            return "# Visible Spots: " + output;
        }

        public static string Part2(string file)
        {
            var input = Utils.OpenFile(file);

            var output = GetHighestScenicScore(input);

            return "# Visible Spots: " + output;
        }

        public static string GetHighestScenicScore(string[] input)
        {
            List<TreeHouse> max = new List<TreeHouse>();
            //{
            //    North = 0,
            //    South = 0,
            //    West = 0,
            //    East = 0,
            //    Position = "",
            //    Size = -1
            //};

            bool isValid = false;

            //look north
            for (int x = 0; x < input[0].Length; x++)
            {
                for (int y = 0; y < input.Length; y++)
                {
                    if (!isEdge(x, y, input.Length - 1, input[x].Length - 1))
                    {
                        //should never be an edge
                        //check north
                        int north = 0;
                        for (int n = x - 1; n >= 0; n--)
                        {
                            if (input[n][y] >= input[x][y])
                            {
                                north += 1;
                                break;
                            }
                            else
                            {
                                north += 1;
                            }
                        }

                        //if (north == true)
                        //{
                        //    AddPosition(visible, x, y, input[x][y]);
                        //    continue;
                        //}

                        //check south
                        int south = 0;
                        for (int n = x + 1; n < input[0].Length; n++)
                        {
                            if (input[n][y] >= input[x][y])
                            {
                                south += 1;
                                break;
                            }
                            else
                            {
                                south += 1;
                            }
                        }

                        //if (south == true)
                        //{
                        //    AddPosition(visible, x, y, input[x][y]);
                        //    continue;
                        //}

                        //check north
                        int west = 0;
                        for (int n = y - 1; n >= 0; n--)
                        {
                            if (input[x][n] >= input[x][y])
                            {
                                west += 1;
                                break;
                            }
                            else
                            {
                                west += 1;
                            }
                        }

                        //if (west == true)
                        //{
                        //    AddPosition(visible, x, y, input[x][y]);
                        //    continue;
                        //}

                        //check west
                        int east = 0;
                        for (int n = y + 1; n < input[x].Length; n++)
                        {
                            if (input[x][n] >= input[x][y])
                            {
                                east += 1;
                                break;
                            }
                            else
                            {
                                east += 1;
                            }
                        }

                        //if (east == true)
                        //{
                        //    AddPosition(visible, x, y, input[x][y]);
                        //    continue;
                        //}

                        if(north > 0 && south > 0 && west > 0 && east > 0)
                        {
                            var size = north * south * west * east;

                            //if(size > max.Size)
                            //{
                            var position = x + "," + y;

                            var newPotentialLocation = new TreeHouse()
                            {
                                North = north,
                                South = south,
                                West = west,
                                East = east,
                                Size = size,
                                Position = position
                            };

                            max.Add(newPotentialLocation);
                            //}

                        }
                    }
                }
            }

            var topchoice = max.OrderByDescending(t => t.Size).First();
            return "North: " + topchoice.North + " South: " + topchoice.South + "\n"
                + "West: " + topchoice.West + " East: " + topchoice.East + "\n"
                + "Position: " + topchoice.Position + " Size: " + topchoice.Size;
        }
        
        public static string FindValidTrees(string[] input)
        {
            //all trees on perimeter are visible 
            //look in one direction and if it's valid skip the other's 
            //all we really care about here is that one direction is valid

            Dictionary<string, Location> visible = new Dictionary<string, Location>();
            bool isValid = false;

            //look north
            for(int x = 0; x < input[0].Length; x++) { 
                for(int y = 0; y < input.Length; y++)
                {
                    if(isEdge(x, y, input.Length  - 1, input[x].Length - 1))
                    {
                        //add to visible spots
                        AddPosition(visible, x, y, input[x][y]);
                    }
                    else
                    {
                        //should never be an edge
                        //check north
                        bool north = false;
                        for(int n = x-1; n >= 0; n--)
                        {
                            if(input[n][y] >= input[x][y])
                            {
                                north = false;
                                break;
                            }
                            else
                            {
                                north = true;
                            }
                        }

                        if(north == true)
                        {
                            AddPosition(visible, x, y, input[x][y]);
                            continue;
                        }

                        //check south
                        bool south = false;
                        for (int n = x + 1; n < input[0].Length; n++)
                        {
                            if (input[n][y] >= input[x][y])
                            {
                                south = false;
                                break;
                            }
                            else
                            {
                                south = true;
                            }
                        }

                        if (south == true)
                        {
                            AddPosition(visible, x, y, input[x][y]);
                            continue;
                        }

                        //check north
                        bool west = false;
                        for (int n = y - 1; n >= 0; n--)
                        {
                            if (input[x][n] >= input[x][y])
                            {
                                west = false;
                                break;
                            }
                            else
                            {
                                west = true;
                            }
                        }

                        if (west == true)
                        {
                            AddPosition(visible, x, y, input[x][y]);
                            continue;
                        }

                        //check west
                        bool east = false;
                        for (int n = y + 1; n < input[x].Length; n++)
                        {
                            if (input[x][n] >= input[x][y])
                            {
                                east = false;
                                break;
                            }
                            else
                            {
                                east = true;
                            }
                        }

                        if (east == true)
                        {
                            AddPosition(visible, x, y, input[x][y]);
                            continue;
                        }
                    }
                }
            }

            return visible.Count.ToString();
        }

        public static bool isEdge(int x, int y, int xmax, int ymax)
        {
            //and edge means that either x = 0 or xmax 
            //or y = 0 or ymax

            if(x == 0 || x == xmax)
            {
                return true;
            }
            else if (y == 0 || y == ymax)
            {
                return true;
            }

            return false;
        }

        public static void AddPosition(Dictionary<string, Location> curr, int x, int y, int value)
        {
            var position = x + "," + y;
            if (!curr.ContainsKey(position))
            {
                curr.Add(position, new Location()
                {
                    X = x,
                    Y = y,
                    Size = value
                });
            }
        }
    }

    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
    }

    public class TreeHouse
    {
        public int North { get; set; }
        public int South { get; set; }
        public int East { get; set; }
        public int West { get; set; }
        public string Position { get; set; }
        public int Size { get; set; }
    }
}
