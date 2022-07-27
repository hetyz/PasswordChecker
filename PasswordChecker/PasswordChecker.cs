﻿using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace PasswordChecker
{
    internal class PasswordChecker
    {
        static void Main(string[] args)
        {
            PrintValidPasswords(new ArrayList
            {
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"
            });
        }

        private static void PrintValidPasswords(ArrayList passwords)
        {
            foreach (var clean in from string password in passwords
                                  let clean = Regex.Replace(password, "[-:]", " ")
                                  .Split(' ')
                                  .Where(s => !string.IsNullOrWhiteSpace(s))
                                  .Distinct()
                                  .ToList()
                                  let m = new Regex(clean[2]).Matches(clean[3])
                                  where m.Count >= Convert.ToInt32(clean[0]) && m.Count <= Convert.ToInt32(clean[1])
                                  select clean)
            {
                Console.WriteLine(clean[3] + " is a good password!");
            }
        }
    }
}