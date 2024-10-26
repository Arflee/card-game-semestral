using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Splines;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Selectable))]
public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private float moveSpeedLimit = 50f;
    [SerializeField] private float selectionOffset = 50f;

    private Canvas _canvas;
    private Image _imageComponent;
    private Selectable _selectableComponent;

    private bool _isDragging;
    private bool _wasDragged;
    private bool _isHovering;

    private Vector3 _offset;

    private Camera _mainCamera;

    private float _pointerUpTime;
    private float _pointerDownTime;

    [HideInInspector] public UnityEvent<Card> PointerEnterEvent;
    [HideInInspector] public UnityEvent<Card> PointerExitEvent;
    [HideInInspector] public UnityEvent<Card, bool> PointerUpEvent;
    [HideInInspector] public UnityEvent<Card> PointerDownEvent;
    [HideInInspector] public UnityEvent<Card> BeginDragEvent;
    [HideInInspector] public UnityEvent<Card> EndDragEvent;
    [HideInInspector] public UnityEvent<Card, bool> SelectEvent;

    public bool IsSelected { get; private set; }
    public bool IsHovering => _isHovering;
    public bool IsDragging => _isDragging;
    public bool WasDragged => _wasDragged;

    [SerializeField] private CardVisual cardVisualPrefab;
    public CardVisual CardVisual { get; private set; }
    public CombatCardDTO CombatDTO { get; private set; }

    public float SelectionOffset => selectionOffset;
    public event Action<Card> OnDieEvent;
    public event Action<Card> OnTakeDamageEvent;

    private void OnEnable()
    {
        _canvas = GetComponentInParent<Canvas>();
        _imageComponent = GetComponent<Image>();
        _mainCamera = Camera.main;
        _selectableComponent = GetComponent<Selectable>();
    }

    public void Initialize(CombatCard combatProperties)
    {
        CombatDTO = new(combatProperties.Name, combatProperties.Description, combatProperties.Health, combatProperties.Damage);
        CardVisual = Instantiate(cardVisualPrefab, _canvas.transform);
        CardVisual.Initialize(this, CombatDTO);
    }

    private void Update()
    {
        ClampPosition();

        if (_isDragging)
        {
            Vector2 targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - _offset;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 velocity = direction * Mathf.Min(moveSpeedLimit, Vector2.Distance(transform.position, targetPosition) / Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }
    }

    private void ClampPosition()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x, screenBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y, screenBounds.y);
        transform.position = new Vector3(clampedPosition.x, clampedPosition.y, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragEvent.Invoke(this);

        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _offset = mousePosition - (Vector2)transform.position;
        _isDragging = true;
        _canvas.GetComponent<GraphicRaycaster>().enabled = false;
        _imageComponent.raycastTarget = false;

        _wasDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {;}

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragEvent.Invoke(this);

        _isDragging = false;
        _canvas.GetComponent<GraphicRaycaster>().enabled = true;
        _imageComponent.raycastTarget = true;

        StartCoroutine(FrameWait());

        IEnumerator FrameWait()
        {
            yield return new WaitForEndOfFrame();
            _wasDragged = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnterEvent.Invoke(this);
        _isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExitEvent.Invoke(this);
        _isHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        _pointerDownTime = Time.time;
        PointerDownEvent.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        _pointerUpTime = Time.time;

        PointerUpEvent.Invoke(this, _pointerUpTime - _pointerDownTime > .2f);

        if (_pointerUpTime - _pointerDownTime > .2f)
            return;

        if (_wasDragged)
            return;

        IsSelected = !IsSelected;
        SelectEvent.Invoke(this, IsSelected);

        if (IsSelected)
            transform.localPosition += (CardVisual.transform.up * selectionOffset);
        else
            transform.localPosition = Vector3.zero;
    }

    public void Deselect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            if (IsSelected)
                transform.localPosition += (CardVisual.transform.up * 50);
            else
                transform.localPosition = Vector3.zero;
        }
    }
    public int SiblingAmount()
    {
        return transform.parent.CompareTag("Slot") ? transform.parent.parent.childCount - 1 : 0;
    }

    public int ParentIndex()
    {
        return transform.parent.CompareTag("Slot") ? transform.parent.GetSiblingIndex() : 0;
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public float NormalizedPosition()
    {
        return transform.parent.CompareTag("Slot") ? Remap((float)ParentIndex(), 0, (float)(transform.parent.parent.childCount - 1), 0, 1) : 0;
    }

    public void DisableCard()
    {
        PointerEnterEvent.RemoveAllListeners();
        PointerExitEvent.RemoveAllListeners();
        PointerUpEvent.RemoveAllListeners();
        PointerDownEvent.RemoveAllListeners();
        BeginDragEvent.RemoveAllListeners();
        EndDragEvent.RemoveAllListeners();
        SelectEvent.RemoveAllListeners();

        _selectableComponent.enabled = false;
    }

    public void TakeDamageFrom(Card source)
    {
        CombatDTO.Health -= source.CombatDTO.Damage;
        OnTakeDamageEvent?.Invoke(this);

        if (!CombatDTO.IsAlive)
        {
            OnDieEvent?.Invoke(this);
        }
    }
}
