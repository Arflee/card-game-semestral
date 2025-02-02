using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1BegginingState : EnemyState
{
    private readonly Queue<DialogueSequence> dialogues;

    public Tutorial1BegginingState(CombatStateMachine machine, Queue<DialogueSequence> dialogues) : base(machine)
    {
        this.dialogues = dialogues;
    }

    public override IEnumerator EnterState()
    {
        yield return StateMachine.DialogueCoroutine(dialogues.Dequeue());
        yield return base.EnterState();
    }

    public override CombatState NextState()
    {
        return new Tutorial1PlayerTurnState(StateMachine, dialogues);
    }
}
