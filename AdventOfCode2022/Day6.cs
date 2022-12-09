using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day6
    {
        public static void Output()
        {
            Console.WriteLine("Day 6");
            Console.WriteLine(Part1_Test());
            Console.WriteLine(Part1());
            Console.WriteLine(Part2_Test());
            Console.WriteLine(Part2());
        }

        public static string[] Setup()
        {
            string[] data = File.ReadAllLines(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day6.txt");

            return data;
        }

        public static string Part1_Test()
        {
            List<string> input = new List<string>();
            input.Add("bvwbjplbgvbhsrlpgdmjqwftvncz");
            input.Add("nppdvjthqldpwncqszvftbrmjlhg");
            input.Add("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg");
            input.Add("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw");

            string output = string.Empty;

            foreach(var item in input)
            {
                output += FindStartSequence(item, 4);
            }

            return output;
        }

        public static string Part2_Test()
        {
            List<string> input = new List<string>();
            input.Add("mjqjpqmgbljsphdztnvjfqwrcgsmlb");
            input.Add("bvwbjplbgvbhsrlpgdmjqwftvncz");
            input.Add("nppdvjthqldpwncqszvftbrmjlhg");
            input.Add("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg");
            input.Add("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw");

            string output = string.Empty;

            foreach (var item in input)
            {
                output += FindStartSequence(item, 14);
            }

            return output;
        }

        public static string FindStartSequence(string input, int size)
        {
            string output = string.Empty;
            //check each sequnce of four characters to make sure they're all unique
            for(int i = 0; i < input.Length; i++)
            {
                string curr_seq = input.Substring(i, size);

                //if all characters are unique return sequence and end of it index
                bool valid_sequence = true;
                if (curr_seq.Distinct().Count() != size)
                {
                    valid_sequence = false;
                }

                if(valid_sequence == true)
                {
                    output += "Sequence: " + curr_seq + " ends at: " + (i + size) + "\n";
                    break;
                }
            }

            return output;
        }

        public static string Part1()
        {
            var info = Setup();

            var output = FindStartSequence(info[0], 4);

            return output;
        }

        public static string Part2()
        {
            var info = Setup();

            var output = FindStartSequence(info[0], 14);

            return output;
        }
    }
}
