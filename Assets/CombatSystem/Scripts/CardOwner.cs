using System.Collections.Generic;

public class CardOwner
{
    public CardDeck OwnersDeck { get; }
    public List<Card> OwnersCardsOnTable { get; }
    public List<Card> OpponentsCardsOnTable { get; }

    public CardOwner(CardDeck ownersDeck, List<Card> ownersCards, List<Card> opponentsCards)
    {
        OwnersDeck = ownersDeck;
        OwnersCardsOnTable = ownersCards;
        OpponentsCardsOnTable = opponentsCards;
    }
}
