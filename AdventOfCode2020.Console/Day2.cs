using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day2
    {
        public static List<Password> Passwords = SetPasswordsAsync().Result;

        public static int Challenge1()
        {
            int counter = 0;
            foreach (Password password in Passwords)
            {
                int occurences = password.Value.ToList().FindAll(c => c == password.CharValue).Count;
                if (occurences >= password.Min && occurences <= password.Max)
                {
                    counter++;
                }
            }
            Console.WriteLine(counter);
            return counter;
        }

        public static int Challenge2()
        {
            int counter = 0;
            foreach (Password password in Passwords)
            {
                if (password.Value.ElementAt(password.Min - 1) == password.CharValue ^ 
                    password.Value.ElementAt(password.Max - 1) == password.CharValue)
                {
                    counter++;
                }
            }
            Console.WriteLine(counter);
            return counter;
        }

        #region ** Helper Methods **
        public static async Task<List<Password>> SetPasswordsAsync()
        {
            var passwords = new List<Password>();
            string[] input = await InputHelper.ReadValuesFromFileAsync(2);
            foreach (var item in input)
            {
                string[] rowValues = item.Split('-', ' ', ':');
                passwords.Add(new Password()
                {
                    Min = int.Parse(rowValues[0]),
                    Max = int.Parse(rowValues[1]),
                    CharValue = char.Parse(rowValues[2]),
                    Value = rowValues.Last()
                });
            }
            return passwords;
        }
        #endregion
    }

    public struct Password
    {
        public int Min;
        public int Max;
        public char CharValue;
        public string Value;
    }
}
