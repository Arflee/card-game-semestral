using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicBehaviorState
{
    public BasicBehaviour active;
    public BasicBehaviorState next;
    public float maxTime;
}
