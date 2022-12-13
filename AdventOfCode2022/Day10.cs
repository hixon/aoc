using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day10
    {
        public static void Output()
        {
            const string test = "Day10_Test.txt";
            const string prod = "Day10.txt";

            Console.WriteLine("Day 10");
            Console.WriteLine("Part 1");
            Console.WriteLine(Part1_Test(test));
            Console.WriteLine(Part1(prod));
            Console.WriteLine("\nPart 2");
            Console.WriteLine(Part2_Test(test));
            //Console.WriteLine(Part2(prod));
        }

        public static string Part1_Test(string file)
        {
            var input = Utils.OpenFile(file);

            var output = CycleValue(input);

            int total = 0;
            foreach (var item in output)
            {
                total += item.Value.xvalue;
            }

            return "total: " + total;
        }

        public static string Part1(string file)
        {
            var input = Utils.OpenFile(file);

            var output = CycleValue(input);

            int total = 0;
            foreach (var item in output)
            {
                total += item.Value.xvalue;
            }

            return "total: " + total;
        }

        public static string Part2_Test(string file)
        {
            var input = Utils.OpenFile(file);

            var output = CycleValue(input);

            int total = 0;
            foreach (var item in output)
            {
                total += item.Value.xvalue;
            }

            return "total: " + total;
        }

        public static Dictionary<int, Cycle> CycleValue(string[] input)
        {
            Dictionary<int, Cycle> CycleLog = new Dictionary<int, Cycle>();
            List<int> toCheck = new List<int>();
            toCheck.Add(20);
            toCheck.Add(60);
            toCheck.Add(100);
            toCheck.Add(140);
            toCheck.Add(180);
            toCheck.Add(220);

            int cycle = 0;
            int value = 1;

            int previous_cycle = 0;
            int previous_value = 0;

            foreach(var item in input)
            {
                var split_instruction = item.Split(' ');
                var instruction = split_instruction[0];

                //save off old cycle and value in case we need it
                previous_cycle = cycle;
                previous_value = value;

                if (instruction == "addx")
                {
                    cycle += 2;
                    value += int.Parse(split_instruction[1]);
                }
                else if(instruction == "noop")
                {
                    cycle += 1;
                }

                //only save off values from 20th then every 40 after
                if(toCheck.Count() > 0 && cycle >= toCheck[0])
                {
                    int cycle_to_log = toCheck[0];

                    if (cycle >= cycle_to_log)
                    {
                        Cycle curr = new Cycle()
                        {
                            cycle = previous_cycle,
                            value = previous_value,
                            xvalue = previous_value * cycle_to_log
                        };
                        CycleLog.Add(cycle, curr);

                        toCheck.Remove(cycle_to_log);
                    }
                    else if (cycle < cycle_to_log)
                    {
                        Cycle curr = new Cycle()
                        {
                            cycle = cycle,
                            value = value,
                            xvalue = value * cycle_to_log
                        };
                        CycleLog.Add(cycle, curr);

                        toCheck.Remove(cycle_to_log);
                    }
                }
            }

            return CycleLog;
        }
    }

    public class Cycle
    {
        public int cycle;   
        public int value;
        public int xvalue;
    }
}
