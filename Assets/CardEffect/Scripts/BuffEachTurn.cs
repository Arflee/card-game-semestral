using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffEachTurn", menuName = "Card Effects/On Turn Start/BuffEachTurn")]
public class BuffEachTurn : CardEffect
{
    [SerializeField] private int healthBuff = 1;
    [SerializeField] private int damageBuff = 1;

    public override void OnTurnStart(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        card.BuffHealth(healthBuff);
        card.BuffDamage(damageBuff);
    }
}
