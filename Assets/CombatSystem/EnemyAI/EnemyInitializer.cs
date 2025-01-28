using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private CardVisual visualPrefab;
    [SerializeField] private CombatCard[] enemyDeck;
    [SerializeField] private RectTransform playedCardsEnemy;
    [SerializeField] private bool shuffleDeck = true;
    [SerializeField] private bool reshufleWhenEmpty = true;

    public int cardsPerTurn = 1;
    private Stack<CombatCard> _shuffledDeck;

    public void OnEnable()
    {
        ResetDeck();
    }

    public Card GetNextCard(CardOwner owner)
    {
        if (_shuffledDeck.Count == 0)
        {
            if (reshufleWhenEmpty)
                ResetDeck();
            else
                return null;
        }

        var nextCard = _shuffledDeck.Pop();

        if (nextCard == null)
            return null;

        var cardSlot = Instantiate(cardSlotPrefab, playedCardsEnemy.transform);
        var card = cardSlot.GetComponentInChildren<Card>();
        card.Initialize(nextCard, owner);
        card.DisableCard();

        return card;
    }

    private void ResetDeck()
    {
        if (shuffleDeck)
            _shuffledDeck = new(Utility.Shuffle(enemyDeck));
        else
            _shuffledDeck = new(enemyDeck.Reverse());
    }
}
