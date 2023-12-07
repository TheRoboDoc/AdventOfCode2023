using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day7
{
    internal static class Parser
    {
        private static string[] ReadFile()
        {
            return File.ReadAllLines("input.txt");
        }

        private static Hand ParseLine(string line)
        {
            MatchCollection matches = Regex.Matches(line, @"\S+");

            return new Hand(matches[0].Value, int.Parse(matches[1].Value));
        }

        public static List<Hand> ParseHands()
        {
            return ReadFile().Select(ParseLine).ToList();
        }
    }
}
