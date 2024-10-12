using UnityEngine;

public class DragNDropTable : MonoBehaviour
{
    [SerializeField] private CardHolder cardHolder;
    [SerializeField] private RectTransform[] snapPoints;

    private void Start()
    {
        cardHolder.RegisterNewOnDragEndAction(OnCardDragEnd);
    }

    private void OnCardDragEnd(Card card)
    {
        foreach (var point in snapPoints)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(point, (Vector2)card.transform.position) &&
                point.CompareTag("Slot"))
            {
                cardHolder.UseCardFromHand(card);

                card.transform.SetParent(point.transform);
            }
        }
    }
}
