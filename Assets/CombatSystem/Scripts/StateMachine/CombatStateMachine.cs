using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [SerializeField] private DragNDropTable table;
    [SerializeField] private EnemyInitializer enemyInitializer;
    [SerializeField] private ManaPanel manaPanel;
    [SerializeField] private LifeCrystalPanel lifeCrystalPanel;
    [SerializeField] private LifeCrystalPanel enemyCrystalPanel;

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

    public event Action OnEndTurn;

    public int CardsDrawnPerTurn { get; private set; } = 1;
    public int PlayerCrystals => lifeCrystalPanel.Amount;
    public int EnemyCrystals => enemyCrystalPanel.Amount;
    public int PlayerMana { get; private set; } = 3;
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
        StartCoroutine(AddCard(card));
        return true;
    }

    public void AddCardOnEnemyTable(Card card)
    {
        EnemyCardsOnTable.Add(card);
        StartCoroutine(AddCard(card));
    }

    private IEnumerator AddCard(Card card)
    {
        foreach (var effect in card.CombatDTO.OnUseEffects)
        {
            yield return effect.StartEffect(this, card);
        }
    }

    public IEnumerator DestroyCard(Card card)
    {
        if (card.IsDestroyed)
            yield break;

        card.IsDestroyed = true;
        foreach (var effect in card.CombatDTO.OnDeathEffects)
        {
            yield return effect.StartEffect(this, card);
            if (effect.PreventDeath)
                card.IsDestroyed = false;
        }

        if (card.IsDestroyed)
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

        StartCoroutine(CleanBoardAfterTurn());
    }

    private IEnumerator CleanBoardAfterTurn()
    {
        var deadCards = PlayerCardsOnTable.Where(card => !card.CombatDTO.IsAlive).ToList();
        foreach (var card in deadCards)
        {
            yield return DestroyCard(card);
        }

        deadCards = EnemyCardsOnTable.Where(card => !card.CombatDTO.IsAlive).ToList();
        foreach (var card in deadCards)
        {
            yield return DestroyCard(card);
        }

        ChangeTurn();

    }

    public void ChangeTurn()
    {
        if (_isPlayerTurn)
        {
            _isPlayerTurn = !_isPlayerTurn;
            PlayerMana = PlayerDeck.MaxCrystals + 3 - PlayerCrystals;
            SetState(_playerState);
        }
        else
        {
            _isPlayerTurn = !_isPlayerTurn;
            SetState(_enemyState);
        }
    }

    public bool TryAttackEnemyCrystal(int damage)
    {
        enemyCrystalPanel.AttackCrystal(damage);
        return EnemyCrystals > 0;
    }

    public bool TryAttackPlayerCrystal(int damage)
    {
        lifeCrystalPanel.AttackCrystal(damage);
        return PlayerCrystals > 0;
    }
}
