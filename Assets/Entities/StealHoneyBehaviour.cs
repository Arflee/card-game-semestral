using System.Collections.Generic;
using UnityEngine;

public class StealHoneyBehaviour : BehaviourState
{
    [SerializeField] private List<SpriteRenderer> honeyTrees;
    [SerializeField] private Sprite honeylessTree;
    [SerializeField] private BehaviourState nextState;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (var item in honeyTrees)
        {
            item.sprite = honeylessTree;
        }
    }

    private void Update()
    {
        Finished();
    }
}
