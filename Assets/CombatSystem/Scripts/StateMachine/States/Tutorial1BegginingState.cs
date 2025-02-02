using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1BegginingState : CombatState
{
    private readonly Queue<DialogueSequence> dialogues;

    public Tutorial1BegginingState(CombatStateMachine machine, Queue<DialogueSequence> dialogues) : base(machine)
    {
        this.dialogues = dialogues;
    }

    public override IEnumerator EnterState()
    {
        yield return StateMachine.DialogueCoroutine(dialogues.Dequeue());
        var nextCards = StateMachine.EnemyInitializer.GetNextCards(StateMachine.EnemyOwner);
        foreach (var card in nextCards)
        {
            if (card == null)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                StateMachine.AddCardOnEnemyTable(card);
            }
        }

        StateMachine.SetState(new Tutorial1PlayerTurnState(StateMachine, dialogues));
    }

    public override CombatState NextState()
    {
        return new Tutorial1PlayerTurnState(StateMachine, dialogues);
    }
}
