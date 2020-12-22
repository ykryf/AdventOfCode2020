using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public static class InputHelper
    {
        public static string CookieValue
        {
            get
            {
                return Configuration["Cookie"];
            }
        }
        
        public static IConfiguration Configuration { get; set; } = new ConfigurationBuilder()
            .AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly())
            .Build();

        /// <summary>
        /// Returns the input as an array of strings where each element represents a row.
        /// </summary>
        /// <param name="path">The relative path of the file.</param>
        /// <param name="allowEmptyStrings"></param>
        /// <returns></returns>
        public static async Task<string[]> ReadValuesFromFileAsync(string path = null, bool allowEmptyStrings = false)
        {
            StreamReader streamReader = new StreamReader(path);
            string input = await streamReader.ReadToEndAsync();
            return input.Replace("\r", "").Split('\n', allowEmptyStrings ? StringSplitOptions.None : StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns the input as an array of strings where each element represents a row. The input file is downloaded automatically.
        /// </summary>
        /// <param name="dayNumber">The number of the calendar day</param>
        /// <param name="allowEmptyStrings"></param>
        /// <returns></returns>
        public static async Task<string[]> ReadValuesFromFileAsync(int dayNumber, bool allowEmptyStrings = false)
        {
            await CreateInputFileAsync(dayNumber);
            string path = GetFilepath(dayNumber);
            return await ReadValuesFromFileAsync(path, allowEmptyStrings);
        }

        /// <summary>
        /// Returns the input as an array of integers where each element represents a row.
        /// </summary>
        /// <param name="path">The relative path of the file</param>
        /// <returns></returns>
        public static async Task<int[]> ReadIntValuesFromFileAsync(string path = null)
        {
            List<int> result = new List<int>();
            foreach (string value in await ReadValuesFromFileAsync(path))
            {
                if (int.TryParse(value, out int intValue))
                {
                    result.Add(intValue);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// Returns the input as an array of integers where each element represents a row. The input file is downloaded automatically.
        /// </summary>
        /// <param name="dayNumber">The number of the calendar day</param>
        /// <returns></returns>
        public static async Task<int[]> ReadIntValuesFromFileAsync(int dayNumber)
        {
            await CreateInputFileAsync(dayNumber);
            string path = GetFilepath(dayNumber);
            return await ReadIntValuesFromFileAsync(path);
        }

        /// <summary>
        /// Downloads and creates input file for the specific day. 
        /// </summary>
        /// <param name="dayNumber">The number of the calendar day</param>
        /// <returns></returns>
        private static async Task CreateInputFileAsync(int dayNumber)
        {
            string filepath = GetFilepath(dayNumber);
            if (!File.Exists(filepath))
            {
                try
                {
                    using (FileStream fileStream = File.Create(filepath))
                    {
                        string input = await GetInput(dayNumber);
                        fileStream.Write(new UTF8Encoding(true).GetBytes(input));
                    }
                    File.Copy(filepath, $"../../../{filepath}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Sends a GET HTTP request to the url that corresponds to the specific calendar day. A session cookie is used for authentication.
        /// </summary>
        /// <param name="dayNumber">The number of the calendar day</param>
        /// <returns></returns>
        private static async Task<string> GetInput(int dayNumber)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("cookie", CookieValue);
            var response = await client.GetAsync($"https://adventofcode.com/2020/day/{dayNumber}/input");
            return await response.Content.ReadAsStringAsync();
        }

        private static string GetFilepath(int dayNumber) => $"Input/Day{dayNumber}.txt";
    }
}
