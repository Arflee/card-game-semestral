using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1AttackState : AttackState
{
    private readonly Queue<DialogueSequence> dialogues;

    public Tutorial1AttackState(CombatStateMachine machine, Queue<DialogueSequence> dialogues) : base(machine)
    {
        this.dialogues = dialogues;
    }

    public override IEnumerator EnterState()
    {
        yield return StateMachine.DialogueCoroutine(dialogues.Dequeue());
        yield return base.EnterState();
    }
}
