using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class DeckEditorManger : MonoBehaviour
{
    [SerializeField] private Transform cardPanel;
    [SerializeField] private Transform deckPanel;
    [SerializeField] private CardEditView cardPrefab;
    [SerializeField] private CardNameView namePrefab;

    [Header("Min Cards")]
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI confirmText;
    [SerializeField, Min(0)] private int minCards;

    private List<CardEditView> cardViews = new List<CardEditView>();
    private CardDeck cardDeck;
    public int MinCards => minCards;

    private void OnEnable()
    {
        cardDeck = FindObjectOfType<CardDeck>();

        for (int i = cardPanel.childCount - 1; i >= 0; i--)
            Destroy(cardPanel.GetChild(i).gameObject);

        cardViews.Clear();
        var allCards = cardDeck.GetAllCards();
        for (int i = 0; i < allCards.Count; i++)
        {
            var view = Instantiate(cardPrefab, cardPanel);
            view.Initialize(allCards[i], i, this);
            cardViews.Add(view);
        }

        for (int i = deckPanel.childCount - 1; i >= 0; i--)
            Destroy(deckPanel.GetChild(i).gameObject);

        foreach (var cardId in cardDeck.Deck)
        {
            var view = Instantiate(namePrefab, deckPanel);
            view.Initialize(allCards[cardId], this, cardId);
            cardViews[cardId].DisableCard(view.gameObject);
        }
        UpdateConfirmButton();
    }

    public GameObject AddToDeck(CombatCard card, int id)
    {
        var view = Instantiate(namePrefab, deckPanel);
        view.Initialize(card, this, id);
        cardDeck.Deck.Add(id);
        UpdateConfirmButton();
        return view.gameObject;
    }

    public void RemoveFromDeck(int id)
    {
        cardDeck.Deck.Remove(id);
        cardViews[id].EnableCard();
        UpdateConfirmButton();
    }

    private void UpdateConfirmButton()
    {
        if (cardDeck.Deck.Count >= minCards)
            confirmText.text = $"Potvrdit\n({cardDeck.Deck.Count}/{minCards})";
        else
            confirmText.text = $"Doplnit\n({cardDeck.Deck.Count}/{minCards})";
    }

    public void ApplyDeck()
    {
        int cardsLeft = minCards - cardDeck.Deck.Count;
        if (cardsLeft > 0)
        {
            var allCards = cardDeck.GetAllCards();
            List<int> unusedCards = Enumerable.Range(0, allCards.Count).Except(cardDeck.Deck).ToList();
            unusedCards = Utility.Shuffle(unusedCards);

            for (int i = 0; i < cardsLeft; i++)
            {
                int cardId = unusedCards[i];
                cardViews[cardId].DisableCard(AddToDeck(allCards[cardId], cardId));
            }
        }
        else
        {
            cardDeck.ApplyDeck();
            gameObject.SetActive(false);
        }
    }
}
