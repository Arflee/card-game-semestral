using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Pospec.Helper.Audio;

public class CombatStateMachine : MonoBehaviour
{
    [SerializeField] private DragNDropTable table;
    [SerializeField] private EnemyInitializer enemyInitializer;
    [SerializeField] private ManaPanel manaPanel;
    [SerializeField] private LifeCrystalPanel lifeCrystalPanel;
    [SerializeField] private LifeCrystalPanel enemyCrystalPanel;
    [SerializeField] private Button endTurnButton;
    [SerializeField] private TextMeshProUGUI endTurnText;
    [SerializeField, Range(0, 10)] private int maxCardsOnBoard;
    [SerializeField] private DialogueTrigger dialogue;

    [Header("Sounds")]
    [SerializeField] private Sound playCardSound;
    [SerializeField] private Sound notEnoughtManaSound;
    [SerializeField] private Sound cardsColideSound;
    [SerializeField] private Sound attackToCrystalSound;
    [SerializeField] private Sound crystalBreaksSound;
    [SerializeField] private Sound cardDestroyedSound;

    public static bool GameActive { get; private set; } = false;

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
    public int PlayerMana { get; set; } = 3;
    public int MaxCardsOnBoard => maxCardsOnBoard;
    public GameEndingHandler GameHandler => _gameStateHandler;
    public bool CanPlayCard { get; set; }
    public int CurrentTurn { get; set; } = 0;
    public Sound CardsColideSound => cardsColideSound;
    public Sound AttackToCrystalSound => attackToCrystalSound;
    public Sound CrystalBreaksSound => crystalBreaksSound;

    private void OnEnable()
    {
        GameActive = true;
        _playerDeck = FindObjectOfType<CardDeck>();
        _gameStateHandler = FindObjectOfType<GameEndingHandler>();

        table.OnTableSlotSnapped += OnCardDragEnd;

        PlayerOwner = new CardOwner(_playerDeck, PlayerCardsOnTable, EnemyCardsOnTable);
        EnemyOwner = new CardOwner(null, EnemyCardsOnTable, PlayerCardsOnTable);

        lifeCrystalPanel.Initialize(PlayerDeck.Crystals);

        SetState(new PreCombatState(this, enemyInitializer.StartingCombatState(this)));
    }

    private void OnDisable()
    {
        GameActive = false;
        table.OnTableSlotSnapped -= OnCardDragEnd;
    }

    public void SetState(CombatState state)
    {
        State = state;
        StartCoroutine(State.EnterState());
    }

    private bool OnCardDragEnd(Card card)
    {
        if (PlayerCardsOnTable.Count >= MaxCardsOnBoard || !CanPlayCard)
            return false;

        if (card.CombatDTO.ManaCost > PlayerMana)
        {
            SoundManager.Instance.Play(notEnoughtManaSound);
            manaPanel.NotEnoughManaAnimation();
            return false;
        }

        PlayerCardsOnTable.Add(card);
        PlayerMana -= card.CombatDTO.ManaCost;
        manaPanel.UseManaCrystals(card.CombatDTO.ManaCost);
        StartCoroutine(AddCard(card));
        SoundManager.Instance.Play(playCardSound);
        return true;
    }

    public void AddCardOnEnemyTable(Card card)
    {
        if (EnemyCardsOnTable.Count >= MaxCardsOnBoard)
        {
            RemoveCardFromTable(card);
            return;
        }

        card.CardVisual.LocalCanvas.sortingLayerName = "Background";

        EnemyCardsOnTable.Add(card);
        StartCoroutine(AddCard(card));
    }

    private IEnumerator AddCard(Card card)
    {
        card.TurnPlayed = CurrentTurn;
        SoundManager.Instance.Play(playCardSound);
        foreach (var effect in card.CombatDTO.OnUseEffects)
        {
            yield return effect.StartEffect(this, card);
        }

        State.CardAdded(card);
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

        SoundManager.Instance.Play(cardDestroyedSound);
    }

    private void RemoveCardFromTable(Card card)
    {
        Destroy(card.CardVisual.gameObject);
        Destroy(card.transform.parent.gameObject);
    }

    public void OnTurnEndButtonClicked()
    {
        SetEndTurnButtonActive(false);
        SetState(State.NextState());
    }

    public IEnumerator CleanBoardAfterTurn()
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
    }

    public bool TryAttackEnemyCrystal(int damage)
    {
        enemyCrystalPanel.AttackCrystal(damage);
        return EnemyCrystals > 0;
    }

    public bool TryGetEnemyCrystalPos(out Vector3 position)
    {
        return enemyCrystalPanel.TryGetCrystalPos(out position);
    }

    public bool TryAttackPlayerCrystal(int damage)
    {
        lifeCrystalPanel.AttackCrystal(damage);
        return PlayerCrystals > 0;
    }

    public bool TryGetPlayerCrystalPos(out Vector3 position)
    {
        return lifeCrystalPanel.TryGetCrystalPos(out position);
    }

    public IEnumerator DialogueCoroutine(DialogueSequence seq)
    {
        bool dialogueFinished = false;

        dialogue.OnDialogueEnd += () => dialogueFinished = true;
        dialogue.EnableDialogue(seq);

        while (!dialogueFinished)
            yield return 0;
    }

    public void Dialogue(DialogueSequence seq, Action onDialogueEnd)
    {
        dialogue.OnDialogueEnd += onDialogueEnd;
        dialogue.EnableDialogue(seq);
    }

    public void SetEndTurnButtonActive(bool isActive)
    {
        endTurnButton.interactable = isActive;
        endTurnText.text = isActive ? "Ukončit tah" : "Nejsi na tahu";
    }
}
