using System.Text.RegularExpressions;

namespace Day2
{
    internal class Program
    {
        const int MAX_RED = 12;
        const int MAX_GREEN = 13;
        const int MAX_BLUE = 14;

        const bool PART_TWO = false;

        struct Game
        {
            public int gameID;
            public List<string> cubeValues;

            public int minValidRed;
            public int minValidGreen;
            public int minValidBlue;

            public Game()
            {
                cubeValues = new List<string>();

                minValidRed = 0;
                minValidGreen = 0;
                minValidBlue = 0;
            }
        }

        static void Main()
        {
            string[] gameEntries = GetGames();

            List<Game> games = new();

            foreach (string gameEntry in gameEntries)
            {
                games.Add(ExtractGameValues(gameEntry));
            }

            if (!PART_TWO)
            {
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CS0162 // Unreachable code detected
                List<Game> validGames = new();
#pragma warning restore IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning restore CS0162 // Unreachable code detected

                foreach (Game game in games)
                {
                    if (IsValid(game))
                    {
                        validGames.Add(game);
                    }
                }
#pragma warning restore IDE0079 // Remove unnecessary suppression

                int sum = 0;

                foreach (Game game in validGames)
                {
                    sum += game.gameID;
                }

                Console.WriteLine($"Answer: {sum}");
            }
            else
            {
#pragma warning disable CS0162 // Unreachable code detected
                int total = 0;
#pragma warning restore CS0162 // Unreachable code detected

                foreach(Game game in games)
                {
                    int power = game.minValidRed * game.minValidGreen * game.minValidBlue;

                    total += power;
                }

                Console.WriteLine($"Asnwer: {total}");
            }
        }

        static string[] GetGames()
        {
            return File.ReadAllLines("Games.txt");
        }

        static Game ExtractGameValues(string gameInfo)
        {
            string pattern = @"\d+:|\d+ blue|\d+ red|\d+ green";

            Regex rg = new(pattern);

            MatchCollection values = rg.Matches(gameInfo);

            Game game = new();

            string pattern2 = @"\d+";

            string id = values[0].Value;

            game.gameID = int.Parse(Regex.Match(id, pattern2).Value);

            Console.WriteLine($"Game {game.gameID}");

            for (int i = 1; i < values.Count; i++)
            {
                game.cubeValues.Add(values[i].Value);
                Console.Write($"<{values[i].Value}> ");
            }

            if (PART_TWO)
            {
#pragma warning disable CS0162 // Unreachable code detected
                Dictionary<string, int> minumumValues = MinumumCubes(game);
#pragma warning restore CS0162 // Unreachable code detected

                game.minValidRed = minumumValues["red"];
                game.minValidGreen = minumumValues["green"];
                game.minValidBlue = minumumValues["blue"];

                Console.WriteLine();
                Console.WriteLine($"Min Red: {game.minValidRed} | Min Green {game.minValidGreen} | Min Blue {game.minValidBlue}");
            }

            Console.WriteLine();
            Console.WriteLine();

            return game;
        }

        static bool IsValid(Game game)
        {
            string pattern = @"\d+";

            foreach(string cubeEntry in game.cubeValues)
            {
                int value = int.Parse(Regex.Match(cubeEntry, pattern).Value);

                if (cubeEntry.Contains("red"))
                {
                    if (value > MAX_RED)
                    {
                        return false;
                    }
                }
                else if (cubeEntry.Contains("green"))
                {
                    if (value > MAX_GREEN)
                    {
                        return false;
                    }
                }
                else if (cubeEntry.Contains("blue"))
                {
                    if (value > MAX_BLUE)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static Dictionary<string, int> MinumumCubes(Game game)
        {
            int minRed = 0;
            int minGreen = 0;
            int minBlue = 0;

            string pattern = @"\d+";

            foreach (string cubeEntry in game.cubeValues)
            {
                int value = int.Parse(Regex.Match(cubeEntry, pattern).Value);

                if (cubeEntry.Contains("red"))
                {
                    if (value > minRed)
                    {
                        minRed = value;
                    }
                }
                else if (cubeEntry.Contains("green"))
                {
                    if (value > minGreen)
                    {
                        minGreen = value;
                    }
                }
                else if (cubeEntry.Contains("blue"))
                {
                    if (value > minBlue)
                    {
                        minBlue = value;
                    }
                }
            }

            return new Dictionary<string, int>
            {
                { "red", minRed },
                { "green", minGreen },
                { "blue", minBlue },
            };
        }
    }
}