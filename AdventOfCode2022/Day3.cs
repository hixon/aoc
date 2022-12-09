using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day3
    {
        public static void Output()
        {
            Console.WriteLine("Day 3");

            Console.WriteLine(Day3_1());
            Console.WriteLine(Day3_2());
        }

        static string Day3_1()
        {
            string[] data = File.ReadAllLines(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day3.txt");

            List<Pack> split_input = SetPack(data);
            List<PackDetails> matches_value = new List<PackDetails>();

            foreach(var pack in split_input)
            {
                foreach(var letter in pack.First)
                {
                    if (pack.Second.Contains(letter))
                    {
                        matches_value.Add(
                            new PackDetails
                            {
                                letter = letter,
                                value = GetLetterValue(letter)
                            });
                        break;
                    }
                }
            }

            return "Total: " + SumOfMatchLetters(matches_value);
        }

        static string Day3_2()
        {
            string[] data = File.ReadAllLines(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day3.txt");

            List<Pack> split_input = SetPack2(data);
            List<PackDetails> matches_value = new List<PackDetails>();

            foreach (var pack in split_input) //teams of three
            {
                foreach (var letter in pack.First)
                {
                    if (pack.Second.Contains(letter) && pack.Third.Contains(letter))
                    {
                        matches_value.Add(
                            new PackDetails
                            {
                                letter = letter,
                                value = GetLetterValue(letter)
                            });
                        break;
                    }
                }
            }

            return "Total: " + SumOfMatchLetters(matches_value);
        }

        static string SumOfMatchLetters(List<PackDetails> input)
        {
            int sum = 0; 

            foreach(var item in input)
            {
                sum += item.value;
            }

            return sum.ToString();
        }

        static int GetLetterValue(char input)
        {
            //lower case values are a-z 1-26, ascii 97-122
            //uppercase values are A-Z 27-52, ascii 65-90

            if((int) input > 90)
            {
                return ((int)input) - 96;
            }
            else
            {
                return ((int)input) - 38;
            }
        }

        static List<Pack> SetPack(string[] input)
        {
            List<Pack> output = new List<Pack>();   

            foreach(var item in input)
            {
                Pack curr = new Pack
                {
                    First = item.Substring(0, item.Length / 2),
                    Second = item.Substring(item.Length / 2)
                };

                output.Add(curr);
            }

            return output;
        }

        static List<Pack> SetPack2(string[] input)
        {
            List<Pack> output = new List<Pack>();

            Pack curr = new Pack();
            for (int index = 0; index < input.Length; index++)
            {
                if(index % 3 == 0)
                {
                    curr.First = input[index];
                }
                else if(index % 3 == 1)
                {
                    curr.Second = input[index];
                }
                else
                {
                    curr.Third = input[index];
                    output.Add(curr);

                    curr = new Pack();
                }
                
            }

            return output;
        }
    }

    public class Pack
    {
        public string First { get; set; }
        public string Second { get; set; }
        public string Third { get; set; }
    }

    public class PackDetails
    {
        public char letter { get; set; }
        public int value { get; set; }
    }
}
