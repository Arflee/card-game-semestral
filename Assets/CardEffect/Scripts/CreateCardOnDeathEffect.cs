using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Death/After death use card on table")]

public class CreateCardOnDeathEffect : CardEffect
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
