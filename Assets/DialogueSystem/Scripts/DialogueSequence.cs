using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Sequence", menuName = "Create New Dialogue Sequence", order = 1)]
public class DialogueSequence : ScriptableObject
{
    public List<DialogueSpeaker> monologues;
    public float delayPerSymbol;
    public List<DialogueChoice> availableChoices;
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
