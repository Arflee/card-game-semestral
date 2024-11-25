using DG.Tweening;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private DialogueSequence _dialogueSequence;

    [SerializeField]
    private GameObject dialogueCanvas;

    [SerializeField]
    private SlidingDialogueText dialogueBubblePrefab;

    private StandardControls _inputActions;
    private SlidingDialogueText _createdBubble;

    private void OnEnable()
    {
        _inputActions = PlayerMovement.Controls;
        _inputActions.Player.Interact.Enable();
    }

    public void EnableDialogue()
    {
        _inputActions.Player.Move.Disable();

        _createdBubble = Instantiate(dialogueBubblePrefab, dialogueCanvas.transform);
        _createdBubble.Init(_dialogueSequence);

        _createdBubble.OnDialogueSequenceEnd += OnDialogueSequenceEnd;
        _createdBubble.gameObject.transform.localScale = new Vector2(0.1f, 0.1f);

        //TODO Make/Change appearing animation
        _createdBubble.gameObject.transform.DOScale(1f, 0.5f).SetEase(Ease.OutCubic);
    }

    private void OnDialogueSequenceEnd()
    {
        //TODO Make/Change disappearing animation
        _createdBubble.gameObject.transform.DOScale(0f, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            _createdBubble.OnDialogueSequenceEnd -= OnDialogueSequenceEnd;
            _inputActions.Player.Move.Enable();
            Destroy(_createdBubble.gameObject);
        });
    }
}
