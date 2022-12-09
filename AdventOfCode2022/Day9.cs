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
            Console.WriteLine("Part 1");
            Console.WriteLine(Part1_Test(test));
            //Console.WriteLine(Part1(prod));
            //Console.WriteLine("\nPart 2");
            //Console.WriteLine(Part2_Test(test));
            //Console.WriteLine(Part2(prod));
        }

        public static string Part1_Test(string file)
        {
            var input = Utils.OpenFile(file);

            //var output = FindValidTrees(input);

            return null;
        }
    }
}
