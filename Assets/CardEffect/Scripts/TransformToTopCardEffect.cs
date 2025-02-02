using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Transform To Top Card", menuName = "Card Effects/Transform To Top Card")]
public class TransformToTopCardEffect : CardEffect
{
    protected override IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        yield return new WaitForSeconds(0.5f);
        if (cardOwner.OwnersDeck == null)
            yield break;

        var takenCard = cardOwner.OwnersDeck.TakeCardWithoutAddingToHolder();
        if (takenCard == null)
        {
            Debug.LogWarning("Player is out of cards");
            yield break;
        }

        card.Reinitialize(takenCard);
        card.TurnPlayed = manager.CurrentTurn;
        foreach (var eff in card.CombatDTO.OnUseEffects)
        {
            yield return eff.StartEffect(manager, card);
        }
        PreventDeath = true;
    }
}
