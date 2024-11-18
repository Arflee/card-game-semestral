using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardDeck : MonoBehaviour
{
    [SerializeField] private CardHolder cardHolder;
    [SerializeField, Range(1, 10)] private int maxCardsInHand = 7;
    [SerializeField] private CombatCard[] playerCards;

    private Queue<CombatCard> _cardDeck;
    private Image _deckFrontImage;

    public int MaxCardsInHand => maxCardsInHand;

    private void Awake()
    {
        _cardDeck = new(playerCards);
        _deckFrontImage = GetComponent<Image>();
    }

    public CombatCard TakeCard()
    {
        if (_cardDeck.Count == 0)
        {
            return null;
        }

        _deckFrontImage.color = new Color(0, _deckFrontImage.color.g - (1f / playerCards.Length), 0);
        var takenCard = _cardDeck.Dequeue();
        cardHolder.AddCard(takenCard);

        return takenCard;
    }
}
