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
            var playerCard = StateMachine.PlayerCardsOnTable[i];

            if (PlayerHasMoreCards(i))
            {
                if (!StateMachine.TryAttackEnemyCrystal(playerCard.CombatDTO.Damage))
                {
                    Debug.LogError("enemy is dead");
                    StateMachine.SetState(new WinState(StateMachine));
                }
                Debug.Log("Player attacks crystal");
                continue;
            }

            var enemyCard = StateMachine.EnemyCardsOnTable[i];

            enemyCard.TakeDamageFrom(playerCard);
            playerCard.TakeDamageFrom(enemyCard);
        }

        for (int i = StateMachine.PlayerCardsOnTable.Count; i < StateMachine.EnemyCardsOnTable.Count; i++)
        {
            var enemyCard = StateMachine.EnemyCardsOnTable[i];
            if (!StateMachine.TryAttackPlayerCrystal(enemyCard.CombatDTO.Damage))
            {
                Debug.LogError("player is dead");
                StateMachine.SetState(new LoseState(StateMachine));
            }
        }
    }

    private bool PlayerHasMoreCards(int index)
    {
        return index + 1 > StateMachine.EnemyCardsOnTable.Count;
    }
}
