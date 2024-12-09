using System.Collections;
using UnityEngine;

public class StartingPlayerState : CombatState
{
    public StartingPlayerState(CombatStateMachine machine) : base(machine)
    {
    }

    public override IEnumerator EnterState()
    {
        StateMachine.ManaPanel.ResetManaCrystals(StateMachine.PlayerMana);
        yield return null;
        StateMachine.ChangeTurn();
    }

    protected void OnEndTurn()
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
            var enemyCard = StateMachine.EnemyCardsOnTable[i];
            if (!StateMachine.LifeCrystalPanel.TryAttackCrystal(enemyCard.CombatDTO.Damage))
            {
                Debug.LogError("player is dead");
            }
        }

        StateMachine.ChangeTurn();
    }

    private bool PlayerHasMoreCards(int index)
    {
        return index + 1 > StateMachine.EnemyCardsOnTable.Count;
    }
}
