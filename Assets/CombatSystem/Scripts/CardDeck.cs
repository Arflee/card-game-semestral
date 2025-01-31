using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardDeck : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int initialCardsInHand = 2;
    [SerializeField, Range(1, 10)] private int maxCardsInHand = 7;
    [SerializeField] private CombatCard[] playerCards;
    [SerializeField] private LifeCrystalParameters crystals;

    public IList<CombatCard> AllCards => playerCards;
    public HashSet<int> Deck { get; private set; } = new HashSet<int>();

    private Queue<CombatCard> _cardDeck;
    private CardHolder _cardHolder;

    public LifeCrystalParameters Crystals => crystals;
    public int MaxCrystals => crystals.CrystalAmount;
    public int InitialCardsInHand => initialCardsInHand;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnNewSceneAdded;
    }

    private void OnNewSceneAdded(Scene arg0, Scene arg1)
    {
        ApplyDeck();
    }

    public void ApplyDeck()
    {
        List<CombatCard> cards = new List<CombatCard>();
        foreach (var cardId in Deck)
            cards.Add(playerCards[cardId]);

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

    public void ReturnCard(Card card)
    {
        _cardDeck.Enqueue(card.CombatDTO.CardPrefab);
        Destroy(card.CardVisual.gameObject);
        Destroy(card.transform.parent.gameObject);
    }
}
