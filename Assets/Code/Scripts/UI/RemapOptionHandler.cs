using TMPro;
using Unity.AI.Navigation;
using UnityEngine;

public class RemapOptionHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ActionNameText;
    [SerializeField] private TextMeshProUGUI _ActionBindingText;

    public void SetupAction(string name, string binding)
    {
        _ActionNameText.text = name;
        _ActionBindingText.text = binding;
    }
}
