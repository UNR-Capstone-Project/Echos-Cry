using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialoguePanelUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private InputTranslator _translator;
    [SerializeField] private DialogueChoiceButton[] _choiceButtons;

    private void OnEnable()
    {
        _infoText.text = $"Press '{_translator.PlayerInputs.Dialogue.Submit.GetBindingDisplayString()}' to continue.";
        DialogueEvents.Instance.OnDialogueEnded += DialogueFinished;
        DialogueEvents.Instance.OnDisplayDialogue += DisplayDialogue;
    }
    private void OnDisable()
    {
        DialogueEvents.Instance.OnDialogueEnded -= DialogueFinished;
        DialogueEvents.Instance.OnDisplayDialogue -= DisplayDialogue;
    }

    private void DialogueFinished()
    {
        _dialogueText.text = "";
    }

    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        _dialogueText.text = dialogueLine;

        if (dialogueChoices.Count > _choiceButtons.Length)
        {
            Debug.LogError("More dialogue choices ("
                + dialogueChoices.Count + ") came through than are supported ("
                + _choiceButtons.Length + ").");
        }

        foreach (DialogueChoiceButton choiceButton in _choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }

        int choiceButtonIndex = dialogueChoices.Count - 1;
        for (int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
        {
            Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
            DialogueChoiceButton choiceButton = _choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceText(dialogueChoice.text);
            choiceButton.SetChoiceIndex(inkChoiceIndex);

            if (inkChoiceIndex == 0)
            {
                choiceButton.SelectButton();
                DialogueEvents.Instance.UpdateChoiceIndex(0);
            }

            choiceButtonIndex--;
        }
    }
}
