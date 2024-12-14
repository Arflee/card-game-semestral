using UnityEngine;


public abstract class CardEffect : ScriptableObject
{
    public virtual void OnUse(CombatState combatState, CombatStateMachine manager, Card card)
    {

    }

    public virtual void OnHit(CombatState combatState, CombatStateMachine manager, Card card, Card target)
    {

    }

    public virtual void OnTakeDamage(CombatState combatState, CombatStateMachine manager, Card card, Card source)
    {

    }

    public virtual bool Die(CombatState combatState, CombatStateMachine manager, Card card)
    {
        return true;
    }

    public virtual void OnTurnStart(CombatState combatState, CombatStateMachine manager, Card card)
    {

    }
}
