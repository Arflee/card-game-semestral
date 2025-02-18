using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class CardVisual : MonoBehaviour
{
    private bool _initialize = false;

    [Header("Card")]
    private Card parentCard;
    private Transform cardTransform;
    private Vector3 rotationDelta;
    private int savedIndex;
    private Vector3 movementDelta;
    private Canvas _globalCanvas;
    private Canvas _localCanvas;

    [Header("References")]
    public Transform visualShadow;
    private float shadowOffset = 20;
    private Vector2 shadowDistance;
    [SerializeField] private Transform shakeParent;
    [SerializeField] private Transform tiltParent;

    [Header("Card info parameters")]
    [SerializeField] private Image cardTemplate;
    [SerializeField] private Image cardImage;
    [SerializeField] private Sprite describtionTemplate;
    [SerializeField] private Sprite textlessTemplate;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardDescription;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text manaText;

    [Header("Follow Parameters")]
    [SerializeField] private float followSpeed = 30;

    [Header("Rotation Parameters")]
    [SerializeField] private float rotationAmount = 20;
    [SerializeField] private float rotationSpeed = 20;
    [SerializeField] private float autoTiltAmount = 30;
    [SerializeField] private float manualTiltAmount = 20;
    [SerializeField] private float tiltSpeed = 20;

    [Header("Scale Parameters")]
    [SerializeField] private bool scaleAnimations = true;
    [SerializeField] private float scaleOnHover = 1.15f;
    [SerializeField] private float scaleOnSelect = 1.25f;
    [SerializeField] private float scaleTransition = .15f;
    [SerializeField] private Ease scaleEase = Ease.OutBack;

    [Header("Select Parameters")]
    [SerializeField] private float selectPunchAmount = 20;

    [Header("Hober Parameters")]
    [SerializeField] private float hoverPunchAngle = 5;
    [SerializeField] private float hoverTransition = .15f;

    [Header("Swap Parameters")]
    [SerializeField] private bool swapAnimations = true;
    [SerializeField] private float swapRotationAngle = 30;
    [SerializeField] private float swapTransition = .15f;
    [SerializeField] private int swapVibrato = 5;

    [Header("Curve")]
    [SerializeField] private CurveParameters curve;

    [Header("Effects")]
    [SerializeField] private Color effectColor;
    private Color normalColor;

    private float curveYOffset;
    private float curveRotationOffset;

    public Canvas LocalCanvas => _localCanvas;

    private void Start()
    {
        shadowDistance = visualShadow.localPosition;
    }

    public void Initialize(Card target, CombatCardDTO combatProperties)
    {
        //Declarations
        normalColor = cardTemplate.color;
        parentCard = target;
        cardTransform = target.transform;
        _globalCanvas = GetComponentInParent<Canvas>();
        _localCanvas = GetComponent<Canvas>();
        _localCanvas.worldCamera = Camera.main;

        //Event Listening
        parentCard.PointerEnterEvent.AddListener(PointerEnter);
        parentCard.PointerExitEvent.AddListener(PointerExit);
        parentCard.BeginDragEvent.AddListener(BeginDrag);
        parentCard.EndDragEvent.AddListener(EndDrag);
        parentCard.PointerDownEvent.AddListener(PointerDown);
        parentCard.PointerUpEvent.AddListener(PointerUp);
        parentCard.SelectEvent.AddListener(Select);

        Reinitialize(combatProperties);

        parentCard.OnTakeDamageEvent += UpdateTextOnTakeDamage;

        //Initialization
        _initialize = true;

    }

    public void Reinitialize(CombatCardDTO combatProperties)
    {
        cardName.text = combatProperties.Name;
        cardDescription.text = combatProperties.Description;
        cardTemplate.sprite = string.IsNullOrEmpty(combatProperties.Description) ? textlessTemplate : describtionTemplate;
        if (combatProperties.Sprite != null)
            cardImage.sprite = combatProperties.Sprite;
        healthText.text = combatProperties.Health.ToString();
        damageText.text = combatProperties.Damage.ToString();
        manaText.text = combatProperties.ManaCost.ToString();
    }

    private void UpdateTextOnTakeDamage(Card card)
    {
        healthText.text = card.CombatDTO.Health.ToString();
        damageText.text = card.CombatDTO.Damage.ToString();
    }

    public void UpdateIndex()
    {
        transform.SetSiblingIndex(parentCard.transform.parent.GetSiblingIndex());
    }

    void Update()
    {
        if (!_initialize || parentCard == null) return;

        HandPositioning();
        SmoothFollow();
        FollowRotation();
        CardTilt();
    }

    private void HandPositioning()
    {
        curveYOffset = (curve.positioning.Evaluate(parentCard.NormalizedPosition()) * curve.positioningInfluence) * parentCard.SiblingAmount();
        curveYOffset = parentCard.SiblingAmount() < 5 ? 0 : curveYOffset;
        curveRotationOffset = curve.rotation.Evaluate(parentCard.NormalizedPosition());
    }

    private void SmoothFollow()
    {
        Vector3 verticalOffset = (Vector3.up * (parentCard.IsDragging ? 0 : curveYOffset));
        transform.position = Vector3.Lerp(transform.position, cardTransform.position + verticalOffset, followSpeed * Time.deltaTime);
    }

    private void FollowRotation()
    {
        Vector3 movement = (transform.position - cardTransform.position);
        movementDelta = Vector3.Lerp(movementDelta, movement, 25 * Time.deltaTime);
        Vector3 movementRotation = (parentCard.IsDragging ? movementDelta : movement) * rotationAmount;
        rotationDelta = Vector3.Lerp(rotationDelta, movementRotation, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(rotationDelta.x, -60, 60));
    }

    private void CardTilt()
    {
        savedIndex = parentCard.IsDragging ? savedIndex : parentCard.ParentIndex();
        float sine = Mathf.Sin(Time.time + savedIndex) * (parentCard.IsHovering ? .2f : 1);
        float cosine = Mathf.Cos(Time.time + savedIndex) * (parentCard.IsHovering ? .2f : 1);

        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float tiltX = parentCard.IsHovering ? ((offset.y * -1) * manualTiltAmount) : 0;
        float tiltY = parentCard.IsHovering ? ((offset.x) * manualTiltAmount) : 0;
        float tiltZ = parentCard.IsDragging ? tiltParent.eulerAngles.z : (curveRotationOffset * (curve.rotationInfluence * parentCard.SiblingAmount()));
        
        if (float.IsNaN(tiltZ))
            tiltZ = 0;

        float lerpX = Mathf.LerpAngle(tiltParent.eulerAngles.x, tiltX + (sine * autoTiltAmount), tiltSpeed * Time.deltaTime);
        float lerpY = Mathf.LerpAngle(tiltParent.eulerAngles.y, tiltY + (cosine * autoTiltAmount), tiltSpeed * Time.deltaTime);
        float lerpZ = Mathf.LerpAngle(tiltParent.eulerAngles.z, tiltZ, tiltSpeed / 2 * Time.deltaTime);

        tiltParent.eulerAngles = new Vector3(lerpX, lerpY, lerpZ);
    }

    private void Select(Card card, bool state)
    {
        if (card.SelectionOffset == 0)
            return;

        DOTween.Kill(2, true);
        float dir = state ? 1 : 0;
        shakeParent.DOPunchPosition(dir * selectPunchAmount * shakeParent.up, scaleTransition, 10, 1);
        shakeParent.DOPunchRotation(Vector3.forward * (hoverPunchAngle / 2), hoverTransition, 20, 1).SetId(2);

        if (scaleAnimations)
            transform.DOScale(scaleOnHover, scaleTransition).SetEase(scaleEase);
    }

    public void Swap(float dir = 1)
    {
        if (!swapAnimations)
            return;

        DOTween.Kill(2, true);
        shakeParent.DOPunchRotation((Vector3.forward * swapRotationAngle) * dir, swapTransition, swapVibrato, 1).SetId(3);
    }

    private void BeginDrag(Card card)
    {
        if (scaleAnimations)
            transform.DOScale(scaleOnSelect, scaleTransition).SetEase(scaleEase);

        _globalCanvas.overrideSorting = true;
    }

    private void EndDrag(Card card)
    {
        _globalCanvas.overrideSorting = false;

        //transform.DOScale(1, scaleTransition).SetEase(scaleEase);
    }

    private void PointerEnter(Card card)
    {
        if (scaleAnimations)
            transform.DOScale(scaleOnHover, scaleTransition).SetEase(scaleEase);

        DOTween.Kill(2, true);
        shakeParent.DOPunchRotation(Vector3.forward * hoverPunchAngle, hoverTransition, 20, 1).SetId(2);
    }

    private void PointerExit(Card card)
    {
        if (!parentCard.WasDragged)
            transform.DOScale(1, scaleTransition).SetEase(scaleEase);
    }

    private void PointerUp(Card card, bool longPress)
    {
        if (scaleAnimations)
            transform.DOScale(longPress ? scaleOnHover : scaleOnSelect, scaleTransition).SetEase(scaleEase);
        _globalCanvas.overrideSorting = false;

        visualShadow.localPosition = shadowDistance;
    }

    private void PointerDown(Card card)
    {
        if (scaleAnimations)
            transform.DOScale(scaleOnSelect, scaleTransition).SetEase(scaleEase);

        visualShadow.localPosition += (-Vector3.up * shadowOffset);
    }

    public void PutOnBackgrond()
    {
        _localCanvas.overrideSorting = true;
        _localCanvas.sortingOrder = 1;
        _localCanvas.sortingLayerName = "Background";
    }

    public void ShowEffect()
    {
        cardTemplate.color = effectColor;
    }

    public void HideEffect()
    {
        cardTemplate.color = normalColor;
    }
}
