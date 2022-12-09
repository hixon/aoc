using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day4
    {
        public static void Output()
        {
            Console.WriteLine("Day 4");
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
        }
        
        public static string Part1()
        {
            List<SectionDetail> detail_info = Setup();

            var overlapping_sections = GetFullOverlaps(detail_info);

            return "Number of FULL overlaps: " + overlapping_sections.Count();
        }

        public static List<SectionDetail> Setup()
        {
            List<SectionDetail> detail_info = new List<SectionDetail>();

            string[] data = File.ReadAllLines(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day4.txt");

            foreach (var section in data)
            {
                var sections = section.Split(',');
                var FirstSection = sections[0].Split('-');
                var SecondSection = sections[1].Split('-');

                SectionDetail curr = new SectionDetail()
                {
                    FullText = section,
                    FirstStart = int.Parse(FirstSection[0]),
                    FirstEnd = int.Parse(FirstSection[1]),
                    FirstLength = GetLength(FirstSection),
                    SecondStart = int.Parse(SecondSection[0]),
                    SecondEnd = int.Parse(SecondSection[1]),
                    SecondLength = GetLength(SecondSection)
                };

                detail_info.Add(curr);
            }

            return detail_info;
        }

        public static string Part2()
        {
            List<SectionDetail> detail_info = Setup();

            var overlapping_sections = GetAnyOverlaps(detail_info);

            return "Number of ANY overlaps: " + overlapping_sections.Count();
        }

        public static List<string> GetAnyOverlaps(List<SectionDetail> input)
        {
            List<string> overlaps = new List<string>();

            foreach (var item in input)
            {
                List<int> workflow = new List<int>();

                //TODO: Refactor since it's a huge copy/paste
                if (item.FirstLength >= item.SecondLength)
                {
                    //push all of first then push/pop second
                    for (int index = item.FirstStart; index <= item.FirstEnd; index++)
                    {
                        workflow.Add(index);
                    }

                    //push/pop 
                    for (int index = item.SecondStart; index <= item.SecondEnd; index++)
                    {
                        if (workflow.Contains(index))
                        {
                            workflow.Remove(index);
                            item.SecondLength -= 1;
                            overlaps.Add(item.FullText);
                            break;
                        }
                        else
                        {
                            workflow.Add(index);
                        }
                    }
                }
                else
                {
                    //push all of second then push/pop first
                    for (int index = item.SecondStart; index <= item.SecondEnd; index++)
                    {
                        workflow.Add(index);
                    }

                    //push/pop 
                    for (int index = item.FirstStart; index <= item.FirstEnd; index++)
                    {
                        if (workflow.Contains(index))
                        {
                            workflow.Remove(index);
                            item.FirstLength -= 1;
                            overlaps.Add(item.FullText);
                            break;
                        }
                        else
                        {
                            workflow.Add(index);
                        }
                    }
                }
            }
            return overlaps;
        }

        public static List<string> GetFullOverlaps(List<SectionDetail> input)
        {
            List<string> overlaps = new List<string>();

            foreach(var item in input)
            {
                List<int> workflow = new List<int>();

                //TODO: Refactor since it's a huge copy/paste
                if(item.FirstLength >= item.SecondLength)
                {
                    //push all of first then push/pop second
                    for(int index = item.FirstStart; index <= item.FirstEnd; index++)
                    {
                        workflow.Add(index);
                    }

                    //push/pop 
                    for (int index = item.SecondStart; index <= item.SecondEnd; index++)
                    {
                        if (workflow.Contains(index))
                        {
                            workflow.Remove(index);
                            item.SecondLength -= 1;
                        }
                        else
                        {
                            workflow.Add(index);
                        }
                    }

                    if (item.SecondLength == 0)
                    {
                        //this overlaps
                        overlaps.Add(item.FullText);
                    }
                }
                else
                {
                    //push all of second then push/pop first
                    for (int index = item.SecondStart; index <= item.SecondEnd; index++)
                    {
                        workflow.Add(index);
                    }

                    //push/pop 
                    for (int index = item.FirstStart; index <= item.FirstEnd; index++)
                    {
                        if (workflow.Contains(index))
                        {
                            workflow.Remove(index);
                            item.FirstLength -= 1;
                        }
                        else
                        {
                            workflow.Add(index);
                        }
                    }

                    if(item.FirstLength == 0)
                    {
                        //this overlaps
                        overlaps.Add(item.FullText);
                    }

                }
            }
            return overlaps;
        }

        public static int GetLength(string[] input)
        {
            if(input[0] == input[1])
            {
                return 1;
            }

            int first = int.Parse(input[0]);
            int second = int.Parse(input[1]);

            return (second - first) + 1;
        }

        public class SectionDetail
        {
            public string FullText { get; set; }
            public int FirstStart { get; set; }
            public int FirstEnd { get; set; }
            public int FirstLength { get; set; }
            public int SecondStart { get; set; }
            public int SecondEnd { get; set; }
            public int SecondLength { get; set; }
            public int TotalLength { get; set; }
        }

    }
}
