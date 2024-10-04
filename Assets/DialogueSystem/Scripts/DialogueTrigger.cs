using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private SlidingDialogueText dialogueBubblePrefab;

    private StandardControls _inputActions;
    private SlidingDialogueText _createdBubble;

    private void OnEnable()
    {
        _inputActions = new StandardControls();
        _inputActions.Player.Interact.Enable();

        _inputActions.Player.Interact.performed += OnMouseClick;
    }

    private void OnDisable()
    {
        _inputActions.Player.Interact.performed -= OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        _createdBubble = Instantiate(dialogueBubblePrefab, dialogueCanvas.transform);
        _createdBubble.OnDialogueSequenceEnd += OnDialogueSequenceEnd;
        _createdBubble.gameObject.transform.localScale = new Vector2(0.1f, 0.1f);

        //TODO Make appearing animation
        _createdBubble.gameObject.transform.DOScale(1f, 0.5f).SetEase(Ease.OutCubic);

        _inputActions.Player.Interact.performed -= OnMouseClick;
    }

    private void OnDialogueSequenceEnd()
    {
        _createdBubble.gameObject.transform.DOScale(0f, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            _createdBubble.OnDialogueSequenceEnd -= OnDialogueSequenceEnd;
            Destroy(_createdBubble.gameObject);
        });
    }
}
