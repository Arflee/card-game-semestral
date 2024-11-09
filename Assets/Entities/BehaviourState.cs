using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourState : MonoBehaviour
{
    public bool Finished { get; protected set; } = false;
    public abstract BehaviourState NextState();
}
