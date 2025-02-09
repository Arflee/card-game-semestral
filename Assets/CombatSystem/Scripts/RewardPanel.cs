using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button deckEditButton;
    [SerializeField] private Button crystalButton;
    [SerializeField] private DeckEditorManger manger;

    public static RewardPanel Instance { get; private set; }
    private List<Card> spawnedCards = new List<Card>();
    private List<GameObject> crystals = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void AddCallback(UnityAction onContinue)
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(onContinue);

        crystalButton.onClick.RemoveAllListeners();
        crystalButton.onClick.AddListener(onContinue);

        deckEditButton.onClick.RemoveAllListeners();
        deckEditButton.onClick.AddListener(() => manger.Open(onContinue));
    }

    public void SetRewardCard(CombatCard[] combatCards, string text)
    {
        var playersDeck = FindObjectOfType<CardDeck>();
        foreach (var combatCard in combatCards)
        {
            playersDeck.AddNewCard(combatCard);

            var cardSlot = Instantiate(cardSlotPrefab, panel.transform);
            var card = cardSlot.GetComponentInChildren<Card>();
            card.Initialize(combatCard, null, panel.transform.position);
            card.DisableCard();

            card.CardVisual.LocalCanvas.overrideSorting = true;
            card.CardVisual.LocalCanvas.sortingLayerName = "Foreground";
            card.CardVisual.LocalCanvas.sortingOrder = 1;
            spawnedCards.Add(card);
        }
        continueButton.gameObject.SetActive(true);
        deckEditButton.gameObject.SetActive(true);
        crystalButton.gameObject.SetActive(false);

        titleText.text = text;
    }

    public void SetRewardCrystal(string text)
    {
        var playersDeck = FindObjectOfType<CardDeck>();
        playersDeck.AddCrystal();
        var go = Instantiate(crystalPrefab, panel.transform);
        crystals.Add(go);

        continueButton.gameObject.SetActive(false);
        deckEditButton.gameObject.SetActive(false);
        crystalButton.gameObject.SetActive(true);

        titleText.text = text;
    }

    public void ClearBoard()
    {
        foreach (var card in spawnedCards)
        {
            Destroy(card.CardVisual.gameObject);
            Destroy(card.transform.parent.gameObject);
        }
        spawnedCards.Clear();

        foreach (var card in crystals)
        {
            Destroy(card);
        }
        crystals.Clear();
        gameObject.SetActive(false);
    }
}
