using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day6
    {
        public static string[] Input = InputHelper.ReadValuesFromFileAsync(6, true).Result;

        public static int Challenge1()
        {
            string group = "";
            int sum = 0;
            foreach (string row in Input)
            {
                group += row;
                if (row == "")
                {
                    sum += group.Distinct().Count();
                    group = "";
                }
            }
            Console.WriteLine(sum);
            return sum;
        }

        public static int Challenge2()
        {
            var group = new List<string>();
            int sum = 0;
            foreach (string row in Input)
            {
                if (row == "")
                {
                    sum += group.GetIntersection().Count();
                    group = new List<string>();
                }
                else
                {
                    group.Add(row);
                }
            }
            Console.WriteLine($"{sum}");
            return sum;
        }
    }
}
