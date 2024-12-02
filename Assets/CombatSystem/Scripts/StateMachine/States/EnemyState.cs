using System.Collections;
using UnityEngine;

public class EnemyState : CombatState
{
    private EnemyInitializer _initializer;

    public EnemyState(CombatStateMachine machine) : base(machine)
    {
        _initializer = machine.EnemyInitializer;
    }

    public override IEnumerator EnterState()
    {
        var nextCard = _initializer.GetNextCard();
        if (nextCard == null)
        {
            Debug.LogWarning("Enemey is out of cards!");
            yield return null;
        }
        StateMachine.AddCardOnEnemyTable(nextCard);

        StateMachine.ChangeTurn();
        yield return null;
    }
}
