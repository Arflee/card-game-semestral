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
        StateMachine.ManaPanel.ResetManaCrystals(StateMachine.PlayerMana);
        yield return null;
    }

    private void OnEndTurn()
    {
        int cardsDifference = StateMachine.EnemyCardsOnTable.Count - StateMachine.PlayerCardsOnTable.Count;

        for (int i = 0; i < StateMachine.PlayerCardsOnTable.Count; i++)
        {
            if (PlayerHasMoreCards(i))
            {
                Debug.Log("Player attacks crystal");
                continue;
            }

            var playerCard = StateMachine.PlayerCardsOnTable[i];
            var enemyCard = StateMachine.EnemyCardsOnTable[i];

            enemyCard.TakeDamageFrom(playerCard);
            playerCard.TakeDamageFrom(enemyCard);
        }

        for (int i = StateMachine.PlayerCardsOnTable.Count; i < StateMachine.PlayerCardsOnTable.Count + cardsDifference; i++)
        {
            Debug.Log(StateMachine.EnemyCardsOnTable[i].name + " attacks player's crystal");
        }

        StateMachine.ChangeTurn();
    }

    private bool PlayerHasMoreCards(int index)
    {
        return index + 1 > StateMachine.EnemyCardsOnTable.Count;
    }
}
