using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckVisual : MonoBehaviour
{
    [SerializeField] private CombatStateMachine combatManager;
    [SerializeField] private TextMeshProUGUI cardCountTMP;
    [SerializeField] private Shadow shadow;
    [SerializeField] private float maxShadow = 10;
    [SerializeField] private int maxCardsForShadow = 20;

    [Header("Discard")]
    [SerializeField] private Image image;
    [SerializeField] private Color dragActive;
    [SerializeField] private Color dragSelected;
    [SerializeField] private CardHolder cardHolder;

    private Color normalColor;

    private void Start()
    {
        normalColor = image.color;
        cardHolder.RegisterNewOnDragEndAction(OnCardDragEnd);
    }

    private void Update()
    {
        var pos = cardHolder.DraggedCardPosition();
        if (pos != null)
        {
            cardCountTMP.text = "Vrátit";
            if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, (Vector2)pos))
                image.color = dragSelected;
            else
                image.color = dragActive;
            return;
        }
        image.color = normalColor;

        int cardCount = combatManager.PlayerDeck.CardCount();
        if (cardCount == 0)
            gameObject.SetActive(false);

        cardCountTMP.text = cardCount.ToString();
        shadow.effectDistance = -Vector2.one * Mathf.Clamp(cardCount * (maxShadow /  maxCardsForShadow), 0, maxShadow);
    }

    private void OnCardDragEnd(Card card)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, (Vector2)card.transform.position))
        {
            combatManager.PlayerCardsOnTable.Remove(card);
            cardHolder.UseCardFromHand(card);
            combatManager.PlayerDeck.ReturnCard(card);
        }
    }
}
