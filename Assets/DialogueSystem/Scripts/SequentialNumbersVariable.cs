using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Variable", menuName = "Dialogues/Variables/Sequential numbers")]

public class SequentialNumbersVariable : VariableGenerator
{
    [SerializeField] private int sequenceLength = 10;


    public override string[] GetNextVariable()
    {
        return Enumerable.Range(0, sequenceLength).Select(i => i.ToString()).ToArray();
    }
}
