using UnityEngine;

public class BehaviourStateMachine : MonoBehaviour
{
    [System.Serializable]
    public class BasicBehaviourState
    {
        public BasicBehaviour active;
        public int nextStateIndex;
        public float maxTime;
    }

    [SerializeField] private BasicBehaviourState[] states;
    private BasicBehaviourState currentState;
    float stateTimeLeft;

    private void OnValidate()
    {
        foreach (BasicBehaviourState state in states)
            if (state.nextStateIndex < 0 || state.nextStateIndex >= states.Length)
                Debug.LogError($"Invalid index: {state.nextStateIndex}", this);
    }

    void Start()
    {
        foreach (BasicBehaviourState state in states)
        {
            state.active.enabled = false;
            if (state.nextStateIndex < 0 || state.nextStateIndex >= states.Length)
                Debug.LogError($"Invalid index: {state.nextStateIndex}", this);
        }
        SwitchState(states[0]);
    }

    public void SwitchState(BasicBehaviourState nextState)
    {
        if (currentState != null)
            currentState.active.enabled = false;
        currentState = nextState;
        currentState.active.enabled = true;
        stateTimeLeft = currentState.maxTime;
    }

    private void Update()
    {
        if (currentState.active.finnished || (stateTimeLeft < 0 && currentState.maxTime > 0))
        {
            SwitchState(states[currentState.nextStateIndex]);
        }

        stateTimeLeft -= Time.deltaTime;
    }
}
