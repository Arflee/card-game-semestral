using System.Collections;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public bool PreventDeath { get; protected set; } = false;

    public IEnumerator StartEffect(CombatStateMachine manager, Card card)
    {
        PreventDeath = false;
        card.CardVisual.ShowEffect();
        yield return TriggerEffect(card.Owner, manager, card);
        card.CardVisual.HideEffect();
    }

    protected abstract IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card);
}
