using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Death/Spawn card to same position")]

public class CreateCardOnDeathEffect : CardEffect
{
    [SerializeField] private CombatCard _combatPreset;

    public override bool Die(CombatState combatState, Card card)
    {
        card.Reinitialize(_combatPreset);
        card.CombatDTO.CardEffects.ForEach(eff => eff.OnUse(combatState));
        return false;
    }
}
