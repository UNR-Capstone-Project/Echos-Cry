using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class DialogueChoiceButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI _choiceText;
    private int _choiceIndex = -1;

    public void OnSelect(BaseEventData eventData)
    {
        DialogueEvents.Instance.UpdateChoiceIndex(_choiceIndex);
    }

    public void SelectButton()
    {
        button.Select();
    }

    public void SetChoiceIndex(int choiceIndex)
    {
        _choiceIndex = choiceIndex;
    }

    public void SetChoiceText(string choiceTextString)
    {
        _choiceText.text = choiceTextString;
    }
}
