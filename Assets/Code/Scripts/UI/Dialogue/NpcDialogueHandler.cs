using UnityEngine;
using UnityEngine.InputSystem;

public class NpcDialogueHandler : MonoBehaviour
{
    [SerializeField] private InputTranslator translator;
    [SerializeField] private GameObject ToolTipPrefab;

    [Header("Dialogue Knot from Ink")]
    [SerializeField] private string _dialogueKnotName;

    private bool _playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        ToolTipPrefab.GetComponent<ToolTip>().text =
            $"Press '{translator.PlayerInputs.Gameplay.Interact.GetBindingDisplayString()}' to talk.";
        Instantiate(ToolTipPrefab, this.transform.position + new Vector3(0, 1, -1), Quaternion.identity);
        _playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerInRange = false;
    }

    private void RequestDialogue()
    {
        if (_playerInRange)
        {
            if (!_dialogueKnotName.Equals(""))
            {
                DialogueEvents.Instance.EnterDialogue(_dialogueKnotName);
            }
        }
    }

    private void RequestSubmit()
    {
        if (_playerInRange)
        {
            DialogueEvents.Instance.SubmitPressed();
        }
    }

    void Start()
    {
        translator.OnInteractEvent += RequestDialogue;
        translator.OnSubmitEvent += RequestSubmit;
    }
    void OnDestroy()
    {
        translator.OnInteractEvent += RequestDialogue;
        translator.OnSubmitEvent -= RequestSubmit;
    }
}
