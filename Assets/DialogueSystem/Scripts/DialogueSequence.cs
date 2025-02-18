using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue Sequence", menuName = "Dialogues/Create New Dialogue Sequence", order = 1)]
public class DialogueSequence : ScriptableObject
{
    [SerializeField] protected List<DialogueSpeaker> monologues;
    [SerializeField] protected float delayPerSymbol;
    [SerializeField] protected List<DialogueChoice> availableChoices;

    public float DelayPerSymbol => delayPerSymbol;

    public virtual List<DialogueSpeaker> Monologues => monologues;

    public virtual List<DialogueChoice> AvailableChoices => availableChoices;

}

[System.Serializable]
public class DialogueSpeaker
{
    public DialogueSpeaker()
    {
        
    }
    public DialogueSpeaker(string name, string sequence)
    {
        speakerName = name;
        this.sequence = sequence;
    }

    public string speakerName;
    [TextArea(3, 50)] public string sequence;
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceDescription;
    public DialogueSequence avalableChoice;
}
