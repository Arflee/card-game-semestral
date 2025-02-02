using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AttackState : CombatState
{
    public AttackState(CombatStateMachine machine) : base(machine)
    {
    }

    private bool PlayerHasMoreCards(int index)
    {
        return index + 1 > StateMachine.EnemyCardsOnTable.Count;
    }

    public override IEnumerator EnterState()
    {
        StateMachine.CurrentTurn++;
        for (int i = 0; i < StateMachine.PlayerCardsOnTable.Count; i++)
        {
            var playerCard = StateMachine.PlayerCardsOnTable[i];

            if (PlayerHasMoreCards(i))
            {
                if (!StateMachine.TryAttackEnemyCrystal(playerCard.CombatDTO.Damage))
                {
                    StateMachine.SetState(new WinState(StateMachine));
                }
                Debug.Log("Player attacks crystal");
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            var enemyCard = StateMachine.EnemyCardsOnTable[i];

            enemyCard.TakeDamageFrom(playerCard);
            playerCard.TakeDamageFrom(enemyCard);
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = StateMachine.PlayerCardsOnTable.Count; i < StateMachine.EnemyCardsOnTable.Count; i++)
        {
            var enemyCard = StateMachine.EnemyCardsOnTable[i];
            if (!StateMachine.TryAttackPlayerCrystal(enemyCard.CombatDTO.Damage))
            {
                StateMachine.SetState(new LoseState(StateMachine));
            }
            yield return new WaitForSeconds(0.5f);
        }

        yield return StateMachine.CleanBoardAfterTurn();
        StateMachine.SetState(NextState());
    }

    public override CombatState NextState()
    {
        return new EnemyState(StateMachine);
    }
}
