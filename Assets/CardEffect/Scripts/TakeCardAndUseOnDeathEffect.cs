using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Death/Take card and use")]
public class TakeCardAndUseOnDeathEffect : CardEffect
{
    public override bool Die(CombatState combatState, CombatStateMachine manager, Card card)
    {
        if (combatState.OwnersDeck == null)
            return true;

        var takenCard = combatState.OwnersDeck.TakeCardWithoutAddingToHolder();
        if (takenCard == null)
        {
            Debug.LogWarning("Player is out of cards");
            return true;
        }

        card.Reinitialize(takenCard);
        card.CombatDTO.CardEffects.ForEach(eff => eff.OnUse(combatState, manager));
        return false;
    }
}
