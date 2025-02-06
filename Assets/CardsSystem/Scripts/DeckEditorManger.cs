using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

    private IList<CombatCard> allCards;
    private UnityAction onCompleted;

    public void Open(UnityAction onCompleted)
    {
        this.onCompleted = onCompleted;
        transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        cardDeck = FindObjectOfType<CardDeck>();

        for (int i = cardPanel.childCount - 1; i >= 0; i--)
            Destroy(cardPanel.GetChild(i).gameObject);

        cardViews.Clear();
        allCards = cardDeck.GetAllCards();
        for (int pos = 0; pos < allCards.Count; pos++)
        {
            var view = Instantiate(cardPrefab, cardPanel);
            view.Initialize(allCards[pos], pos, this);
            cardViews.Add(view);
        }

        for (int i = deckPanel.childCount - 1; i >= 0; i--)
            Destroy(deckPanel.GetChild(i).gameObject);

        foreach (var pos in cardDeck.GetDeckOfPos())
        {
            var view = Instantiate(namePrefab, deckPanel);
            view.Initialize(allCards[pos], this, pos);
            cardViews[pos].DisableCard(view.gameObject);
        }
        UpdateConfirmButton();
    }

    public GameObject AddToDeck(int pos)
    {
        var view = Instantiate(namePrefab, deckPanel);
        view.Initialize(allCards[pos], this, pos);
        cardDeck.AddToDeck(pos);
        UpdateConfirmButton();
        return view.gameObject;
    }

    public void RemoveFromDeck(int pos)
    {
        cardDeck.RemoveFromDeck(pos);
        cardViews[pos].EnableCard();
        UpdateConfirmButton();
    }

    private void UpdateConfirmButton()
    {
        if (cardDeck.GetDeckLength() >= minCards)
            confirmText.text = $"Potvrdit\n({cardDeck.GetDeckLength()}/{minCards})";
        else
            confirmText.text = $"Doplnit\n({cardDeck.GetDeckLength()}/{minCards})";
    }

    public void ApplyDeck()
    {
        int cardsLeft = minCards - cardDeck.GetDeckLength();
        if (cardsLeft > 0)
        {
            List<int> unusedPos = Enumerable.Range(0, cardDeck.AllCardsCount()).Except(cardDeck.GetDeckOfPos()).ToList();
            unusedPos = Utility.Shuffle(unusedPos);

            for (int i = 0; i < cardsLeft; i++)
            {
                int pos = unusedPos[i];
                cardViews[pos].DisableCard(AddToDeck(pos));
            }
        }
        else
        {
            cardDeck.ApplyDeck();
            onCompleted?.Invoke();
            onCompleted = null;
            gameObject.SetActive(false);
        }
    }

    public void ClearDeck()
    {
        var toDelete = cardDeck.GetDeckOfPos();
        foreach (var cardId in toDelete)
            RemoveFromDeck(cardId);

        for (int i = deckPanel.childCount - 1; i >= 0; i--)
            Destroy(deckPanel.GetChild(i).gameObject);
    }
}
