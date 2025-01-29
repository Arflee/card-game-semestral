using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private CardVisual visualPrefab;
    [SerializeField] private Enemy enemy;
    [SerializeField] private RectTransform playedCardsEnemy;
    private Stack<CombatCard> _shuffledDeck;

    public void OnEnable()
    {
        ResetDeck();
    }

    public IEnumerable<Card> GetNextCards(CardOwner owner)
    {
        List<Card> cards = new List<Card>();

        for (int i = 0; i < enemy.CardsPerTurn; i++)
        {
            if (_shuffledDeck.Count == 0)
            {
                if (enemy.ReshufleWhenEmpty)
                    ResetDeck();
                else
                    return cards;
            }

            var nextCard = _shuffledDeck.Pop();

            if (nextCard == null)
                continue;

            var cardSlot = Instantiate(cardSlotPrefab, playedCardsEnemy.transform);
            var card = cardSlot.GetComponentInChildren<Card>();
            card.Initialize(nextCard, owner);
            card.DisableCard();
            cards.Add(card);
        }

        return cards;
    }

    private void ResetDeck()
    {
        if (enemy.ShuffleDeck)
            _shuffledDeck = new(Utility.Shuffle(enemy.EnemyDeck));
        else
            _shuffledDeck = new(enemy.EnemyDeck.Reverse());
    }
}
