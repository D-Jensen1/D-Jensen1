//create deck of cards and randomly deal 5 cards

//52 cards consisting of 13 cards in 4 suits (no joker)
using DeckLibrary;
using System.Text;

//string[] cards = new string[52];
string[] randomCards = Deck.RandomCards(5);
string[] deck = Deck.GenerateDeck();
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine(string.Join(", ",randomCards));

