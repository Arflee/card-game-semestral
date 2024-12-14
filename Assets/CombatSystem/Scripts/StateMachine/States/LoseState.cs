using System.Collections;
using System.Collections.Generic;

public class LoseState : CombatState
{
    private GameEndingHandler _gameHandler;

    public override CardDeck OwnersDeck => null;

    public override List<Card> OwnersCardsOnTable => null;

    public override List<Card> OpponentsCardsOnTable => null;

    public LoseState(CombatStateMachine machine) : base(machine)
    {
        _gameHandler = machine.GameHandler;
    }

    public override IEnumerator EnterState()
    {
        _gameHandler.PlayerLostGame();
        yield return null;
    }
}
