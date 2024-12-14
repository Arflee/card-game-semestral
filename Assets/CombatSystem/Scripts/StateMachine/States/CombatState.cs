using System;
using System.Collections;
using System.Collections.Generic;

public abstract class CombatState
{
    protected CombatStateMachine StateMachine { get; }
    public abstract CardDeck OwnersDeck { get; }
    public abstract List<Card> OwnersCardsOnTable { get; }
    public abstract List<Card> OpponentsCardsOnTable { get; }

    public CombatState(CombatStateMachine machine)
    {
        StateMachine = machine;
    }

    public virtual IEnumerator EnterState()
    {
        throw new NotImplementedException();
    }
}
