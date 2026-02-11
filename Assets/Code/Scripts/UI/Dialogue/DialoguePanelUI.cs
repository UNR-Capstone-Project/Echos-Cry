using UnityEngine;
using System;
using TMPro;

public class DialoguePanelUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText;

    private void OnEnable()
    {
        DialogueEvents.Instance.OnDialogueEnded += DialogueFinished;
        DialogueEvents.Instance.OnDisplayDialogue += DisplayDialogue;
    }
    private void OnDisable()
    {
        DialogueEvents.Instance.OnDialogueEnded -= DialogueFinished;
        DialogueEvents.Instance.OnDisplayDialogue -= DisplayDialogue;
    }

    public void ChoiceIndexButton(int choiceIndex)
    {
        DialogueEvents.Instance.UpdateChoiceIndex(choiceIndex);
    }

    private void DialogueFinished()
    {
        _dialogueText.text = "";
    }

    private void DisplayDialogue(string dialogueLine)
    {
        _dialogueText.text = dialogueLine;
    }
}
