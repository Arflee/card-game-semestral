using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SlidingDialogueText : MonoBehaviour
{
    [SerializeField]
    private DialogueSequence _dialogueSequence;

    [SerializeField]
    private ScrollRect _scrollRect;

    [SerializeField]
    private TextMeshProUGUI _slidingText;

    private StandardControls inputActions;
    private bool _isTyping;
    private bool _isSkipped;
    private int dialogueIndex = 0;

    private void Start()
    {
        inputActions = new StandardControls();
        inputActions.Player.Interact.Enable();

        inputActions.Player.Interact.performed += OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        if (dialogueIndex == _dialogueSequence.sequence.Length) return;

        if (_isTyping)
        {
            _isSkipped = true;
        }
        else
        {
            StartCoroutine(TypeSymbols(_dialogueSequence.sequence[dialogueIndex]));
        }
    }

    private IEnumerator TypeSymbols(string dialogue)
    {
        _isTyping = true;
        _slidingText.text = string.Empty;

        for (int i = 0; i < dialogue.Length; i++)
        {
            if (_isSkipped)
            {
                _slidingText.text = dialogue;
                _scrollRect.verticalNormalizedPosition = 0;
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_scrollRect.transform);
                _isSkipped = false;
                break;
            }

            _slidingText.text += dialogue[i];
            _scrollRect.verticalNormalizedPosition = 0;
            yield return new WaitForSeconds(_dialogueSequence.delayPerSymbol);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_scrollRect.transform);
        }

        _isTyping = false;
        dialogueIndex++;
    }
}
