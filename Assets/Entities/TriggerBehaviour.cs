using UnityEngine;

public class TriggerBehaviour : BehaviourState
{
    [SerializeField] private Collider2D triggerArea;
    [SerializeField] private LayerMask layerMask;

    [Header("States")]
    [SerializeField] public BehaviourState nextState;

    private ContactFilter2D contactFilter;

    private void Start()
    {
        contactFilter.SetLayerMask(layerMask);
    }

    protected virtual void Update()
    {
        if (Physics2D.OverlapCollider(triggerArea, contactFilter, new Collider2D[1]) > 0)
            Finished();
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }
}
