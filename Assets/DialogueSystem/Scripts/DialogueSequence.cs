using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Sequence", menuName = "Create New Dialogue Sequence", order = 1)]
public class DialogueSequence : ScriptableObject
{
    [TextArea(3, 50)]
    public string[] sequence;

    public float delayPerSymbol;
}
