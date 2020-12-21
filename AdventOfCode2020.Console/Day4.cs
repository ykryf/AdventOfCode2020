using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day4
    {
        public static List<Passport> Passports { get; set; } = GetPassports("Day4.txt");

        private static string[] passportKeys = new string[]
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid",
        };


        public static int Challenge1()
        {
            int counter = 0;
            foreach (Passport passport in Passports)
            {
                int fieldsCount = passport.GetType().GetProperties().Where(p => !string.IsNullOrEmpty(p.GetValue(passport) as string)).Count();
                if (fieldsCount == 8 || (fieldsCount == 7 && passport.Cid == null))
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
            foreach (Passport passport in Passports)
            {
                if (IsValid(passport))
                {
                    counter++;
                }
            }
            Console.WriteLine(counter);
            return counter;
        }

        #region ** Helper Methods **
        public static List<Passport> GetPassports(string filepath)
        {
            string[] input = Helper.ReadValuesFromFile(filepath, true);
            List<Passport> passports = new List<Passport>();
            string passportValue = "";
            foreach (string row in input)
            {
                passportValue += " " + row;
                if (string.IsNullOrEmpty(row))
                {
                    // EndOfPassport
                    string[] fields = passportValue.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var passport = new Passport();
                    foreach (string field in fields)
                    {
                        if (field.Split(':').Length > 1)
                        {
                            PropertyInfo property = passport.GetType().GetProperty(
                                field.Split(':')[0].Substring(0, 1).ToUpper() + field.Split(':')[0].Substring(1));
                            property.SetValue(passport, field.Split(':')[1]);
                        }
                    }
                    passports.Add(passport);
                    passportValue = "";
                }
            }
            return passports;
        }

        #endregion

        #region ** Private Methods **
        private static bool IsValid(Passport passport)
        {
            // BYR
            if (!int.TryParse(passport.Byr, out int byr) || !(byr >= 1920 && byr <= 2002))
            {
                return false;
            }
            // IYR
            if (!int.TryParse(passport.Iyr, out int iyr) || !(iyr >= 2010 && iyr <= 2020))
            {
                return false;
            }
            // EYR
            if (!int.TryParse(passport.Eyr, out int eyr) || !(eyr >= 2020 && eyr <= 2030))
            {
                return false;
            }
            // HGT
            if (passport.Hgt == null || !(passport.Hgt.Contains("cm") || passport.Hgt.Contains("in")))
            {
                return false;
            }
            (string Value, string Suffix) hgt = (string.Join("", passport.Hgt.SkipLast(2)), string.Join("", passport.Hgt.TakeLast(2)));
            int hgtValue;
            if (hgt.Suffix == "cm" && (!int.TryParse(hgt.Value, out hgtValue) || !(hgtValue >= 150 && hgtValue <= 193)))
            {
                return false;
            }
            if (hgt.Suffix == "in" && (!int.TryParse(hgt.Value, out hgtValue) || !(hgtValue >= 59 && hgtValue <= 76)))
            {
                return false;
            }
            // HCL
            var regex = new Regex(@"^[0-9a-f]{6}$");
            if (passport.Hcl == null || passport.Hcl.First() != '#' || !regex.IsMatch(passport.Hcl.Substring(1)))
            {
                return false;
            }
            // ECL
            string[] validEclValues = new string[]
            {
                    "amb",
                    "blu",
                    "brn",
                    "gry",
                    "grn",
                    "hzl",
                    "oth"
            };
            if (passport.Ecl == null || !validEclValues.Contains(passport.Ecl))
            {
                return false;
            }
            // PID
            regex = new Regex(@"^[0-9]{9}$");
            if (passport.Pid == null || !regex.IsMatch(passport.Pid))
            {
                return false;
            }
            return true;
        }
        #endregion
    }

    class Passport
    {
        public string Byr { get; set; }
        public string Iyr { get; set; }
        public string Eyr { get; set; }
        public string Hgt { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }
        public string Cid { get; set; }
    }
}
