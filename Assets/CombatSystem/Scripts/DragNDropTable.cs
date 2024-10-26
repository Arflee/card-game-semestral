using System;
using UnityEngine;

public class DragNDropTable : MonoBehaviour
{
    [SerializeField] private CardHolder cardHolder;
    [SerializeField] private RectTransform playerCardsPlayer;

    public event Action<Card> OnTableSlotSnapped;

    private void Start()
    {
        cardHolder.RegisterNewOnDragEndAction(OnCardDragEnd);
    }

    private void OnCardDragEnd(Card card)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)playerCardsPlayer.transform, (Vector2)card.transform.position) &&
            playerCardsPlayer.CompareTag("Slot"))
        {
            cardHolder.UseCardFromHand(card);
            OnTableSlotSnapped?.Invoke(card);
            card.transform.SetParent(playerCardsPlayer.transform);
        }
    }
}
