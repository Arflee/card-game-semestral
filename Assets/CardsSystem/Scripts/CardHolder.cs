using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    private RectTransform _rect;

    [Header("Spawn Settings")]
    private List<Card> _cards = new ();
    private Card _selectedCard;

    private bool _isCrossing = false;
    [SerializeField] private bool tweenCardReturn = true;
    [SerializeField] private Transform startingPosition;

    [Header("Drag Visualization")]
    [SerializeField] private Image dragArea;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color selectedColor;
    private Color normalColor;

    private List<UnityAction<Card>> _externalOnDragEndActions = new();

    public List<Card> CardsInHand => _cards;

    private void Start()
    {
        _rect = (RectTransform)transform;
        normalColor = dragArea.color;
    }

    private void BeginDrag(Card card)
    {
        _selectedCard = card;
    }

    private void EndDrag(Card card)
    {
        dragArea.color = normalColor;

        if (_selectedCard == null)
            return;

        _selectedCard.transform.DOLocalMove(
            _selectedCard.IsSelected ? new Vector3(0, _selectedCard.SelectionOffset, 0) : Vector3.zero, tweenCardReturn ? .15f : 0)
            .SetEase(Ease.OutBack);

        _rect.sizeDelta += Vector2.right;
        _rect.sizeDelta -= Vector2.right;

        _selectedCard = null;
    }

    public Vector2? DraggedCardPosition()
    {
        if (_selectedCard == null)
            return null;
        return _selectedCard.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            foreach (Card card in _cards)
            {
                card.Deselect();
            }
        }

        if (_selectedCard == null || _selectedCard.OnBoard)
            return;

        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)dragArea.transform, (Vector2)_selectedCard.transform.position))
            dragArea.color = selectedColor;
        else
            dragArea.color = activeColor;

        if (_isCrossing)
            return;

        for (int i = 0; i < _cards.Count; i++)
        {
            if (_selectedCard.transform.position.x > _cards[i].transform.position.x)
            {
                if (_selectedCard.ParentIndex() < _cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }

            if (_selectedCard.transform.position.x < _cards[i].transform.position.x)
            {
                if (_selectedCard.ParentIndex() > _cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }
        }
    }

    private void Swap(int index)
    {
        _isCrossing = true;

        Transform focusedParent = _selectedCard.transform.parent;
        Transform crossedParent = _cards[index].transform.parent;

        _cards[index].transform.SetParent(focusedParent);
        _cards[index].transform.localPosition = _cards[index].IsSelected ? new Vector3(0, _cards[index].SelectionOffset, 0) : Vector3.zero;
        _selectedCard.transform.SetParent(crossedParent);

        _isCrossing = false;

        if (_cards[index].CardVisual == null)
            return;

        bool swapIsRight = _cards[index].ParentIndex() > _selectedCard.ParentIndex();
        _cards[index].CardVisual.Swap(swapIsRight ? -1 : 1);

        //Updated Visual Indexes
        foreach (Card card in _cards)
        {
            card.CardVisual.UpdateIndex();
        }
    }

    public void RegisterNewOnDragEndAction(UnityAction<Card> action)
    {
        _externalOnDragEndActions.Add(action);
    }

    public void UseCardFromHand(Card card)
    {
        card.PlaceOnBoard();
        _cards.Remove(card);
        card.CardVisual.PutOnBackgrond();
    }

    public Card CreateTempCard(CombatCard combatCard, CardOwner owner, Transform parent)
    {
        var createdSlot = Instantiate(slotPrefab, parent);
        var createdCard = createdSlot.GetComponentInChildren<Card>();

        createdCard.Initialize(combatCard, owner, startingPosition.position);
        createdCard.DisableCard();
        return createdCard;
    }

    public void AddCard(CombatCard combatCard, CardOwner owner)
    {
        var createdSlot = Instantiate(slotPrefab, transform);
        var createdCard = createdSlot.GetComponentInChildren<Card>();

        createdCard.Initialize(combatCard, owner, startingPosition.position);

        createdCard.BeginDragEvent.AddListener(BeginDrag);
        createdCard.EndDragEvent.AddListener(EndDrag);

        foreach (var action in _externalOnDragEndActions)
        {
            createdCard.EndDragEvent.AddListener(action);
        }

        createdCard.name = combatCard.Name;

        _cards.Add(createdCard);
    }
}
