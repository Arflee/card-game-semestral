using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AttackState : CombatState
{
    private readonly float _attackDuration = 0.3f;
    private readonly float _pauseDuration = 0.5f;
    private readonly float _returnDuration = 0.1f;
    private readonly float _betweenAttackDuration = 0.1f;

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
        int attacking = 0;

        for (int i = 0; i < StateMachine.PlayerCardsOnTable.Count; i++)
        {
            var playerCard = StateMachine.PlayerCardsOnTable[i];
            Sequence attackSequence = DOTween.Sequence();
            var originalPosition = playerCard.CardVisual.transform.position;

            if (PlayerHasMoreCards(i))
            {
                if (!StateMachine.TryGetEnemyCrystalPos(out var crystal))
                {
                    Debug.Log("player wins");
                    StateMachine.SetState(new WinState(StateMachine));
                    break;
                }

                attackSequence.Append(playerCard.CardVisual.transform.DOMove(crystal, _attackDuration)
                    .SetEase(Ease.OutExpo));
                attackSequence.AppendInterval(_pauseDuration);
                attackSequence.Append(playerCard.CardVisual.transform.DOMove(originalPosition, _returnDuration)
                    .SetEase(Ease.OutQuint));

                bool attackingCrystal = true;
                attackSequence.OnComplete(() =>
                {
                    attackingCrystal = false;
                    if (!StateMachine.TryAttackEnemyCrystal(playerCard.CombatDTO.Damage))
                    {
                        Debug.Log("player wins");
                        StateMachine.SetState(new WinState(StateMachine));
                    }
                });

                while (attackingCrystal)
                {
                    yield return 1;
                }
                continue;
            }

            var enemyCard = StateMachine.EnemyCardsOnTable[i];

            attackSequence.Append(playerCard.CardVisual.transform.DOMove(enemyCard.CardVisual.transform.position, _attackDuration)
                .SetEase(Ease.OutExpo));
            attackSequence.AppendInterval(_pauseDuration);
            attackSequence.Append(playerCard.CardVisual.transform.DOMove(originalPosition, _returnDuration)
                .SetEase(Ease.OutQuint));

            attacking++;
            attackSequence.OnComplete(() =>
            {
                attacking--;
                enemyCard.TakeDamageFrom(playerCard);
                playerCard.TakeDamageFrom(enemyCard);
            });

            yield return new WaitForSeconds(_betweenAttackDuration);
        }

        for (int i = StateMachine.PlayerCardsOnTable.Count; i < StateMachine.EnemyCardsOnTable.Count; i++)
        {
            var enemyCard = StateMachine.EnemyCardsOnTable[i];
            Sequence attackSequence = DOTween.Sequence();
            var originalPosition = enemyCard.CardVisual.transform.position;

            if (!StateMachine.TryGetPlayerCrystalPos(out var crystal))
            {
                Debug.Log("player wins");
                StateMachine.SetState(new WinState(StateMachine));
                break;
            }

            attackSequence.Append(enemyCard.CardVisual.transform.DOMove(crystal, _attackDuration)
                .SetEase(Ease.OutExpo));
            attackSequence.AppendInterval(_pauseDuration);
            attackSequence.Append(enemyCard.CardVisual.transform.DOMove(originalPosition, _returnDuration)
                .SetEase(Ease.OutQuint));

            bool attackingCrystal = true;
            attackSequence.OnComplete(() =>
            {
                attackingCrystal = false;
                if (!StateMachine.TryAttackPlayerCrystal(enemyCard.CombatDTO.Damage))
                {
                    Debug.Log("player loses");
                    StateMachine.SetState(new LoseState(StateMachine));
                }
            });

            while (attackingCrystal)
            {
                yield return 1;
            }
        }

        while (attacking > 0)
        {
            yield return 1;
        }
        yield return StateMachine.CleanBoardAfterTurn();
        StateMachine.SetState(NextState());
    }

    public override CombatState NextState()
    {
        return new EnemyState(StateMachine);
    }
}
