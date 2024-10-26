using System;
using UnityEngine;

public class DragNDropTable : MonoBehaviour
{
    [SerializeField] private CardHolder cardHolder;
    [SerializeField] private RectTransform playedCardsPlayer;

    public event Action<Card> OnTableSlotSnapped;

    private void Start()
    {
        cardHolder.RegisterNewOnDragEndAction(OnCardDragEnd);
    }

    private void OnCardDragEnd(Card card)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)playedCardsPlayer.transform, (Vector2)card.transform.position) &&
            playedCardsPlayer.CompareTag("Slot"))
        {
            cardHolder.UseCardFromHand(card);
            OnTableSlotSnapped?.Invoke(card);
            card.transform.parent.SetParent(playedCardsPlayer.transform);
        }
    }
}
