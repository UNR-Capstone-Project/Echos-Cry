using System;
using UnityEditor;
using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;

public class DialogueEvents : MonoBehaviour
{
    public static DialogueEvents Instance { get; private set; }

    public event Action<string> OnEnterDiaglogue;
    public event Action OnSubmitPressed;
    public event Action OnDialogueStarted;
    public event Action OnDialogueEnded;
    public event Action<string, List<Choice>> OnDisplayDialogue;
    public event Action<int> OnUpdateChoiceIndex;
    public bool InDialogue = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of DialogueEvents detected. There should only be one instance of DialogueEvents in the scene.");
        }
        Instance = this;
    }
    public void EnterDialogue(string knotName)
    {
        OnEnterDiaglogue?.Invoke(knotName);
    }

    public void SubmitPressed()
    {
        OnSubmitPressed?.Invoke();
    }

    public  void DialogueStarted()
    {
        OnDialogueStarted?.Invoke();
        InDialogue = true;
    }

    public void DialogueEnded()
    {
        OnDialogueEnded?.Invoke();
        InDialogue = false;
    }

    public void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        OnDisplayDialogue?.Invoke(dialogueLine, dialogueChoices);
    }

    public void UpdateChoiceIndex(int choiceIndex)
    {
        OnUpdateChoiceIndex?.Invoke(choiceIndex);
    }
}
