using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day5
    {
        public static string[] Passes { get; set; } = InputHelper.ReadValuesFromFileAsync(5).Result;
        public static int Challenge1()
        {
            int max = 0;
            foreach (string pass in Passes)
            {
                int seatId = GetSeatId(pass);
                if (seatId > max)
                {
                    max = seatId;
                }
            }
            Console.WriteLine(max);
            return max;
        }

        public static int Challenge2()
        {
            int[] seatIds = Passes.Select(p => GetSeatId(p)).OrderBy(s => s).ToArray();

            for (int i = 1; i < seatIds.Length - 1; i++)
            {
                if (seatIds[i] == seatIds[i + 1] - 2)
                {
                    Console.WriteLine(seatIds[i] + 1);

                    return seatIds[i] + 1;
                }
            }
            return -1;
        }

        private static int GetSeatId(string pass)
        {
            (int, int) rows = (0, 127);
            (int, int) columns = (0, 7);

            return FindRowOrColumn(rows, 'B', 'F', pass.Substring(0, 7)) * 8 + FindRowOrColumn(columns, 'R', 'L', pass.Substring(7));
        }

        /// <summary>
        /// Returns the number of row or column based on input.
        /// </summary>
        /// <param name="limits">The upper and bottom limit of the columns or row set</param>
        /// <param name="up">The character that defines the upper half</param>
        /// <param name="down">The character that defines the bottom half</param>
        /// <param name="input">The string with the binary space partitioning</param>
        /// <returns></returns>
        private static int FindRowOrColumn((int min, int max) limits, char up, char down, string input)
        {
            foreach (char c in input)
            {
                if (c == up)
                {
                    limits.min = (int)Math.Ceiling((limits.min + limits.max) / (double)2);
                }
                else
                {
                    limits.max = (int)Math.Floor((limits.min + limits.max) / (double)2);
                }
            }
            return limits.min;
        }
    }
}
