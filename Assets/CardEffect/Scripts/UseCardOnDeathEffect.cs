using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "CardEffects/On Death/After death use card on table")]

public class UseCardOnDeathEffect : CardEffect
{
    [SerializeField] private GameObject _cardSlotPrefab;
    [SerializeField] private CombatCard _combatPreset;

    public override Card OnDeathCreateCard(Card deadCard, Transform parentTransform)
    {
        var cardSlot = Instantiate(_cardSlotPrefab, parentTransform);
        var card = cardSlot.GetComponentInChildren<Card>();

        card.Initialize(_combatPreset);
        card.DisableCard();

        return card;
    }
}
