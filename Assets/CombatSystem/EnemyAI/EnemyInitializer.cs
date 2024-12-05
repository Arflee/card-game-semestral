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
        _shuffledDeck = new(Shuffle(enemyDeck));
    }

    public Card GetNextCard()
    {
        if (_shuffledDeck.Count == 0)
        {
            return null;
        }

        var nextCard = _shuffledDeck.Pop();

        var cardSlot = Instantiate(cardSlotPrefab, playedCardsEnemy.transform);
        var card = cardSlot.GetComponentInChildren<Card>();
        card.Initialize(nextCard);
        card.DisableCard();

        return card;
    }

    //https://www.wikiwand.com/en/articles/Fisher-Yates_shuffle
    private List<T> Shuffle<T>(IEnumerable<T> listToShuffle)
    {
        List<T> copiedDeck = new (listToShuffle);

        List<T> shuffled = new();
        System.Random random = new();

        while (copiedDeck.Count > 0)
        {
            int k = random.Next(copiedDeck.Count);
            shuffled.Add(copiedDeck[k]);
            copiedDeck.RemoveAt(k);
        }

        return shuffled;
    }
}
