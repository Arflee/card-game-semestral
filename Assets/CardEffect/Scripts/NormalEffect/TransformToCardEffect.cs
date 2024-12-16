using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Transform To Card", menuName = "Card Effects/Transform To Card")]
public class TransformToCardEffect : NormalCardEffect
{
    [SerializeField] private CombatCard _combatPreset;

    protected override IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        yield return new WaitForSeconds(0.5f);
        card.Reinitialize(_combatPreset);
        foreach (var eff in card.CombatDTO.OnUseEffects)
        {
            yield return eff.StartEffect(manager, card);
        }
        PreventDeath = true;
    }
}
