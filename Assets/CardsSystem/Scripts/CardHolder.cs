using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CardHolder : MonoBehaviour
{
    [SerializeField] private CardDeck playerDeck;
    [SerializeField] private GameObject slotPrefab;
    private RectTransform _rect;

    [Header("Spawn Settings")]
    private List<Card> _cards;
    private Card _selectedCard;
    private Card _hoveredCard;

    private bool _isCrossing = false;
    [SerializeField] private bool tweenCardReturn = true;

    private List<UnityAction<Card>> _externalOnDragEndActions = new();

    private void Start()
    {
        for (int i = 0; i < playerDeck.MaxCardsInHand; i++)
        {
            Instantiate(slotPrefab, transform);
        }

        _rect = (RectTransform)transform;
        _cards = GetComponentsInChildren<Card>().ToList();
        int cardCount = 0;


        foreach (Card card in _cards)
        {
            var playerCombatCard = playerDeck.TakeCard();
            card.Initialize(playerCombatCard);

            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            foreach (var action in _externalOnDragEndActions)
            {
                card.EndDragEvent.AddListener(action);
            }
            card.name = cardCount.ToString();
            cardCount++;
        }
    }

    private void BeginDrag(Card card)
    {
        _selectedCard = card;
    }

    private void EndDrag(Card card)
    {
        if (_selectedCard == null)
            return;

        _selectedCard.transform.DOLocalMove(
            _selectedCard.IsSelected ? new Vector3(0, _selectedCard.SelectionOffset, 0) : Vector3.zero, tweenCardReturn ? .15f : 0)
            .SetEase(Ease.OutBack);

        _rect.sizeDelta += Vector2.right;
        _rect.sizeDelta -= Vector2.right;

        _selectedCard = null;
    }

    private void CardPointerEnter(Card card)
    {
        _hoveredCard = card;
    }

    private void CardPointerExit(Card card)
    {
        _hoveredCard = null;
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

        if (_selectedCard == null)
            return;

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
            card.CardVisual.UpdateIndex(transform.childCount);
        }
    }

    public void RegisterNewOnDragEndAction(UnityAction<Card> action)
    {
        _externalOnDragEndActions.Add(action);
    }

    public void UseCardFromHand(Card card)
    {
        var cardSlot = card.transform.parent;

        card.DisableCard();
        _cards.Remove(card);
        card.CardVisual.PutOnBackgrond();
        Destroy(cardSlot.gameObject);
    }
}
