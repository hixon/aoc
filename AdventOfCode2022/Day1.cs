using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day1
    {
        public static void Output()
        {
            Console.WriteLine("Advent of Code 2022 - Day 1");
            var output = Day1_1();
            Console.WriteLine(FindMax(output));
            Console.WriteLine();
            Console.WriteLine(FindTop3Max(output));
            Console.WriteLine();
            Console.WriteLine("Total calories of top 3 elves: " + FindTop3Max_Calories(output));
        }

static Dictionary<string, int> Day1_1()
        {
            Dictionary<string, int> ElfTotals = new Dictionary<string, int>();

            //read in the input file
            string[] data = File.ReadAllLines(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day1.txt");
            int currSum = 0;
            int currElfNumber = 1;

            //goes through the full set of 
            for (int elfIndex = 0; elfIndex < data.Length; elfIndex++)
            {
                if (data[elfIndex] != String.Empty)
                {
                    for (int currElf = 0; currElf < data.Length; currElf++)
                    {
                        if (data[currElf] != String.Empty)
                        {
                            currSum += Int32.Parse(data[currElf]);
                            elfIndex++;
                        }
                        else
                        {
                            ElfTotals.Add("Elf " + currElfNumber, currSum);
                            elfIndex = currElf;
                            currElfNumber += 1;
                            currSum = 0;
                            continue;
                        }
                    }
                }
            }

            return ElfTotals;
        }

        static string FindMax(Dictionary<string, int> data)
        {
            int max = 0;

            foreach (var item in data)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                }
            }

            var output = data.Where(d => d.Value == max).FirstOrDefault();

            return output.Key.ToString() + ":" + output.Value.ToString();
        }

        static string FindMaxExcluding(Dictionary<string, int> data, List<int> exclude)
        {
            int max = 0;

            foreach (var item in data)
            {
                if (!exclude.Contains(item.Value))
                {
                    if (item.Value > max)
                    {
                        max = item.Value;
                    }
                }
            }

            var output = data.Where(d => d.Value == max).FirstOrDefault();

            return output.Key.ToString() + ":" + output.Value.ToString();
        }

        static string FindTop3Max(Dictionary<string, int> data)
        {
            List<int> excludelList = new List<int>();

            var top1 = string.Empty;
            var top2 = string.Empty;
            var top3 = string.Empty;

            top1 = FindMax(data);

            excludelList.Add(Int32.Parse(top1.Split(':')[1]));

            top2 = FindMaxExcluding(data, excludelList);

            excludelList.Add(Int32.Parse(top2.Split(':')[1]));

            top3 = FindMaxExcluding(data, excludelList);

            return top1 + "\n" + top2 + "\n" + top3;
        }

        static int FindTop3Max_Calories(Dictionary<string, int> data)
        {
            List<int> excludelList = new List<int>();

            var top1 = string.Empty;
            var top2 = string.Empty;
            var top3 = string.Empty;

            int total_calories = 0;

            top1 = FindMax(data);

            excludelList.Add(Int32.Parse(top1.Split(':')[1]));

            top2 = FindMaxExcluding(data, excludelList);

            excludelList.Add(Int32.Parse(top2.Split(':')[1]));

            top3 = FindMaxExcluding(data, excludelList);

            return Int32.Parse(top1.Split(':')[1]) +
                Int32.Parse(top2.Split(':')[1]) +
                Int32.Parse(top3.Split(':')[1]);
        }
    }
}
