namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Hand> hands = Parser.ParseHands();
            hands.Sort();

            int totalWinnings = 0;

            for (int i = 0; i < hands.Count; i++)
            {
                Hand hand = hands[i];
                hand.rank = i + 1;

                int winnings = hand.bid * hand.rank;
                totalWinnings += winnings;

                Console.WriteLine($"Hand: {new string(hand.cards.ToArray()),-15} Type: {hand.type,-15} Bid: {hand.bid,-15} Rank: {hand.rank,-15} Winnings: {winnings}");
            }

            Console.WriteLine($"\nTotal Winnings: {totalWinnings}");
        }
    }
}