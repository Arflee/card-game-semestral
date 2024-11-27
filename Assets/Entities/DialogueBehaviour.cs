using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBehaviour : BehaviourState
{
    [SerializeField] private DialogueTrigger dialogue;

    [SerializeField, Header("States")]
    private BehaviourState nextState;

    protected override void OnEnable()
    {
        dialogue.OnDialogueEnd += Finished;
        dialogue.EnableDialogue();
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }
}
