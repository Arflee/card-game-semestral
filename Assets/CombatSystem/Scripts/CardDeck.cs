using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardDeck : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int maxCardsInHand = 7;
    [SerializeField] private CombatCard[] playerCards;

    private Queue<CombatCard> _cardDeck;
    private CardHolder _cardHolder;

    public int MaxCardsInHand => maxCardsInHand;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnNewSceneAdded;
        Deck = new HashSet<int>(Enumerable.Range(0, playerCards.Count));
    }

    private void OnNewSceneAdded(Scene current, Scene next)
    {
        ApplyDeck();
    }

    private void OnNewSceneAdded(Scene arg0, Scene arg1)
    {
        _cardDeck = new(Utility.Shuffle(playerCards));
        _cardHolder = FindObjectOfType<CardHolder>();
    }

    public CombatCard TakeCard(CardOwner owner)
    {
        if (_cardDeck.Count == 0)
        {
            return null;
        }

        var takenCard = _cardDeck.Dequeue();
        _cardHolder.AddCard(takenCard, owner);

        return takenCard;
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

    public void AddNewCard(CombatCard newCard)
    {
        playerCards.Add(newCard);
    }


    public int AllCardsCount() => playerCards.Count;
}
