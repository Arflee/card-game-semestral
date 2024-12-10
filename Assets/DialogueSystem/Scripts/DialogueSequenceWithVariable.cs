using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Sequence", menuName = "Dialogues/Create New Dialogue Sequence with variable", order = 1)]
public class DialogueSequenceWithVariable : DialogueSequence
{
    [SerializeField] private VariableGenerator variableGenerator;

    private int _variableSequenceStart = 0;
    private int _variableSpeakerStart = 0;
    private int _variableIndex = 0;
    private bool _wasInitialized = false;
    private int _variableLength = 1;

    public override List<DialogueSpeaker> Monologues
    {
        get
        {
            //if (!_wasInitialized)
            //{
            //    _wasInitialized = true;

            //    foreach (var monologue in monologues)
            //    {
            //        int speakerStart = monologue.sequence.IndexOf('[');
            //        _variableSpeakerStart = speakerStart;
            //        int speakerEnd = monologue.sequence.IndexOf(']');

            //        monologue.speakerName =
            //            ReplaceUsingIndices(speakerStart, speakerEnd, monologue.speakerName, variableGenerator.GetNextVariable()[_variableIndex]);

            //        int sequenceStart = monologue.sequence.IndexOf('[');
            //        _variableSequenceStart = sequenceStart;
            //        int sequenceEnd = monologue.sequence.IndexOf(']');

            //        // Concatenate the parts with the replacement
            //        monologue.sequence =
            //            ReplaceUsingIndices(sequenceStart, sequenceEnd, monologue.sequence, variableGenerator.GetNextVariable()[_variableIndex]);

            //    }

            //    _variableIndex++;
            //    _variableLength = variableGenerator.GetNextVariable()[_variableIndex].Length;
            //}
            //else
            //{
            //    foreach (var monologue in monologues)
            //    {
            //        string speakerBefore = monologue.speakerName[.._variableSpeakerStart];
            //        string speakerAfter = monologue.speakerName[(_variableSpeakerStart + _variableLength)..];

            //        // Concatenate the parts with the replacement
            //        monologue.speakerName = speakerBefore + variableGenerator.GetNextVariable()[_variableIndex] + speakerAfter;


            //        string sequenceBefore = monologue.sequence[.._variableSequenceStart];
            //        string sequencerAfter = monologue.sequence[(_variableSequenceStart + _variableLength)..];

            //        // Concatenate the parts with the replacement
            //        monologue.sequence = sequenceBefore + variableGenerator.GetNextVariable()[_variableIndex] + sequencerAfter;
            //    }

            //    _variableIndex++;
            //    _variableLength = variableGenerator.GetNextVariable()[_variableIndex].Length;
            //}


            return monologues;
        }
    }

    public override List<DialogueChoice> AvaliableChoices => base.AvaliableChoices;

    private string ReplaceUsingIndices(int start, int end, string source, string replace)
    {
        if (start != -1 && end != -1 && start < end)
        {
            // Extract parts of the string outside the brackets
            string before = source[..start];
            string after = source[(end + 1)..];

            // Concatenate the parts with the replacement
            return before + replace + after;
        }

        return null;
    }
}
