using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Sequence", menuName = "Dialogues/Create New Dialogue Sequence", order = 1)]
public class DialogueSequence : ScriptableObject
{
    [SerializeField] protected List<DialogueSpeaker> monologues;
    [SerializeField] protected float delayPerSymbol;
    [SerializeField] protected List<DialogueChoice> availableChoices;

    public float DelayPerSymbol => delayPerSymbol;

    public virtual List<DialogueSpeaker> Monologues => monologues;

    public virtual List<DialogueChoice> AvaliableChoices => availableChoices;

}

[System.Serializable]
public class DialogueSpeaker
{
    public string speakerName;
    [TextArea(3, 50)] public string sequence;
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceDescription;
    public DialogueSequence avalableChoice;
}
