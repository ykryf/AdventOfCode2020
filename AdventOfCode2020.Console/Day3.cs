using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day3
    {
        public static string[] Input { get; set; } = Helper.ReadValuesFromFile("Day3.txt");
        public static int Challenge1(int downStep = 1, int rightStep = 3)
        {
            int index = 0;
            int counter = 0;
            for (int i = 0 + downStep; i < Input.Length; i += downStep)
            {
                index += rightStep;
                if (Input[i][index % Input[i].Length] == '#')
                {
                    counter++;
                }
            }
            Console.WriteLine(counter);
            return counter;
        }

        public static int Challenge2()
        {
            int result = 1;
            var slopes = new List<(int, int)>()
            {
                (1, 1),
                (1, 3),
                (1, 5),
                (1, 7),
                (2, 1)
            };

            foreach (var slope in slopes)
            {
                result *= Challenge1(slope.Item1, slope.Item2);
            }

            Console.WriteLine(result);
            return result;
        }
    }
}
