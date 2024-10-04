using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private SlidingDialogueText dialogueBubblePrefab;

    private StandardControls inputActions;

    private void OnEnable()
    {
        inputActions = new StandardControls();
        inputActions.Player.Interact.Enable();

        inputActions.Player.Interact.performed += OnMouseClick;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        SlidingDialogueText createdBubble = Instantiate(dialogueBubblePrefab, dialogueCanvas.transform);
        createdBubble.gameObject.transform.localScale = new Vector2(0.1f, 0.1f);

        //TODO Make appearing animation
        createdBubble.gameObject.transform.DOScale(1f, 0.5f).SetEase(Ease.OutCubic);

        inputActions.Player.Interact.performed -= OnMouseClick;
    }
}
