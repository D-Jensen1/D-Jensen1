using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckLibrary
{
    public class Deck
    {
        public static string[] GenerateDeck()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string[] cards = new string[52];

            for (int x = 0; x < cards.Length; x += 13)
            {
                int deckIndex = x;
                int cardUnicode = default;

                switch (x/13)
                {
                    case 0:
                        //Spades start
                        cardUnicode = 0x1F0A0;
                        break;
                    case 1:
                        //Hearts start
                        cardUnicode = 0x1F0B0;
                        break;
                    case 2:
                        //
                        cardUnicode = 0x1F0C0;
                        break;
                    case 3:

                        cardUnicode = 0x1F0D0;
                        break;
                    default:
                        break;
                }
                for (int i = 1; i < 15; i++)
                {
                    if(i == 12) { i++; }

                    cards[deckIndex] = char.ConvertFromUtf32(cardUnicode + i);
                    deckIndex++;    
                }
            }
            return cards;
        }

        public static string[] RandomCards(int num)
        {
            string[] deck = GenerateDeck();
            string[] randomCards = new string[num];

            Random card = new Random();
            for (int i = 0; i < num; i++)
            {
                int rand = card.Next(0, 52);
                randomCards[i] = deck[rand];

            }
            return randomCards;
        }

        public static void Shuffle(string[] shuffleDeck)
        {
            Random rand = new Random();
            int numberOfPasses = 3;
            for (int i = 0; i < numberOfPasses; i++)
            {
                for (int j = 0; j < GenerateDeck().Length; j++)
                {
                    int destination = rand.Next(52);
                    string temp = shuffleDeck[j];
                    shuffleDeck[j] = shuffleDeck[destination];
                    shuffleDeck[destination] = temp;
                }
            }
        }

        public static string[] DrawCards(ref string[] deck, int v)
        {
            string[] result = deck[0..v];
            string[] newDeck = new string[deck.Length - v];
            Array.Copy(deck, v, newDeck, 0, v);
            deck = newDeck;
            return result;
        }
    }
}
