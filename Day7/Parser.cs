using System.Text.RegularExpressions;

namespace Day7
{
    internal static partial class Parser
    {
        private static string[] ReadFile()
        {
            return File.ReadAllLines("input.txt");
        }

        private static Hand ParseLine(string line)
        {
            MatchCollection matches = CardData().Matches(line);

            return new Hand(matches[0].Value, int.Parse(matches[1].Value));
        }

        public static List<Hand> ParseHands()
        {
            return ReadFile().Select(ParseLine).ToList();
        }

        [GeneratedRegex("\\S+")]
        private static partial Regex CardData();
    }
}
