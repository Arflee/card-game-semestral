using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckVisual : MonoBehaviour
{
    [SerializeField] private CombatStateMachine combatManager;
    [SerializeField] private TextMeshProUGUI cardCountTMP;
    [SerializeField] private CardHolder cardHolder;
    [SerializeField] private Image image;

    [Header("Shadow")]
    [SerializeField] private Shadow shadow;
    [SerializeField] private float maxShadow = 10;
    [SerializeField] private int maxCardsForShadow = 20;

    [Header("Full Deck")]
    [SerializeField] private Color deckColor;
    [SerializeField] private Color deckTextColor;
    [SerializeField] private Color dragActive;
    [SerializeField] private Color dragSelected;

    [Header("Empty Deck")]
    [SerializeField] private Color emptyColor;
    [SerializeField] private Color emptyTextColor;
    [SerializeField] private Color emptyDragActive;
    [SerializeField] private Color emptyDragSelected;

    private void Start()
    {
        cardHolder.RegisterNewOnDragEndAction(OnCardDragEnd);
    }

    private void Update()
    {
        int cardCount = combatManager.PlayerDeck.CardCount();

        if (cardCount == 0)
        {
            image.color = emptyColor;
            cardCountTMP.text = "";
            cardCountTMP.color = emptyTextColor;
            shadow.enabled = false;
        }
        else
        {
            image.color = deckColor;
            cardCountTMP.text = cardCount.ToString();
            cardCountTMP.color = deckTextColor;
            shadow.enabled = true;
            shadow.effectDistance = -Vector2.one * Mathf.Clamp(cardCount * (maxShadow / maxCardsForShadow), 0, maxShadow);
        }

        if (cardHolder.IsDragging())
        {
            cardCountTMP.text = "Vrátit";
            if (cardHolder.IsOverlapping(image.rectTransform))
                image.color = cardCount == 0 ? emptyDragSelected : dragSelected;
            else
                image.color = cardCount == 0 ? emptyDragActive : dragActive;
            return;
        }
    }

    private void OnCardDragEnd(Card card)
    {
        if (cardHolder.IsOverlapping(image.rectTransform))
        {
            combatManager.PlayerCardsOnTable.Remove(card);
            cardHolder.UseCardFromHand(card);
            combatManager.PlayerDeck.ReturnCard(card);
        }
    }
}
