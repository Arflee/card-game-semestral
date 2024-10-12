using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [SerializeField] private CardDeck playerDeck;

    public CardDeck PlayerDeck => playerDeck;

    public CombatState State { get; private set; }

    private void Start()
    {
        SetState(new PreCombatState(this));
    }

    public void SetState(CombatState state)
    {
        State = state;
        StartCoroutine(State.EnterState());
    }
}
