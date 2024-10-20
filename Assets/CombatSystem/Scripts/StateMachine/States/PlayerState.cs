using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerState : CombatState
{
    public PlayerState(CombatStateMachine machine) : base(machine)
    {
    }

    public override IEnumerator EnterState()
    {
        StateMachine.OnEndTurn += OnEndTurn;
        yield return null;
    }

    private void OnEndTurn()
    {
        foreach (var slot in StateMachine.PlayerCardsOnTable)
        {
            CombatCardDTO oppositeCard = StateMachine.Table.GetOppositeCard(slot);

            if (oppositeCard == null)
            {
                Debug.LogWarning("Opposite slot is empty");
                continue;
            }

            oppositeCard.TakeDamage(slot.CardInSlot);
            slot.CardInSlot.TakeDamage(oppositeCard);
        }

        StateMachine.SetState(new EnemyState(StateMachine));
    }
}
