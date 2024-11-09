using UnityEngine;

public class BehaviourStateMachine : MonoBehaviour
{
    [SerializeField] private BehaviourState currentState;

    void Start()
    {
        foreach (BehaviourState state in GetComponents<BehaviourState>())
            state.enabled = false;
        SwitchState(currentState);
    }

    public void SwitchState(BehaviourState nextState)
    {
        if (currentState != null)
            currentState.enabled = false;
        currentState = nextState;
        currentState.enabled = true;
    }

    private void Update()
    {
        if (currentState.Finished)
            SwitchState(currentState.NextState());
    }
}
