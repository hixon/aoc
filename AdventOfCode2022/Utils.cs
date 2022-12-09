using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Utils
    {
        public const string PATH = @"D:/Projects/AdventOfCode2022/AdventOfCode2022/InputFiles/";

        public static string[] OpenFile(string file)
        { 
            return File.ReadAllLines(PATH + file);
        }
    }
}
