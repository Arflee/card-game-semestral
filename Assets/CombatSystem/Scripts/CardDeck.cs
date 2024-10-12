using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardDeck : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int maxCardsInHand = 7;
    [SerializeField] private CombatCard[] playerCards;

    private Stack<CombatCard> _cardDeck;
    private Image _deckFrontImage;

    public int MaxCardsInHand => maxCardsInHand;

    private void Start()
    {
        _cardDeck = new(playerCards.Reverse());
        _deckFrontImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeCard();
        }

    }

    public CombatCard TakeCard()
    {
        if (_cardDeck.Count == 0)
        {
            return null;
        }

        _deckFrontImage.color = new Color(0, _deckFrontImage.color.g - (1f / playerCards.Length), 0);

        return _cardDeck.Pop();
    }
}
