using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : CombatState
{
    private List<CombatSlot> _playedSlots;

    public PlayerState(CombatStateMachine machine) : base(machine)
    {
        _playedSlots = new();
    }

    public override IEnumerator EnterState()
    {
        return base.EnterState();
    }

    public override IEnumerator Attack(CombatSlot slot)
    {
        _playedSlots.Add(slot);
        yield return null;
    }

    public override IEnumerator EndTurn(DragNDropTable table)
    {
        foreach (var slot in _playedSlots)
        {
            CombatCardDTO oppositeCard = table.GetOppositeCard(slot);

            if (oppositeCard == null)
            {
                Debug.LogWarning("Opposite slot is empty");
                continue;
            }

            oppositeCard.TakeDamage(slot.CardInSlot);
            slot.CardInSlot.TakeDamage(oppositeCard);
        }

        yield return null;
    }
}
