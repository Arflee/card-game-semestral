using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2BegginingState : EnemyState
{
    private readonly Queue<DialogueSequence> dialogues;

    public Tutorial2BegginingState(CombatStateMachine machine, Queue<DialogueSequence> dialogues) : base(machine)
    {
        this.dialogues = dialogues;
    }

    public override IEnumerator EnterState()
    {
        yield return StateMachine.DialogueCoroutine(dialogues.Dequeue());
        yield return base.EnterState();
    }
}
