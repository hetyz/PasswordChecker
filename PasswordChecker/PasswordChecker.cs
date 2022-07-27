using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PasswordChecker
{
    internal class PasswordChecker
    {
        static void Main(string[] args)
        {
            var filePath = "..\\..\\passwords.txt";
 
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File does not exist: {0}", filePath);
                return;
            }

            Console.WriteLine(GetValidPasswordsNumber(new ArrayList(File.ReadAllLines(filePath))));
        }

        private static int GetValidPasswordsNumber(ArrayList passwords)
        {
            return (from string password in passwords
                    let clean = Regex.Replace(password, "[-:]", " ")
                        .Split(' ')
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Distinct()
                        .ToList()
                    let m = new Regex(clean[2]).Matches(clean[3])
                    where IsValidPassword(clean, m)
                    select password).Count();
        }

        private static bool IsValidPassword(List<string> clean, MatchCollection m)
        {
            return m.Count >= Convert.ToInt32(clean[0]) && m.Count <= Convert.ToInt32(clean[1]);
        }
    }
}
