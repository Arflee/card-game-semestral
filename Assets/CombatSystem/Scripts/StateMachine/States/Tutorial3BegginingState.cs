using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3BegginingState : EnemyState
{
    private readonly Queue<DialogueSequence> dialogues;

    public Tutorial3BegginingState(CombatStateMachine machine, Queue<DialogueSequence> dialogues) : base(machine)
    {
        this.dialogues = dialogues;
    }

    public override IEnumerator EnterState()
    {
        if (StateMachine.PlayerCrystals > 1)
            yield return StateMachine.DialogueCoroutine(dialogues.Dequeue());
        yield return base.EnterState();
    }
}
