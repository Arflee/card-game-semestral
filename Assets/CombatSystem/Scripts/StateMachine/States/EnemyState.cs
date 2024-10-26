using System.Collections;

public class EnemyState : CombatState
{
    private EnemyInitializer _initializer;
    private bool _wasInitialized = false;

    public EnemyState(CombatStateMachine machine) : base(machine)
    {
        _initializer = machine.EnemyInitializer;
    }

    public override IEnumerator EnterState()
    {
        if (!_wasInitialized)
        {
            _wasInitialized = true;
            var createdCards = _initializer.PlaceStartCards();

            foreach (var card in createdCards)
            {
                StateMachine.AddCardOnEnemyTable(card);
            }

            yield return null;
        }

        StateMachine.ChangeTurn();
    }
}
