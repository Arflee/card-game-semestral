using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private CardVisual visualPrefab;
    [SerializeField] private CombatCard[] enemyHand;
    [SerializeField] private RectTransform playedCardsEnemy;

    public List<Card> PlaceStartCards()
    {
        List<Card> _cardObjects = new();

        for (int i = 0; i < enemyHand.Length; i++)
        {
            var cardSlot = Instantiate(cardSlotPrefab, playedCardsEnemy.transform);
            var card = cardSlot.GetComponentInChildren<Card>();
            card.Initialize(enemyHand[i]);
            card.DisableCard();

            _cardObjects.Add(card);
        }

        return _cardObjects;
    }
}
