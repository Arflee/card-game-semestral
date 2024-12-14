using System.Collections;
using System.Collections.Generic;

public class WinState : CombatState
{
    public override CardDeck OwnersDeck => null;

    public override List<Card> OwnersCardsOnTable => null;

    public override List<Card> OpponentsCardsOnTable => null;

    public WinState(CombatStateMachine machine) : base(machine)
    {
    }

    public override IEnumerator EnterState()
    {
        return base.EnterState();
    }
}
