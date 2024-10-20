using System;
using System.Collections;
using System.Collections.Generic;
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
    public DragNDropTable Table => table;
    public List<CombatSlot> PlayerCardsOnTable { get; private set; } = new();
    public List<CombatSlot> EnemyCardsOnTable { get; private set; } = new();

    public event Action OnEndTurn;

    private void Start()
    {
        table.OnTableSlotSnapped += OnCardDragEnd;

        SetState(new PreCombatState(this));
    }

    public void SetState(CombatState state)
    {
        State = state;
        StartCoroutine(State.EnterState());
    }

    private void OnCardDragEnd(CombatSlot slot)
    {
        PlayerCardsOnTable.Add(slot);
    }

    public void OnTurnEndButtonClicked()
    {
        OnEndTurn();
    }
}
