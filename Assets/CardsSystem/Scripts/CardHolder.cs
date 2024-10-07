using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;

    private Card _selectedCard;
    private Card _hoveredCard;
    private RectTransform _rect;

    [Header("Spawn Settings")]
    [SerializeField] private int cardsToSpawn = 7;
    public List<Card> cards;

    [SerializeField] private bool tweenCardReturn = true;
    private bool _isCrossing = false;

    void Start()
    {
        for (int i = 0; i < cardsToSpawn; i++)
        {
            Instantiate(slotPrefab, transform);
        }

        _rect = GetComponent<RectTransform>();
        cards = GetComponentsInChildren<Card>().ToList();

        int cardCount = 0;

        foreach (Card card in cards)
        {
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            card.name = cardCount.ToString();
            cardCount++;
        }

        //StartCoroutine(Frame());

        //IEnumerator Frame()
        //{
        //    yield return new WaitForSecondsRealtime(.1f);
        //    for (int i = 0; i < cards.Count; i++)
        //    {
        //        if (cards[i].cardVisual != null)
        //            cards[i].cardVisual.UpdateIndex(transform.childCount);
        //    }
        //}
    }

    private void BeginDrag(Card card)
    {
        _selectedCard = card;
    }


    void EndDrag(Card card)
    {
        if (_selectedCard == null)
            return;

        _selectedCard.transform.DOLocalMove(_selectedCard.IsSelected ? new Vector3(0, _selectedCard.SelectionOffset, 0) : Vector3.zero, tweenCardReturn ? .15f : 0).SetEase(Ease.OutBack);

        _rect.sizeDelta += Vector2.right;
        _rect.sizeDelta -= Vector2.right;

        _selectedCard = null;
    }

    void CardPointerEnter(Card card)
    {
        _hoveredCard = card;
    }

    void CardPointerExit(Card card)
    {
        _hoveredCard = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (_hoveredCard != null)
            {
                Destroy(_hoveredCard.transform.parent.gameObject);
                cards.Remove(_hoveredCard);

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Card card in cards)
            {
                card.Deselect();
            }
        }

        if (_selectedCard == null)
            return;

        if (_isCrossing)
            return;

        for (int i = 0; i < cards.Count; i++)
        {

            if (_selectedCard.transform.position.x > cards[i].transform.position.x)
            {
                if (_selectedCard.ParentIndex() < cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }

            if (_selectedCard.transform.position.x < cards[i].transform.position.x)
            {
                if (_selectedCard.ParentIndex() > cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }
        }
    }

    void Swap(int index)
    {
        _isCrossing = true;

        Transform focusedParent = _selectedCard.transform.parent;
        Transform crossedParent = cards[index].transform.parent;

        cards[index].transform.SetParent(focusedParent);
        cards[index].transform.localPosition = cards[index].IsSelected ? new Vector3(0, cards[index].SelectionOffset, 0) : Vector3.zero;
        _selectedCard.transform.SetParent(crossedParent);

        _isCrossing = false;

        //if (cards[index].cardVisual == null)
        //    return;

        //bool swapIsRight = cards[index].ParentIndex() > selectedCard.ParentIndex();
        //cards[index].cardVisual.Swap(swapIsRight ? -1 : 1);

        ////Updated Visual Indexes
        //foreach (Card card in cards)
        //{
        //    card.cardVisual.UpdateIndex(transform.childCount);
        //}
    }

}
