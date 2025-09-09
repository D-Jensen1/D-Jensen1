using DeckLibrary;
namespace LearnMethods;
// create seperate ClassLibrary Project
// move reusable class into the Class Library
// make project reference to class library
// use class library classes in your test UI
[TestClass]
public class DeckTest
{
    [TestMethod]
    public void ADeckHas52Cards()
    {
        string[] aDeck = Deck.GenerateDeck();
        Assert.AreEqual(52, aDeck.Length);
    }

    [TestMethod]
    public void ADeckHas52UniqueCards()
    {
        HashSet<string> cardSet = new();
        string[] aDeck = Deck.GenerateDeck();
        //aDeck[0] = aDeck[49];
        foreach(var card in aDeck)
        {
            if(!cardSet.Add(card)) 
            {
                Assert.Fail($"There is a duplicate card: {card}");
            }
        }
    }

    [TestMethod]
    public void ANewDeckHasSortedCardsBySuitThenRank()
    {
        string[] aDeck = Deck.GenerateDeck();
        string[] spade = aDeck[0..13];
        string[] heart = aDeck[13..26];
        string[] diamond = aDeck[26..39];
        string[] club = aDeck[39..];


    }

    [TestMethod]
    public void ANewDeckHasSpadesFirst()
    {
        string[] aDeck = Deck.GenerateDeck();
        Assert.AreEqual("\U0001F0A1", aDeck[0]);
    }

    [TestMethod]
    public void Shuffle()
    {
        string[] newDeck = Deck.GenerateDeck();
        string[] shuffleDeck = Deck.GenerateDeck();
        Deck.Shuffle(shuffleDeck);
        Assert.IsFalse(shuffleDeck.SequenceEqual(newDeck));
        CollectionAssert.AreEquivalent(newDeck, shuffleDeck);
    }

    

    [TestMethod]
    public void DrawCardsReturnCorrectNumberOfCardsTest()
    {
        string[] newDeck = Deck.GenerateDeck();
        string[] hand = Deck.DrawCards(ref newDeck, 5);
        Assert.AreEqual(5, hand.Length);
    }

    [TestMethod]
    public void MultipleDrawDoesNotReturnDuplicateCards()
    {
        string[] newDeck = Deck.GenerateDeck();
        string[] hand1 = Deck.DrawCards(ref newDeck, 10);
        string[] hand2 = Deck.DrawCards(ref newDeck, 10);
        CollectionAssert.AreNotEquivalent(hand1, hand2); //make sure its not dealing the same 10 card twice
        HashSet<string> hand1Set = new(hand1);
        HashSet<string> hand2Set = new(hand2);
        Assert.AreEqual(0, hand1Set.Intersect(hand2).Count());//2 hands must not have card in common
    }

    [TestMethod]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(20)]
    [DataRow(30)]

    public void DrawCardsReturnUniqueCardsTest(int countOfCards)
    {
        string[] newDeck = Deck.GenerateDeck();
        string[] hand = Deck.DrawCards(ref newDeck, 10);
        Assert.AreEqual(10, hand.Distinct().Count());
    }

    [TestMethod]
    [DataRow(5)]
    [DataRow(10)]
    [DataRow(15)]
    [DataRow(20)]
    [DataRow(30)]
    [DataRow(40)]
    public void DrawCardsReturnUniqueCardsFromShuffledDeckTest(int countOfCards)
    {
        string[] newDeck = Deck.GenerateDeck();
        Deck.Shuffle(newDeck);
        string[] hand = Deck.DrawCards(ref newDeck, countOfCards);
        Assert.AreEqual(countOfCards, hand.Distinct().Count());
    }
}
