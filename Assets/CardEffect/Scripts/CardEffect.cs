using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public virtual void OnUse(CardDeck deck, Card usedCard)
    {

    }

    public virtual void OnHit(Card target)
    {

    }

    public virtual void OnTakeDamage(Card source)
    {

    }

    public virtual Card OnDeathCreateCard(Card deadCard, Transform parentTransform)
    {
        return null;
    }

    public virtual void OnTurnStart()
    {

    }
}
