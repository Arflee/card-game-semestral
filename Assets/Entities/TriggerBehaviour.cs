using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehaviour : BasicBehaviour
{
    [SerializeField] private Collider2D triggerArea;
    [SerializeField] private LayerMask layerMask;

    private bool running = false;
    private ContactFilter2D contactFilter;

    private void Start()
    {
        contactFilter.SetLayerMask(layerMask);
    }

    protected override void Update()
    {
        if (!running)
            running = Physics2D.OverlapCollider(triggerArea, contactFilter, new Collider2D[1]) > 0;

        if (running)
            base.Update();
    }
}
