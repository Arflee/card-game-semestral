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
}
