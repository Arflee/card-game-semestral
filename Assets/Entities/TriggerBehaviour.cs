using UnityEngine;

public class TriggerBehaviour : BehaviourState
{
    [SerializeField] private Collider2D triggerArea;
    [SerializeField] private LayerMask layerMask;

    [Header("States")]
    [SerializeField] public BehaviourState nextState;

    private ContactFilter2D contactFilter;
    private bool triggeredThisFrame = false;

    private void Start()
    {
        contactFilter.SetLayerMask(layerMask);
    }

    protected virtual void Update()
    {
        if (Physics2D.OverlapCollider(triggerArea, contactFilter, new Collider2D[1]) > 0)
        {
            if (triggeredThisFrame)
                return;
            triggeredThisFrame = true;
            Finished();
        }
        else
        {
            triggeredThisFrame = false;
        }
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }
}
