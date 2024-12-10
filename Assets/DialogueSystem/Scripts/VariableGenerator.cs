using System.Collections.Generic;
using UnityEngine;

public abstract class VariableGenerator : ScriptableObject
{
    public abstract string[] GetNextVariable();
}
