using System.Collections;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [SerializeField] private CardDeck playerDeck;
    [SerializeField] private DragNDropTable table;

    private PlayerState _playerState;

    public CardDeck PlayerDeck => playerDeck;

    public CombatState State { get; private set; }

    private void Start()
    {
        table.OnTableSlotSnapped += OnCardDragEnd;
        _playerState = new PlayerState(this);

       StartCoroutine(new PreCombatState(this).EnterState());
    }

    private void OnCardDragEnd(CombatSlot slot)
    {
        StartCoroutine(_playerState.Attack(slot));
    }

    public void OnTurnEndButtonClicked()
    {
        StartCoroutine(_playerState.EndTurn(table));
    }
}
