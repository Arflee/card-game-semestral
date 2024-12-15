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
    public CardOwner PlayerOwner { get; private set; }
    public CardOwner EnemyOwner { get; private set; }
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

        PlayerOwner = new CardOwner(_playerDeck, PlayerCardsOnTable, EnemyCardsOnTable);
        EnemyOwner = new CardOwner(null, EnemyCardsOnTable, PlayerCardsOnTable);

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
            effect.OnUse(PlayerOwner, this, card);
        }

        return true;
    }

    public void AddCardOnEnemyTable(Card card)
    {
        EnemyCardsOnTable.Add(card);
        foreach (var effect in card.CombatDTO.CardEffects)
        {
            effect.OnUse(EnemyOwner, this, card);
        }
    }

    public void DestroyCard(Card card)
    {
        bool destroy = true;
        foreach (var effect in card.CombatDTO.CardEffects)
        {
            if (!effect.Die(card.Owner, this, card))
                destroy = false;
        }

        if (destroy)
        {
            PlayerCardsOnTable.Remove(card);
            EnemyCardsOnTable.Remove(card);
            RemoveCardFromTable(card);
        }
    }

    private void RemoveCardFromTable(Card card)
    {
        Destroy(card.CardVisual.gameObject);
        Destroy(card.transform.parent.gameObject);
    }

    public void OnTurnEndButtonClicked()
    {
        OnEndTurn?.Invoke();

        var deadCards = PlayerCardsOnTable.Where(card => !card.CombatDTO.IsAlive).ToList();
        foreach (var card in deadCards)
        {
            DestroyCard(card);
        }

        deadCards = EnemyCardsOnTable.Where(card => !card.CombatDTO.IsAlive).ToList();
        foreach (var card in deadCards)
        {
            DestroyCard(card);
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
