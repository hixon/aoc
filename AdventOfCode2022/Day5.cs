using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day5
    {
        public static void Output()
        {
            Console.WriteLine("Day 5");
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
        }

        public static string Part1()
        {
            var info = Setup();

            List<Stack<string>> crates = SetCrates(info);
            List<Details> instructions = SetInstructions();

            return DoWork(crates, instructions);
        }

        public static string Part2()
        {
            var info = Setup();

            List<Stack<string>> crates = SetCrates(info);
            List<Details> instructions = SetInstructions();

            return DoWork2(crates, instructions);
        }

        public static string DoWork(List<Stack<string>> crates, List<Details> instructions)
        {
            foreach(var item in instructions)
            {
                for(int count = item.NumberOfCratesToMove; count > 0; count--)
                {
                    crates[item.ToCrate].Push(crates[item.FromCrate].Pop());
                }
            }

            //we only care about the top value from each stack, put those in a string and return
            string output = string.Empty;

            foreach(var item in crates)
            {
                if(item.Count > 0)
                {
                    output += item.Pop()[1];
                }
            }

            return output;
        }

        public static string DoWork2(List<Stack<string>> crates, List<Details> instructions)
        {
            foreach (var item in instructions)
            {
                Stack<string> temp = new Stack<string>();

                //move everything to temp
                for (int count = item.NumberOfCratesToMove; count > 0; count--)
                {
                    temp.Push(crates[item.FromCrate].Pop());
                }

                //move everything from temp to final location
                while(temp.Count > 0)
                {
                    crates[item.ToCrate].Push(temp.Pop());
                }
            }

            //we only care about the top value from each stack, put those in a string and return
            string output = string.Empty;

            foreach (var item in crates)
            {
                if (item.Count > 0)
                {
                    output += item.Pop()[1];
                }
            }

            return output;
        }

        public static List<Stack<string>> SetCrates(List<Stack<string>> input)
        {
            List<Stack<string>> updated_crates = new List<Stack<string>>();
            //flip the stack that we initially setup
            //create the stacks in the list
            for(int i = 0; i < input.Count; i++)
            {
                updated_crates.Add(new Stack<string>());

                foreach(var item in input[i])
                {
                    if(item.Trim().Length == 3)
                    {
                        updated_crates[i].Push(item);
                    }
                }
            }

            return updated_crates;
        }

        public static List<Details> SetInstructions()
        {
            string[] data = File.ReadAllLines(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day5.txt");
            List<Details> instructions = new List<Details>();

            foreach(var item in data)
            {
                if (item != "" && item.Length != 35)
                {
                    var split_instructions = item.Split(' ');
                    //fix this so we can work with it
                    Details details = new Details()
                    {
                        Text = item,
                        NumberOfCratesToMove = int.Parse(split_instructions[1]),
                        FromCrate = int.Parse(split_instructions[3]) - 1,
                        ToCrate = int.Parse(split_instructions[5]) - 1
                    };

                    instructions.Add(details);
                }
            }

            return instructions;
        }

        public static List<Stack<string>> Setup()
        {
            string[] data = File.ReadAllLines(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day5.txt");

            List<Stack<string>> cranesOpposite = new List<Stack<string>>();
            
            //cranesOpposite.Add(new Stack<string>());

            foreach (var item in data)
            {
                if(item.Length == 35)
                {
                    int stackIndex = 0;
                    string crate = string.Empty;

                    for (int index = 0; index <= item.Length; index++)
                    {
                        if (index == 0)
                        {
                            crate += item[index];
                        }
                        else if (index > 0 && index % 4 != 3)
                        {
                            crate += item[index];
                        }
                        else
                        {
                            int number;
                            //everything should be set to add to the list
                            if (cranesOpposite.Count == 0 || cranesOpposite.Count <= stackIndex + 1)
                            {
                                cranesOpposite.Add(new Stack<string>());
                            }

                            if (crate != "   ")
                            {
                                //push to stack but we need to know what stack to push to
                                cranesOpposite[stackIndex].Push(crate);
                            }

                            stackIndex++;
                            crate = string.Empty;

                            continue;
                        }
                    }
                }
            }

            return cranesOpposite;
        }
    }

    public class Details
    {
        public string Text { get; set; }
        public int NumberOfCratesToMove { get; set; }
        public int FromCrate { get; set; }
        public int ToCrate { get; set; }
    }
}
