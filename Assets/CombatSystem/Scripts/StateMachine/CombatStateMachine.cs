using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [SerializeField] private CardDeck playerDeck;
    [SerializeField] private DragNDropTable table;
    [SerializeField] private EnemyInitializer enemyInitializer;

    private bool _isPlayerTurn = false;
    private PlayerState _playerState;
    private EnemyState _enemyState;

    public CardDeck PlayerDeck => playerDeck;
    public EnemyInitializer EnemyInitializer => enemyInitializer;
    public CombatState State { get; private set; }
    public List<Card> PlayerCardsOnTable { get; private set; } = new();
    public List<Card> EnemyCardsOnTable { get; private set; } = new();

    public event Action OnEndTurn;

    public int PlayerCrystals { get; private set; } = 3;
    public int EnemyCrystals { get; private set; } = 3;

    private void Start()
    {
        _playerState = new PlayerState(this);
        _enemyState = new EnemyState(this);

        table.OnTableSlotSnapped += OnCardDragEnd;

        SetState(new PreCombatState(this));
    }

    private void OnDisable()
    {
        table.OnTableSlotSnapped -= OnCardDragEnd;
    }

    private void OnCardDragEnd(Card card)
    {
        AddCardOnPlayerTable(card);
    }

    private void OnDieEvent(Card card)
    {
        card.OnDieEvent -= OnDieEvent;

        Destroy(card.CardVisual.gameObject);
        Destroy(card.gameObject);
    }

    private void OnPlayerCardDie(Card card)
    {
        card.OnDieEvent -= OnPlayerCardDie;
        PlayerCardsOnTable.Remove(card);
        Debug.Log(PlayerCardsOnTable.Count);
    }

    private void OnEnemyCardDie(Card card)
    {
        card.OnDieEvent -= OnEnemyCardDie;
        EnemyCardsOnTable.Remove(card);
        Debug.Log(EnemyCardsOnTable.Count);
    }

    public void SetState(CombatState state)
    {
        State = state;
        StartCoroutine(State.EnterState());
    }

    public void AddCardOnPlayerTable(Card card)
    {
        PlayerCardsOnTable.Add(card);
        card.OnDieEvent += OnDieEvent;
        card.OnDieEvent += OnPlayerCardDie;
    }

    public void AddCardOnEnemyTable(Card card)
    {
        EnemyCardsOnTable.Add(card);
        card.OnDieEvent += OnDieEvent;
        card.OnDieEvent += OnEnemyCardDie;
    }

    public void OnTurnEndButtonClicked()
    {
        OnEndTurn?.Invoke();
    }

    public void ChangeTurn()
    {
        if (_isPlayerTurn)
        {
            _isPlayerTurn = !_isPlayerTurn;
            SetState(_playerState);
        }
        else
        {
            _isPlayerTurn = !_isPlayerTurn;
            SetState(_enemyState);
        }
    }
}
