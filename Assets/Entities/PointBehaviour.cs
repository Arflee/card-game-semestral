using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PointBehaviour : BehaviourState
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float distanceThreshold = 0.1f;

    public Vector2[] points = new Vector2[0];

    [Header("Gizmos")]
    public Color gizmosColor = Color.blue;
}
