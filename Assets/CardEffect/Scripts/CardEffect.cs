using UnityEngine;


public abstract class CardEffect : ScriptableObject
{
    public virtual void OnUse(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {

    }

    public virtual void OnHit(CardOwner cardOwner, CombatStateMachine manager, Card card, Card target)
    {

    }

    public virtual void OnTakeDamage(CardOwner cardOwner, CombatStateMachine manager, Card card, Card source)
    {

    }

    public virtual bool Die(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        return true;
    }

    public virtual void OnTurnStart(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {

    }
}
