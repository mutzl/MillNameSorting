﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MillNameSorting
{
    class Program
    {
        static void Main(string[] args)
        {

            var names = new []
            {
                "test",
                "b",
                "a",
                "aa",
                "a11",
                "a1",
                "a2",
            };




            foreach (var name in names.OrderBy(n => n, new SemiNumericComparer()))
            {
                Console.WriteLine(name);
            }

            Console.ReadLine();
        }



    }



    //https://stackoverflow.com/a/33330715/527316
    public class SemiNumericComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            int s1r, s2r;
            var s1n = IsNumeric(s1, out s1r);
            var s2n = IsNumeric(s2, out s2r);

            if (s1n && s2n) return s1r - s2r;
            else if (s1n) return -1;
            else if (s2n) return 1;

            var num1 = Regex.Match(s1, @"\d+$");
            var num2 = Regex.Match(s2, @"\d+$");

            var onlyString1 = s1.Remove(num1.Index, num1.Length);
            var onlyString2 = s2.Remove(num2.Index, num2.Length);

            if (onlyString1 == onlyString2)
            {
                if (num1.Success && num2.Success) return Convert.ToInt32(num1.Value) - Convert.ToInt32(num2.Value);
                else if (num1.Success) return 1;
                else if (num2.Success) return -1;
            }

            return string.Compare(s1, s2, true);
        }

        private bool IsNumeric(string value, out int result)
        {
            return int.TryParse(value, out result);
        }
    }
}
