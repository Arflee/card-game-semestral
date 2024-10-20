using System.Collections;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [SerializeField] private CardDeck playerDeck;
    [SerializeField] private DragNDropTable table;
    [SerializeField] private EnemyInitializer enemyInitializer;

    //private PlayerState _playerState;

    public CardDeck PlayerDeck => playerDeck;
    public EnemyInitializer EnemyInitializer => enemyInitializer;
    public CombatState State { get; private set; }
    public DragNDropTable Table { get; private set; }

    private void Start()
    {
        //table.OnTableSlotSnapped += OnCardDragEnd;
        //_playerState = new PlayerState(this);

        SetState(new PreCombatState(this));
    }

    public void SetState(CombatState state)
    {
        State = state;
        StartCoroutine(State.EnterState());
    }

    public void OnTurnEndButtonClicked()
    {
        //TODO
        StartCoroutine(_playerState.EndTurn(table));
    }
}
