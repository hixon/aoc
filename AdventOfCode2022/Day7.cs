using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day7
    {
        public static void Output()
        {
            Console.WriteLine("Day 7");
            Console.WriteLine("Part 1");
            Console.WriteLine(Part1_Test());
            Console.WriteLine(Part1());
            Console.WriteLine("\nPart 2");
            Console.WriteLine(Part2());
        }

        public static string Part1_Test()
        {
            const int MAX_DIR_SIZE = 100000;
            var input = Setup(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day7_Test.txt");

            var output = GetDirectoriesUnder(input, MAX_DIR_SIZE);

            return output;
        }

        public static string Part1()
        {
            const int MAX_DIR_SIZE = 100000;
            var input = Setup(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day7.txt");

            var output = GetDirectoriesUnder(input, MAX_DIR_SIZE);

            return output;
        }

        public static string Part2()
        {
            const int MAX_DIR_SIZE = 100000;
            var input = Setup(@"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/Day7.txt");

            var output = GetDirectoriesToFree(input, MAX_DIR_SIZE);

            return output;
        }

        public static string[] Setup(string path)
        {
            var input = File.ReadAllLines(path);

            return input;
        }

        public static string GetDirectoriesToFree(string[] data, int max_size)
        {
            int total_space = 70000000;
            int space_to_delete = 30000000;

            string output = string.Empty;

            Stack<FS_Details> stack = new Stack<FS_Details>();
            Stack<FS_Details> final_countdown = new Stack<FS_Details>();

            stack.Push(new FS_Details()
            {
                Path = "/",
                Size = 0
            });

            int total = 0;
            List<int> curr_total = new List<int>();

            foreach (var line in data)
            {
                if (line == "$ cd /" || line == "$ ls")
                {
                    continue;
                }

                if (line.Contains("$ cd"))
                {
                    var call = line.Substring(5);

                    if (call == "..")
                    {
                        //pop the stack 
                        FS_Details curr = stack.Pop();

                        if (curr.Size <= max_size)
                        {
                            total += curr.Size;
                            //Console.WriteLine(curr.Path + ":\t" + curr.Size);
                        }

                        //add value that was popped the stack's previous item
                        stack.Peek().Size += curr.Size;
                        final_countdown.Push(curr);
                    }
                    else
                    {
                        //push onto stack
                        stack.Push(new FS_Details()
                        {
                            Path = call,
                            Size = 0
                        });
                    }

                    continue;
                }

                //file to add
                if (!line.Contains("dir"))
                {
                    var filesize = int.Parse(line.Split(" ")[0]);

                    stack.Peek().Size += filesize;
                }
            }

            while(stack.Count() > 0)
            {
                var curr = stack.Pop();

                final_countdown.Push(curr);

                if(stack.Count() > 0)
                {
                    stack.Peek().Size += curr.Size;
                }
            }

            var free_space = total_space - final_countdown.Peek().Size;
            var space_required = space_to_delete - free_space;

            var total2 = final_countdown.Where(s => s.Size >= space_required).OrderBy(s => s.Size).First();

            return "Total: " + total2.Size + "\n";
        }

        public static string GetDirectoriesUnder(string[] data, int max_size)
        {
            string output = string.Empty;

            Stack<FS_Details> stack = new Stack<FS_Details>();
            stack.Push(new FS_Details()
            {
                Path = "/",
                Size = 0
            });

            int total = 0;
            List<int> curr_total = new List<int>();

            foreach (var line in data)
            {
                if(line == "$ cd /" || line == "$ ls")
                {
                    continue;
                }
                 
                if (line.Contains("$ cd"))
                {
                    var call = line.Substring(5);

                    if(call == "..")
                    {
                        //pop the stack 
                        FS_Details curr = stack.Pop();

                        if(curr.Size <= max_size)
                        {
                            total += curr.Size;
                            Console.WriteLine(curr.Path + ":\t" + curr.Size);
                        }
                        
                        //add value that was popped the stack's previous item
                        stack.Peek().Size += curr.Size;
                    }
                    else
                    {
                        //push onto stack
                        stack.Push(new FS_Details()
                        {
                            Path = call,
                            Size = 0
                        });
                    }

                    continue;
                }

                //file to add
                if (!line.Contains("dir"))
                {
                    var filesize = int.Parse(line.Split(" ")[0]);

                    stack.Peek().Size += filesize;
                }
            }

            return "Total: " + total + "\n";
        }

        //public static Dictionary<string, FS_Details> BuildTree(string[] input)
        //{
        //    //we can have sub directories with the same name as the 
        //    Dictionary<string, FS_Details> FS = new Dictionary<string, FS_Details>();
        //    FS_Details curr = new FS_Details();
        //    string parentDir = String.Empty;

        //    int index = 0;

        //    foreach (var item in input)
        //    {
        //        var currLine = index + ": " + item;
        //        index++;

        //        if (item.StartsWith("$"))
        //        {
        //            //this means we should cd or ls)
        //            if (item.Contains("cd ") && !item.EndsWith(".."))
        //            {
        //                string arg = item.Split(' ')[item.Split(' ').Length - 1];

        //                if (!FS.ContainsKey(arg))
        //                {
        //                    //add the item to the dictionary and then set the curr to it
        //                    FS.Add(arg, new FS_Details());

        //                    if(arg == "/")
        //                    {
        //                        parentDir = "/";
        //                    }
        //                }
        //                else
        //                {
        //                    curr = FS[arg];
        //                    parentDir = curr.Parent;
        //                }       
        //            }
        //            else if (item.EndsWith(" .."))
        //            {
        //                //set currNode to parent
        //                curr = FS[curr.Parent];
        //                parentDir = curr.Parent;
        //            }
        //            else
        //            {
        //                //ls command so we don't have to really do anything?
        //            }
        //        }
        //        else
        //        {
        //            //list of files/dirs 
        //            if (item.StartsWith("dir"))
        //            {
        //                if(!FS.ContainsKey(item.Substring(4)))
        //                {
        //                    //not in dict yet so add it
        //                    FS.Add(item.Substring(4), new FS_Details() { Parent = parentDir});
        //                }
        //            }
        //            else
        //            {
        //                //add files to list and if there aren't anymore then we add that node
        //                string[] file = item.Split(' ');
        //                if(curr.Files == null)
        //                {
        //                    curr.Files = new Dictionary<string, int>();
        //                    //add file
        //                    curr.Files.Add(file[1], int.Parse(file[0]));
        //                    curr.Size += int.Parse(file[0]);
        //                }
        //                else if (!curr.Files.ContainsKey(file[1]))
        //                {
        //                    curr.Files.Add(file[1], int.Parse(file[0]));
        //                    curr.Size += int.Parse(file[0]);
        //                }
        //            }
        //        }
        //    }

        //    return FS; 
        //}
    }

    public class FS_Details
    {
        public string Path { get; set; }
        public int Size { get; set; }
    }
}


