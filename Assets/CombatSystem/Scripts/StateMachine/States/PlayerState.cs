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
            if (PlayerHasMoreCards(i))
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

    private bool PlayerHasMoreCards(int index)
    {
        return index + 1 > StateMachine.EnemyCardsOnTable.Count;
    }
}
