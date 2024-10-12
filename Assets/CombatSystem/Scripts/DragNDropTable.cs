using UnityEngine;

public class DragNDropTable : MonoBehaviour
{
    [SerializeField] private CardHolder cardHolder;
    [SerializeField] private CombatSlot[] snapPoints;

    private void Start()
    {
        cardHolder.RegisterNewOnDragEndAction(OnCardDragEnd);
    }

    private void OnCardDragEnd(Card card)
    {
        foreach (CombatSlot point in snapPoints)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)point.transform, (Vector2)card.transform.position) &&
                point.CompareTag("Slot"))
            {
                cardHolder.UseCardFromHand(card);
                point.PutCardInSlot(card.CombatCard);
                card.transform.SetParent(point.transform);
            }
        }
    }
}
