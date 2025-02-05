using DG.Tweening;
using System;
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
            var originalPosition = playerCard.CardVisual.transform.position;
            Vector3 halfPos;

            if (PlayerHasMoreCards(i))
            {
                if (!StateMachine.TryGetEnemyCrystalPos(out var crystal))
                {
                    Debug.Log("player wins");
                    StateMachine.SetState(new WinState(StateMachine));
                    break;
                }

                attacking++;
                bool attackingCrystal = true;
                halfPos = (crystal - originalPosition) * 0.5f + originalPosition;
                playerCard.CardVisual.transform.DOMove(halfPos, _attackDuration)
                    .SetEase(Ease.OutExpo)
                    .OnComplete(() =>
                    {
                        attackingCrystal = false;
                        if (!StateMachine.TryAttackEnemyCrystal(playerCard.CombatDTO.Damage))
                        {
                            Debug.Log("player wins");
                            StateMachine.SetState(new WinState(StateMachine));
                        }

                        Sequence returnSequence = DOTween.Sequence();
                        returnSequence.AppendInterval(_pauseDuration);
                        returnSequence.Append(playerCard.CardVisual.transform.DOMove(originalPosition, _returnDuration)
                            .SetEase(Ease.OutQuint));
                        returnSequence.OnComplete(() => attacking--);
                    });

                while (attackingCrystal)
                {
                    yield return 1;
                }
                continue;
            }


            var enemyCard = StateMachine.EnemyCardsOnTable[i];
            var enemyPosition = enemyCard.CardVisual.transform.position;
            halfPos = (enemyPosition - originalPosition) * 0.25f + originalPosition;
            attacking++;
            playerCard.CardVisual.transform.DOMove(halfPos, _attackDuration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    enemyCard.TakeDamageFrom(playerCard);
                    playerCard.TakeDamageFrom(enemyCard);

                    Sequence returnSequence = DOTween.Sequence();
                    returnSequence.AppendInterval(_pauseDuration);
                    returnSequence.Append(playerCard.CardVisual.transform.DOMove(originalPosition, _returnDuration)
                        .SetEase(Ease.OutQuint));

                    returnSequence.OnComplete(() => attacking--);
                });

            Sequence enemySequence = DOTween.Sequence();
            halfPos = (originalPosition - enemyPosition) * 0.25f + enemyPosition;
            enemySequence.Append(enemyCard.CardVisual.transform.DOMove(halfPos, _attackDuration)
                .SetEase(Ease.OutExpo));
            enemySequence.AppendInterval(_pauseDuration);
            enemySequence.Append(enemyCard.CardVisual.transform.DOMove(enemyPosition, _returnDuration)
                .SetEase(Ease.OutQuint));
            enemySequence.OnComplete(() => { });

            yield return new WaitForSeconds(_betweenAttackDuration);
        }

        for (int i = StateMachine.PlayerCardsOnTable.Count; i < StateMachine.EnemyCardsOnTable.Count; i++)
        {
            var enemyCard = StateMachine.EnemyCardsOnTable[i];
            var originalPosition = enemyCard.CardVisual.transform.position;

            if (!StateMachine.TryGetPlayerCrystalPos(out var crystal))
            {
                Debug.Log("player loses");
                StateMachine.SetState(new LoseState(StateMachine));
                break;
            }

            attacking++;
            bool attackingCrystal = true;
            Vector3 halfPos = (crystal - originalPosition) * 0.5f + originalPosition;
            enemyCard.CardVisual.transform.DOMove(halfPos, _attackDuration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    attackingCrystal = false;
                    if (!StateMachine.TryAttackPlayerCrystal(enemyCard.CombatDTO.Damage))
                    {
                        Debug.Log("player loses");
                        StateMachine.SetState(new LoseState(StateMachine));
                    }

                    Sequence returnSequence = DOTween.Sequence();
                    returnSequence.AppendInterval(_pauseDuration);
                    returnSequence.Append(enemyCard.CardVisual.transform.DOMove(originalPosition, _returnDuration)
                        .SetEase(Ease.OutQuint));
                    returnSequence.OnComplete(() => attacking--);
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
