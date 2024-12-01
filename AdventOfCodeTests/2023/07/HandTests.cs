using AdventOfCode._07;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests._07;

[TestClass]
public class HandTests
{
    [DataTestMethod]
    [DataRow("AAAAA", HandType.FiveOfAKind)]
    [DataRow("AAA4A", HandType.FourOfAKind)]
    [DataRow("99JJ9", HandType.FullHouse)]
    [DataRow("J2322", HandType.ThreeOfAKind)]
    [DataRow("J2A2J", HandType.TwoPairs)]
    [DataRow("QA34A", HandType.OnePair)]
    [DataRow("QAK56", HandType.HighCard)]
    public void GetHandTypeTest(string cards, HandType expectedType)
    {
        // Act
        var hand = new Hand(cards);

        // Assert
        Assert.AreEqual(expectedType, hand.Type);
    }

    [DataTestMethod]
    [DataRow("AAAAA", HandType.FiveOfAKind)]
    [DataRow("AAAJA", HandType.FiveOfAKind)]
    [DataRow("AAJJA", HandType.FiveOfAKind)]
    [DataRow("AJJJA", HandType.FiveOfAKind)]
    [DataRow("JJJJA", HandType.FiveOfAKind)]
    [DataRow("AAA4A", HandType.FourOfAKind)]
    [DataRow("AAA4J", HandType.FourOfAKind)]
    [DataRow("AAJ4J", HandType.FourOfAKind)]
    [DataRow("AJJ4J", HandType.FourOfAKind)]
    [DataRow("99QQ9", HandType.FullHouse)]
    [DataRow("9JQQ9", HandType.FullHouse)] 
    [DataRow("52322", HandType.ThreeOfAKind)]
    [DataRow("5J322", HandType.ThreeOfAKind)]
    [DataRow("5J3J2", HandType.ThreeOfAKind)]
    [DataRow("Q2A2Q", HandType.TwoPairs)]
    [DataRow("QA34A", HandType.OnePair)]
    [DataRow("QJ34A", HandType.OnePair)]
    [DataRow("QAK56", HandType.HighCard)]
    public void GetHandTypeWithJokerTest(string cards, HandType expectedType)
    {
        // Act
        var hand = new Hand(cards, true);

        // Assert
        Assert.AreEqual(expectedType, hand.Type);
    }

    [DataTestMethod]
    [DataRow("AAAAA", "AAAAA", 0)]
    [DataRow("AAAAA", "55555", 1)]
    [DataRow("44545", "45444", -1)]
    [DataRow("22456", "22A48", -1)]
    [DataRow("23456", "34567", -1)]
    [DataRow("44545", "45454", -1)]
    public void CompareCardTests(string cards1, string cards2, int expectedResult)
    {
        // Arrange
        var hand1 = new Hand(cards1);
        var hand2 = new Hand(cards2);

        // Act
        var compareResult = hand1.CompareTo(hand2);

        // Assert
        Assert.AreEqual(expectedResult, compareResult);
    }
}
