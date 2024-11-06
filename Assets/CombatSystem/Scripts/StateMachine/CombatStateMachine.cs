using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatStateMachine : MonoBehaviour
{
    [SerializeField] private CardDeck playerDeck;
    [SerializeField] private DragNDropTable table;
    [SerializeField] private EnemyInitializer enemyInitializer;
    [SerializeField] private ManaPanel manaPanel;

    private bool _isPlayerTurn = false;
    private PlayerState _playerState;
    private EnemyState _enemyState;

    public CardDeck PlayerDeck => playerDeck;
    public EnemyInitializer EnemyInitializer => enemyInitializer;
    public CombatState State { get; private set; }
    public List<Card> PlayerCardsOnTable { get; private set; } = new();
    public List<Card> EnemyCardsOnTable { get; private set; } = new();
    public ManaPanel ManaPanel => manaPanel;

    public event Action OnEndTurn;

    public int PlayerCrystals { get; private set; } = 3;
    public int EnemyCrystals { get; private set; } = 3;

    public int PlayerMana { get; private set; } = 3;
    public int PlayerManaNextTurn { get; private set; } = 4;
    public int MaxPlayerMana { get; private set; } = 10;

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
            Debug.Log("used effect");
            effect.OnUse(PlayerDeck, card, PlayerCardsOnTable);
        }

        return true;
    }

    public void AddCardOnEnemyTable(Card card)
    {
        EnemyCardsOnTable.Add(card);
    }

    private void RemoveCardFromTable(Card card)
    {
        Destroy(card.CardVisual.gameObject);
        Destroy(card.transform.parent.gameObject);
    }

    public void OnTurnEndButtonClicked()
    {
        OnEndTurn?.Invoke();

        List<Card> cardsToBeAdded = new();

        var deadCards = PlayerCardsOnTable.Where(card => !card.CombatDTO.IsAlive);

        foreach (var card in deadCards)
        {
            foreach (var effect in card.CombatDTO.CardEffects)
            {
                var createdCard = effect.OnDeathCreateCard(card, table.PlayerTableSide);
                if (createdCard)
                {
                    cardsToBeAdded.Add(createdCard);
                }
            }
        }

        PlayerCardsOnTable = PlayerCardsOnTable.Except(deadCards).ToList();
        PlayerCardsOnTable.AddRange(cardsToBeAdded);

        foreach (var card in deadCards)
        {
            RemoveCardFromTable(card);
        }

        deadCards = EnemyCardsOnTable.Where(card => !card.CombatDTO.IsAlive);
        EnemyCardsOnTable = EnemyCardsOnTable.Except(deadCards).ToList();
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
