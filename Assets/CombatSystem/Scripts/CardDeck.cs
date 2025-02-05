using Pospec.Helper.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardDeck : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int initialCardsInHand = 2;
    [SerializeField, Range(1, 10)] private int maxCardsInHand = 7;
    [SerializeField] private List<CombatCard> playerCards;
    [SerializeField] private LifeCrystalParameters crystals;

    [Header("Sounds")]
    [SerializeField] private Sound takeCardSound;
    [SerializeField] private Sound returnCardSound;
    [SerializeField] private Sound destroyCardSound;
    public HashSet<int> Deck { get; private set; } = new HashSet<int>();

    private Queue<CombatCard> _cardDeck;
    private CardHolder _cardHolder;
    private static CardDeck _instance;

    public LifeCrystalParameters Crystals => crystals;
    public int MaxCrystals => crystals.CrystalAmount;
    public int InitialCardsInHand => initialCardsInHand;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.activeSceneChanged += OnNewSceneAdded;
        Deck = new HashSet<int>(Enumerable.Range(0, playerCards.Count));
    }

    private void OnNewSceneAdded(Scene current, Scene next)
    {
        ApplyDeck();
    }

    public void ApplyDeck()
    {
        List<CombatCard> cards = new List<CombatCard>();
        var allCards = GetAllCards();
        foreach (var cardId in Deck)
            cards.Add(allCards[cardId]);

        _cardDeck = new(Utility.Shuffle(cards));
    }

    public CombatCard TakeCard(CardOwner owner)
    {
        if (_cardDeck.Count == 0)
        {
            return null;
        }

        if (_cardHolder == null)
            _cardHolder = FindObjectOfType<CardHolder>();

        var takenCard = _cardDeck.Dequeue();

        SoundManager.Instance.Play(takeCardSound);

        if (_cardHolder.CardsInHand.Count == maxCardsInHand)
        {
            var card = _cardHolder.CreateTempCard(takenCard, owner, _cardHolder.transform.parent);
            StartCoroutine(DiscardCard(card));
            return null;
        }

        _cardHolder.AddCard(takenCard, owner);

        return takenCard;
    }

    private IEnumerator DiscardCard(Card card)
    {
        yield return new WaitForSeconds(1.5f);
        SoundManager.Instance.Play(destroyCardSound);
        Destroy(card.CardVisual.gameObject);
        Destroy(card.transform.parent.gameObject);
    }

    public CombatCard TakeCardWithoutAddingToHolder()
    {
        if (_cardDeck.Count == 0)
        {
            return null;
        }

        var takenCard = _cardDeck.Dequeue();

        return takenCard;
    }

    public int CardCount()
    {
        return _cardDeck.Count;
    }

    public void AddNewCard(CombatCard card)
    {
        playerCards.Add(card);
    }

    public void ReturnCard(Card card)
    {
        _cardDeck.Enqueue(card.CombatDTO.CardPrefab);
        Destroy(card.CardVisual.gameObject);
        Destroy(card.transform.parent.gameObject);
        SoundManager.Instance.Play(returnCardSound);
    }

    public IList<CombatCard> GetAllCards()
    {
        var cards = new List<CombatCard>(playerCards);
        cards.Sort((a, b) =>
        {
            int res = a.ManaCost.CompareTo(b.ManaCost);
            if (res != 0)
                return res;
            return a.Name.CompareTo(b.Name);
        });
        return cards;
    }

    public int AllCardsCount() => playerCards.Count;
}
