using System.Collections;
using UnityEngine;

public class PlayerState : CombatState
{
    public PlayerState(CombatStateMachine machine) : base(machine)
    {
        StateMachine.OnEndTurn += OnEndTurn;
    }

    public override IEnumerator EnterState()
    {
        yield return null;
    }

    private void OnEndTurn()
    {
        for (int i = 0; i < StateMachine.PlayerCardsOnTable.Count; i++)
        {
            if (i + 1 > StateMachine.EnemyCardsOnTable.Count)
            {
                Debug.Log(i + " Attacks crystal");
                continue;
            }

            var playerCard = StateMachine.PlayerCardsOnTable[i];
            var enemyCard = StateMachine.EnemyCardsOnTable[i];

            enemyCard.TakeDamageFrom(playerCard);
            playerCard.TakeDamageFrom(enemyCard);
        }

        StateMachine.ChangeTurn();
    }
}
