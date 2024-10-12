

using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    public CombatState State { get; private set; }

    private void Start()
    {
        
    }

    public void SetState(CombatState state)
    {
        State = state;
        StartCoroutine(State.EnterState());
    }
}
