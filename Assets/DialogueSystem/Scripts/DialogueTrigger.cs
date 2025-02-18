using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSequence _dialogueSequence;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private SlidingDialogueText dialogueBubblePrefab;
#if UNITY_EDITOR
    [SerializeField] private bool skipInEditor = false;
#endif
    [SerializeField] private UnityEvent[] choiceEvents;
    [SerializeField] private UnityEvent onDialogueEndEvent;

    public event Action OnDialogueEnd;

    private StandardControls _inputActions;
    private SlidingDialogueText _createdBubble;

    private void Start()
    {
        _inputActions = PlayerMovement.Controls;
        _inputActions.Player.Interact.Enable();
    }

    public void EnableDialogue(DialogueSequence seq = null)
    {
#if UNITY_EDITOR
        if (skipInEditor && OnDialogueEnd != null)
        {
            OnDialogueEnd();
            return;
        }
#endif
        if (seq == null)
            seq = _dialogueSequence;

        _inputActions.Player.Move.Disable();

        _createdBubble = Instantiate(dialogueBubblePrefab, dialogueCanvas.transform);
        _createdBubble.Init(seq);

        _createdBubble.OnDialogueSequenceEnd += OnDialogueSequenceEnd;
        _createdBubble.OnChoiceMade += OnChoiceMade;
        _createdBubble.OnDialogueSequenceEnd += OnDialogueEnd;
        _createdBubble.gameObject.transform.localScale = new Vector2(0.1f, 0.1f);

        //this causes error
        _createdBubble.gameObject.transform.DOScale(1f, 0.5f).SetEase(Ease.OutCubic);
    }

    private void OnChoiceMade(int choiceIndex)
    {
        if (choiceEvents.Length > 0)
        {
            choiceEvents[choiceIndex]?.Invoke();
        }
    }

    private void OnDialogueSequenceEnd()
    {
        _createdBubble.gameObject.transform.DOScale(0f, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            _inputActions.Player.Move.Enable();

            _createdBubble.OnDialogueSequenceEnd -= OnDialogueSequenceEnd;
            _createdBubble.OnChoiceMade -= OnChoiceMade;
            _createdBubble.OnDialogueSequenceEnd -= OnDialogueEnd;

            Destroy(_createdBubble.gameObject);
        });

        if (onDialogueEndEvent != null)
            onDialogueEndEvent.Invoke();
    }
}
