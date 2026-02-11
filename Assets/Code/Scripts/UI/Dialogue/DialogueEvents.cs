using System;
using UnityEditor;
using UnityEngine;

public class DialogueEvents : MonoBehaviour
{
    public static DialogueEvents Instance { get; private set; }

    public event Action<string> OnEnterDiaglogue;
    public event Action OnSubmitPressed;

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
}
