using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class Hand: IComparable<Hand>
    {
        public enum Type
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind
        }

        public Type type { get; private set;}
        public List<char> cards { get; private set;}
        public int bid { get; private set;}
        public int rank;

        public Hand(string hand, int bid)
        {
            cards = GetCardsInHand(hand);

            this.bid = bid;

            type = GetHandType(cards);
        }

        private List<char> GetCardsInHand(string hand)
        {
            return hand.Trim().ToList();
        }

        private Type GetHandType(List<char> cards)
        {
            List<IGrouping<char, char>> cardGroups =
                cards.GroupBy(card => card).OrderByDescending(group => group.Count()).ToList();

            return cardGroups[0].Count() switch
            {
                5 => Type.FiveOfAKind,
                4 => Type.FourOfAKind,
                3 => cardGroups[1].Count() == 2 ? Type.FullHouse : Type.ThreeOfAKind,
                2 => cardGroups[1].Count() == 2 ? Type.TwoPair : Type.OnePair,
                _ => Type.HighCard,
            };
        }

        public int CompareTo(Hand other)
        {
            // First, compare hand types
            int typeComparison = type.CompareTo(other.type);
            if (typeComparison != 0)
            {
                return typeComparison; // Higher type is stronger
            }

            // If types are the same, compare the entire set of cards
            for (int i = 0; i < cards.Count; i++)
            {
                int cardComparison = CompareCard(cards[i], other.cards[i]);
                if (cardComparison != 0)
                {
                    return cardComparison; // Higher card is stronger
                }
            }

            return 0; // Hands are identical
        }

        private static int CompareCard(char card1, char card2)
        {
            string cardOrder = "23456789TJQKA";

            int index1 = cardOrder.IndexOf(card1);
            int index2 = cardOrder.IndexOf(card2);

            return index1.CompareTo(index2);
        }
    }
}
