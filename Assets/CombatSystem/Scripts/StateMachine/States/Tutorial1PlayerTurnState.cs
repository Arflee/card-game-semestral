using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1PlayerTurnState : PlayerState
{
    private readonly Queue<DialogueSequence> dialogues;
    private int addedCards;

    public Tutorial1PlayerTurnState(CombatStateMachine machine, Queue<DialogueSequence> dialogues) : base(machine)
    {
        this.dialogues = dialogues;
    }

    public override IEnumerator EnterState()
    {
        yield return base.EnterState();
        StateMachine.CanPlayCard = false;
        StateMachine.EndTurnButton.interactable = false;
        yield return StateMachine.DialogueCoroutine(dialogues.Dequeue());
        StateMachine.CanPlayCard = true;
    }

    public override void CardAdded(Card card)
    {
        if (addedCards == 0)
            StateMachine.Dialogue(dialogues.Dequeue(), () => StateMachine.EndTurnButton.interactable = true);
        addedCards++;
    }

    public override CombatState NextState()
    {
        return new Tutorial1AttackState(StateMachine, dialogues);
    }
}
