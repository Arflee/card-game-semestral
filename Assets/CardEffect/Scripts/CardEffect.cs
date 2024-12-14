using UnityEngine;


public abstract class CardEffect : ScriptableObject
{
    public virtual void OnUse(CombatState combatState)
    {

    }

    public virtual void OnHit(CombatState combatState, Card target)
    {

    }

    public virtual void OnTakeDamage(CombatState combatState, Card source)
    {

    }

    public virtual bool Die(CombatState combatState, Card card)
    {
        return true;
    }

    public virtual void OnTurnStart(CombatState combatState)
    {

    }
}
