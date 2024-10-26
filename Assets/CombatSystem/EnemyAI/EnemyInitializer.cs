using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private CardVisual visualPrefab;
    [SerializeField] private CombatCard[] enemyHand;
    [SerializeField] private RectTransform playedCardsEnemy;

    private List<Card> _cardObjects = new();

    public List<Card> PlaceStartCards()
    {
        for (int i = 0; i < enemyHand.Length; i++)
        {
            var cardSlot = Instantiate(cardSlotPrefab, playedCardsEnemy.transform);
            var card = cardSlot.GetComponentInChildren<Card>();
            card.Initialize(enemyHand[i]);

            _cardObjects.Add(card);
        }

        return _cardObjects;
    }
}
