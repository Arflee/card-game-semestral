using System;
using UnityEngine;

public class DragNDropTable : MonoBehaviour
{
    [SerializeField] private CardHolder cardHolder;
    [SerializeField] private RectTransform playedCardsPlayer;

    public event Func<Card, bool> OnTableSlotSnapped;

    public Transform PlayerTableSide => playedCardsPlayer;

    private void Start()
    {
        cardHolder.RegisterNewOnDragEndAction(OnCardDragEnd);
    }

    private void OnCardDragEnd(Card card)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)playedCardsPlayer.transform, (Vector2)card.transform.position))
        {
            if (OnTableSlotSnapped?.Invoke(card) ?? false)
            {
                cardHolder.UseCardFromHand(card);
                card.transform.parent.SetParent(playedCardsPlayer.transform);
            }
            else
            {
                Debug.Log("not enough mana");
            }
        }
    }
}
