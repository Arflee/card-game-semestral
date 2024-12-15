using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private CardVisual visualPrefab;
    [SerializeField] private CombatCard[] enemyDeck;
    [SerializeField] private RectTransform playedCardsEnemy;

    private Stack<CombatCard> _shuffledDeck;

    public void OnEnable()
    {
        _shuffledDeck = new(Utility.Shuffle(enemyDeck));
    }

    public Card GetNextCard(CardOwner owner)
    {
        if (_shuffledDeck.Count == 0)
        {
            return null;
        }

        var nextCard = _shuffledDeck.Pop();

        var cardSlot = Instantiate(cardSlotPrefab, playedCardsEnemy.transform);
        var card = cardSlot.GetComponentInChildren<Card>();
        card.Initialize(nextCard, owner);
        card.DisableCard();

        return card;
    }
}
