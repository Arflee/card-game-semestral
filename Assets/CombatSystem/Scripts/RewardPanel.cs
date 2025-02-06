using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button deckEditButton;
    [SerializeField] private DeckEditorManger manger;

    public static RewardPanel Instance { get; private set; }
    public List<Card> spawnedCards = new List<Card>();

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void AddCallback(UnityAction onContinue)
    {
        continueButton.onClick.AddListener(onContinue);
        deckEditButton.onClick.AddListener(() => manger.Open(onContinue));
    }

    public void SetRewardCard(CombatCard[] combatCards)
    {
        foreach (var combatCard in combatCards)
        {
            var playersDeck = FindObjectOfType<CardDeck>();
            var newCrystal = FindObjectOfType<AddCrystal>();

            if (newCrystal != null)
                playersDeck.SetCrystals(newCrystal.NewCrystal);

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
    }

    public void ClearBoard()
    {
        foreach (var card in spawnedCards)
        {
            Destroy(card.CardVisual.gameObject);
            Destroy(card.transform.parent.gameObject);
        }
        spawnedCards.Clear();
        gameObject.SetActive(false);
    }
}
