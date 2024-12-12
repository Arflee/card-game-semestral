using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SlidingDialogueText : MonoBehaviour
{
    [SerializeField] private GameObject _choicePanel;
    [SerializeField] private Button _choiceButton;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private TextMeshProUGUI _speakersName;
    [SerializeField] private TextMeshProUGUI _slidingText;

    private DialogueSequence _dialogueSequence;
    private StandardControls inputActions;
    private bool _isTyping;
    private bool _isSkipped;
    private int _dialogueIndex = 0;
    private List<Button> _createdButtons = new();
    
    public event Action OnDialogueSequenceEnd;


    private void OnEnable()
    {
        inputActions = PlayerMovement.Controls;
        inputActions.Player.Interact.Enable();

        inputActions.Player.Interact.performed += OnMouseClick;
    }

    public void Init(DialogueSequence sequence)
    {
        _speakersName.text = sequence.Monologues[0].speakerName;
        _dialogueSequence = sequence;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        if (_dialogueIndex == _dialogueSequence.Monologues.Count)
        {
            if (_dialogueSequence.AvailableChoices.Count != 0)
            {
                if (_choicePanel.activeSelf)
                    return;

                _choicePanel.SetActive(true);
                _scrollRect.gameObject.SetActive(false);
                for (int i = 0; i < _dialogueSequence.AvailableChoices.Count; i++)
                {
                    DialogueChoice choice = _dialogueSequence.AvailableChoices[i];
                    var button = Instantiate(_choiceButton, _choicePanel.transform);
                    _createdButtons.Add(button);
                    int temp = i;
                    button.onClick.AddListener(() =>
                    {
                        ChooseDialogueSequenceOnClick(temp);
                        _createdButtons.ForEach(b => Destroy(b.gameObject));
                        _createdButtons.Clear();
                    });
                    button.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceDescription;
                }

                return;
            }


            OnDialogueSequenceEnd();
            inputActions.Player.Interact.performed -= OnMouseClick;
            return;
        }

        if (_isTyping)
        {
            _isSkipped = true;
        }
        else
        {
            StartCoroutine(TypeSymbols(_dialogueSequence.Monologues[_dialogueIndex]));
        }

    }
    private void ChooseDialogueSequenceOnClick(int buttonIndex)
    {
        _dialogueIndex = 0;
        _dialogueSequence = _dialogueSequence.AvailableChoices[buttonIndex].avalableChoice;
        _scrollRect.gameObject.SetActive(true);
        _choicePanel.SetActive(false);

        StartCoroutine(TypeSymbols(_dialogueSequence.Monologues[_dialogueIndex]));
    }

    private IEnumerator TypeSymbols(DialogueSpeaker monologue)
    {
        _isTyping = true;
        _slidingText.text = string.Empty;
        _speakersName.text = monologue.speakerName;

        for (int i = 0; i < monologue.sequence.Length; i++)
        {
            if (_isSkipped)
            {
                _slidingText.text = monologue.sequence;
                _scrollRect.verticalNormalizedPosition = 0;
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_scrollRect.transform);
                _isSkipped = false;
                break;
            }

            _slidingText.text += monologue.sequence[i];
            _scrollRect.verticalNormalizedPosition = 0;
            yield return new WaitForSeconds(_dialogueSequence.DelayPerSymbol);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_scrollRect.transform);
        }

        _isTyping = false;
        _dialogueIndex++;
    }
}
