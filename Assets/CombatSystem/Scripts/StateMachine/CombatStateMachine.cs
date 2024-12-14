using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [SerializeField] private DragNDropTable table;
    [SerializeField] private EnemyInitializer enemyInitializer;
    [SerializeField] private ManaPanel manaPanel;
    [SerializeField] private LifeCrystalPanel lifeCrystalPanel;

    private bool _isPlayerTurn = false;
    private PlayerState _playerState;
    private EnemyState _enemyState;
    private CardDeck _playerDeck;
    private GameEndingHandler _gameStateHandler;

    public CardDeck PlayerDeck => _playerDeck;
    public EnemyInitializer EnemyInitializer => enemyInitializer;
    public CombatState State { get; private set; }
    public List<Card> PlayerCardsOnTable { get; private set; } = new();
    public List<Card> EnemyCardsOnTable { get; private set; } = new();
    public ManaPanel ManaPanel => manaPanel;
    public LifeCrystalPanel LifeCrystalPanel => lifeCrystalPanel;

    public event Action OnEndTurn;

    public int PlayerCrystals { get; private set; } = 3;
    public int EnemyCrystals { get; private set; } = 3;
    public int PlayerMana { get; private set; } = 3;
    public int PlayerManaNextTurn { get; private set; } = 4;
    public int MaxPlayerMana { get; private set; } = 10;
    public GameEndingHandler GameHandler => _gameStateHandler;


    private void Start()
    {
        _playerDeck = FindObjectOfType<CardDeck>();
        _playerState = new PlayerState(this);
        _enemyState = new EnemyState(this);

        _gameStateHandler = FindObjectOfType<GameEndingHandler>();

        table.OnTableSlotSnapped += OnCardDragEnd;

        SetState(new PreCombatState(this));
    }

    private void OnDisable()
    {
        table.OnTableSlotSnapped -= OnCardDragEnd;
    }

    public void SetState(CombatState state)
    {
        State = state;
        StartCoroutine(State.EnterState());
    }

    private bool OnCardDragEnd(Card card)
    {
        if (card.CombatDTO.ManaCost > PlayerMana) return false;

        PlayerCardsOnTable.Add(card);
        PlayerMana -= card.CombatDTO.ManaCost;
        manaPanel.UseManaCrystals(card.CombatDTO.ManaCost);

        foreach (var effect in card.CombatDTO.CardEffects)
        {
            effect.OnUse(State, this);
        }

        return true;
    }

    public void AddCardOnEnemyTable(Card card)
    {
        EnemyCardsOnTable.Add(card);
        foreach (var effect in card.CombatDTO.CardEffects)
        {
            effect.OnUse(State, this);
        }
    }

    public void RemoveCardFromTable(Card card)
    {
        Destroy(card.CardVisual.gameObject);
        Destroy(card.transform.parent.gameObject);
    }

    public void OnTurnEndButtonClicked()
    {
        OnEndTurn?.Invoke();

        List<Card> cardsToBeAdded = new();

        var deadCards = PlayerCardsOnTable.Where(card => !card.CombatDTO.IsAlive);
        var deadThatSurvived = new List<Card>();

        foreach (var card in deadCards)
        {
            foreach (var effect in card.CombatDTO.CardEffects)
            {
                if (!effect.Die(State, this, card))
                    deadThatSurvived.Add(card);
            }
        }

        deadCards = deadCards.Except(deadThatSurvived);
        PlayerCardsOnTable = PlayerCardsOnTable.Except(deadCards).ToList();
        PlayerCardsOnTable.AddRange(cardsToBeAdded);

        foreach (var card in deadCards)
        {
            RemoveCardFromTable(card);
        }

        deadCards = EnemyCardsOnTable.Where(card => !card.CombatDTO.IsAlive);
        EnemyCardsOnTable = EnemyCardsOnTable.Except(deadCards).ToList();

        foreach (var card in deadCards)
        {
            RemoveCardFromTable(card);
        }

        ChangeTurn();
    }

    public void ChangeTurn()
    {
        if (_isPlayerTurn)
        {
            _isPlayerTurn = !_isPlayerTurn;
            PlayerMana = PlayerManaNextTurn;
            PlayerManaNextTurn = Math.Clamp(PlayerManaNextTurn + 1, 0, MaxPlayerMana);
            SetState(_playerState);
        }
        else
        {
            _isPlayerTurn = !_isPlayerTurn;
            SetState(_enemyState);
        }
    }
}
