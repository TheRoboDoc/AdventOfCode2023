namespace Day7
{
    internal class Program
    {
        static void Main()
        {
            List<Hand> hands = Parser.ParseHands();
            hands.Sort();

            int totalWinnings = 0;

            for (int i = 0; i < hands.Count; i++)
            {
                Hand hand = hands[i];
                hand.Rank = i + 1;

                int winnings = hand.Bid * hand.Rank;
                totalWinnings += winnings;

                Console.WriteLine($"Hand: {new string(hand.Cards.ToArray()),-15} Type: {hand.Type,-15} Bid: {hand.Bid,-15} Rank: {hand.Rank,-15} Winnings: {winnings}");
            }

            Console.WriteLine($"\nTotal Winnings: {totalWinnings}");
        }
    }
}