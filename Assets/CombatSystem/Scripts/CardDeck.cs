using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardDeck : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int maxCardsInHand = 7;
    [SerializeField] private CombatCard[] playerCards;
    [SerializeField] private LifeCrystalParameters crystals;

    private Queue<CombatCard> _cardDeck;
    private CardHolder _cardHolder;
    private static CardDeck _instance;

    public int MaxCrystals => crystals.CrystalAmount;
    public int MaxCardsInHand => maxCardsInHand;

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
    }

    private void OnNewSceneAdded(Scene current, Scene next)
    {
        _cardDeck = new(Utility.Shuffle(playerCards));
        _cardHolder = FindObjectOfType<CardHolder>();

        if (next.name.ToLower().Contains("fight"))
        {
            GameObject.FindWithTag("PlayerCrystals").GetComponent<LifeCrystalPanel>().Initialize(crystals);
        }
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
}
