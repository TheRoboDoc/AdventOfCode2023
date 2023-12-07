namespace Day7
{
    internal class Hand: IComparable<Hand>
    {
        public enum CardType
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind
        }

        public CardType Type { get; private set;}
        public List<char> Cards { get; private set;}
        public int Bid { get; private set;}
        public int Rank;

        public Hand(string hand, int bid)
        {
            Cards = GetCardsInHand(hand);

            Bid = bid;

            Type = GetHandType(Cards);
        }

        private static List<char> GetCardsInHand(string hand)
        {
            return hand.Trim().ToList();
        }

        private static CardType GetHandType(List<char> cards)
        {
            List<IGrouping<char, char>> cardGroups =
                cards.GroupBy(card => card).OrderByDescending(group => group.Count()).ToList();

            return cardGroups[0].Count() switch
            {
                5 => CardType.FiveOfAKind,
                4 => CardType.FourOfAKind,
                3 => cardGroups[1].Count() == 2 ? CardType.FullHouse : CardType.ThreeOfAKind,
                2 => cardGroups[1].Count() == 2 ? CardType.TwoPair : CardType.OnePair,
                _ => CardType.HighCard,
            };
        }

        public int CompareTo(Hand? other)
        {
            // First, compare hand types
            int typeComparison = Type.CompareTo(other?.Type);
            if (typeComparison != 0)
            {
                return typeComparison;
            }

            for (int i = 0; i < Cards.Count; i++)
            {
                int cardComparison = CompareCard(Cards[i], other?.Cards[i]);
                if (cardComparison != 0)
                {
                    return cardComparison;
                }
            }

            return 0;
        }

        private static int CompareCard(char? card1, char? card2)
        {
            if (card1 == null || card2 == null)
            {
                throw new NullReferenceException("Cannot compare null cards");
            }

            string cardOrder = "23456789TJQKA";

            int index1 = cardOrder.IndexOf((char)card1);
            int index2 = cardOrder.IndexOf((char)card2);

            return index1.CompareTo(index2);
        }
    }
}
