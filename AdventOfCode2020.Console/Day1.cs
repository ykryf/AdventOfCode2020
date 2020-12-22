using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day1
    {
        public static int[] OrderedInput { get; set; } = InputHelper.ReadIntValuesFromFileAsync(1).Result.OrderBy(n => n).ToArray();

        public static int Challenge1()
        {
            (int, int)? numbers = FindTwoSum(OrderedInput);
            int result = numbers != null ? numbers.Value.Item1 * numbers.Value.Item2 : -1;
            Console.WriteLine(result);
            return result;
        }

        public static int Challenge2()
        {
            for (int i = 0; i < OrderedInput.Length; i++)
            {
                (int Number1, int Number2)? numbers = FindTwoSum(OrderedInput.SkipAt(i).ToArray(), 2020 - OrderedInput[i]);
                if (numbers != null)
                {
                    int result = OrderedInput[i] * numbers.Value.Number1 * numbers.Value.Number2;
                    Console.WriteLine(result);
                    return result;
                }
            }
            return -1;
        }


        #region ** Private Methods **
        private static (int First, int Second)? FindTwoSum(int[] orderedInput, int target = 2020)
        {
            int start = 0;
            int end = orderedInput.Length - 1;

            while (start < end)
            {
                int sum = orderedInput[start] + orderedInput[end];
                if (sum < target)
                {
                    start++;
                }
                else if (sum > target)
                {
                    end--;
                }
                else
                {
                    return (orderedInput[start], orderedInput[end]);
                }
            }
            return null;
        }

        #endregion
    }
}
