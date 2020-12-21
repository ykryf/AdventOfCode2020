using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    static class Helper
    {
        public static string[] ReadValuesFromFile(string path, bool allowEmptyStrings = false)
        {
            StreamReader streamReader = new StreamReader(path);
            string input = streamReader.ReadToEnd();
            return input.Replace("\r", "").Split('\n', allowEmptyStrings ? StringSplitOptions.None : StringSplitOptions.RemoveEmptyEntries);
        }
        
        public static int[] ReadIntValuesFromFile(string path)
        {
            List<int> result = new List<int>();
            foreach (string value in ReadValuesFromFile(path))
            {
                if (int.TryParse(value, out int intValue))
                {
                    result.Add(intValue);
                }
            }
            return result.ToArray();
        }
    }
}
