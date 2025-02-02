using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private CardVisual visualPrefab;
    [SerializeField] private LifeCrystalPanel crystalPanel;
    [SerializeField] private Enemy enemy;
    [SerializeField] private RectTransform playedCardsEnemy;
    [SerializeField] private RectTransform spawnLocation;
    [SerializeField] private CombatStateMachine manager;

    private EnemyDeck currentDeck;
    private Stack<CombatCard> _shuffledDeck;
    private int crystalsDestroyed = -1;

    public void OnEnable()
    {
        crystalPanel.Initialize(enemy.Crystals);
        ResetDeck();
    }

    public CombatState StartingCombatState(CombatStateMachine machine)
    {
        return enemy.StartingCombatState(machine);
    }

    public IEnumerable<Card> GetNextCards(CardOwner owner)
    {
        if (currentDeck == null || crystalsDestroyed != enemy.Crystals.CrystalAmount - manager.EnemyCrystals)
            ResetDeck();

        List<Card> cards = new List<Card>();

        for (int i = 0; i < currentDeck.CardsPerTurn; i++)
        {
            if (_shuffledDeck.Count == 0)
            {
                if (currentDeck.ReshufleWhenEmpty)
                    ResetDeck();
                else
                    return cards;
            }

            var nextCard = _shuffledDeck.Pop();

            if (nextCard == null)
                continue;

            if (manager.EnemyCardsOnTable.Count + cards.Count >= manager.MaxCardsOnBoard)
                continue;

            var cardSlot = Instantiate(cardSlotPrefab, playedCardsEnemy.transform);
            var card = cardSlot.GetComponentInChildren<Card>();
            card.Initialize(nextCard, owner, spawnLocation.position);
            card.DisableCard();
            cards.Add(card);
        }

        return cards;
    }

    private void ResetDeck()
    {
        crystalsDestroyed = enemy.Crystals.CrystalAmount - manager.EnemyCrystals;
        if (manager.EnemyCrystals == 0)
            return;
        currentDeck = enemy.EnemyDeckPerCrystalsDestroyed[crystalsDestroyed];
        if (currentDeck.ShuffleDeck)
            _shuffledDeck = new(Utility.Shuffle(currentDeck));
        else
            _shuffledDeck = new(currentDeck.Reverse());
    }

    public int CardCount()
    {
        if (currentDeck == null)
            ResetDeck();

        if (currentDeck.ReshufleWhenEmpty)
            return int.MaxValue;

        return _shuffledDeck.Count;
    }
}
