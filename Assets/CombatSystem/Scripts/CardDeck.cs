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

    private Queue<CombatCard> _cardDeck;
    private CardHolder _cardHolder;

    public int MaxCrystals => crystals.CrystalAmount;
    public int InitialCardsInHand => initialCardsInHand;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnNewSceneAdded;
    }

    private void OnNewSceneAdded(Scene arg0, Scene arg1)
    {
        _cardDeck = new(Utility.Shuffle(playerCards));
        _cardHolder = FindObjectOfType<CardHolder>();
        GameObject.FindWithTag("PlayerCrystals").GetComponent<LifeCrystalPanel>().Initialize(crystals);
    }

    public CombatCard TakeCard(CardOwner owner)
    {
        if (_cardDeck.Count == 0)
        {
            return null;
        }

        var takenCard = _cardDeck.Dequeue();

        if (_cardHolder.CardsInHand.Count == maxCardsInHand)
        {
            // TODO: Discard card
            return null;
        }

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
}
