using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDropTable : MonoBehaviour
{
    [SerializeField] private CardHolder cardHolder;

    private RectTransform _tableTransform;
    private Rect _boundingBox;

    private void Start()
    {
        _tableTransform = (RectTransform)transform;
        var corners = new Vector3[4];
        _tableTransform.GetWorldCorners(corners);
        var position = corners[0];

        Vector2 size = new Vector2(
            _tableTransform.lossyScale.x * _tableTransform.rect.size.x,
            _tableTransform.lossyScale.y * _tableTransform.rect.size.y);

        _boundingBox = new Rect(position, size);

        cardHolder.RegisterNewOnDragEndAction(OnCardDragEnd);
    }

    private void OnCardDragEnd(Card card)
    {
        if (_boundingBox.Contains(card.transform.position))
        {
            Debug.Log("Dropped on table");
        }
    }
}
