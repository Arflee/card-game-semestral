using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBridgeBehaviour : BehaviourState
{
    [SerializeField] BehaviourState nextState;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float distanceThreshold = 0.1f;

    private Vector2 target;
    private Bridge bridge;

    public void Setup(Vector2 target, Bridge bridge)
    {
        this.target = target;
        this.bridge = bridge;
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (var item in GetComponents<BehaviourState>())
            if (item != this)
                item.enabled = false;
    }

    private void Update()
    {

        Vector2 targetDirection = target - (Vector2)transform.position;
        float distance = targetDirection.magnitude;
        if (distance <= distanceThreshold)
        {
            Finished();
            bridge.Colapase();
            return;
        }

        transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
    }

    public void BridgeWasDestroyed()
    {
        if (!enabled)
            return;

        Finished();
    }
}
