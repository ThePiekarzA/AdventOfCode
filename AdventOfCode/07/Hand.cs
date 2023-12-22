namespace AdventOfCode._07;

public class Hand : IComparable<Hand>
{
    private readonly Dictionary<char, int> _cardValuesWithoutJoker = new()
    {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'J', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 },
    };

    private readonly Dictionary<char, int> _cardValuesWithJoker = new()
    {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'T', 11 },
        { '9', 10 },
        { '8', 9 },
        { '7', 8 },
        { '6', 7 },
        { '5', 6 },
        { '4', 5 },
        { '3', 4 },
        { '2', 3 },
        { 'J', 2 },
    };

    public string Cards { get; }
    public HandType Type { get; }
    public int Bid { get; set; }
    public bool UseJoker { get; set; }

    public Hand(string cards, bool useJoker = false)
    {
        Cards = cards;
        UseJoker = useJoker;
        Type = GetHandType(cards);
    }

    public virtual int CompareTo(Hand? other)
    {
        if (Type > other.Type)
        {
            return 1;
        }

        if (Type < other.Type)
        {
            return -1;
        }

        var cardValues = UseJoker ? _cardValuesWithJoker : _cardValuesWithoutJoker;

        for (var i = 0; i < Cards.Length; i++)
        {
            if (cardValues[Cards[i]] > cardValues[other.Cards[i]])
            {
                return 1;
            }

            if (cardValues[Cards[i]] < cardValues[other.Cards[i]])
            {
                return -1;
            }
        }

        return 0;
    }

    public override string ToString()
    {
        var description = $"{Cards}";
        if (Bid < 0)
        {
            description += $" {Bid}";
        }
        return description;
    }

    protected virtual HandType GetHandType(string hand)
    {
        var distinctCards = hand.Distinct().ToArray();
        var cardCounts = distinctCards.Select(dc => hand.Count(hc => hc == dc)).ToArray();
        var isJokerInHand = UseJoker && hand.Any(c => c == 'J');

        // Five of a kind
        if ( distinctCards.Length == 1 || 
             ( isJokerInHand && distinctCards.Length == 2 ))
        {
            return HandType.FiveOfAKind;
        }

        // Fourth of a kind
        if (( distinctCards.Length == 2 && cardCounts.Any(cc => cc == 4) ) ||
            ( isJokerInHand && distinctCards.Length == 3 && 
                ( cardCounts.Count(cc => cc == 1) == 2 || hand.Count(hc => hc == 'J') == 2 )))
        {
            return HandType.FourOfAKind;
        }

        // Full house
        if (( distinctCards.Length == 2 && cardCounts.Count(cc => cc == 3) == 1 && cardCounts.Count(cc => cc == 2) == 1 ) ||
            ( isJokerInHand && distinctCards.Length == 3 ))
        {
            return HandType.FullHouse;
        }

        // Three of a kind
        if (( distinctCards.Length == 3 && cardCounts.Any(cc => cc == 3 )) ||
            ( isJokerInHand && distinctCards.Length == 4 && cardCounts.Any(cc => cc == 2) ))
        {
            return HandType.ThreeOfAKind;
        }

        // Two Pairs
        if (( distinctCards.Length == 3 && cardCounts.Count(cc => cc == 2) == 2 && cardCounts.Count(cc => cc == 1) == 1 ) ||
            ( isJokerInHand && distinctCards.Length == 4 && cardCounts.Any(cc => cc == 2) ))
        {
            return HandType.TwoPairs;
        }

        // One pair
        if ( distinctCards.Length == 4 && cardCounts.Any(cc => cc == 2) ||
            ( isJokerInHand && distinctCards.Length == 5 ))
        {
            return HandType.OnePair;
        }

        return HandType.HighCard;
    }
}