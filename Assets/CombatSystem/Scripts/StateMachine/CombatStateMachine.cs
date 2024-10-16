using System.Collections;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [SerializeField] private CardDeck playerDeck;
    [SerializeField] private DragNDropTable table;

    private PlayerState _playerState;

    public CardDeck PlayerDeck => playerDeck;

    public CombatState State { get; private set; }

    private IEnumerator Start()
    {
        table.OnTableSlotSnapped += OnCardDragEnd;
        yield return new PreCombatState(this).EnterState();
        _playerState = new PlayerState(this);
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
