using AudioSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class NpcDialogueHandler : MonoBehaviour
{
    [SerializeField] private InputTranslator _translator;
    [SerializeField] private GameObject ToolTipPrefab;
    [SerializeField] private soundEffect _submitSound;

    [Header("Dialogue Knot from Ink")]
    [SerializeField] private string _dialogueKnotName;

    private bool _playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        ToolTipPrefab.GetComponent<ToolTip>().text =
            $"Press '{_translator.PlayerInputs.Gameplay.Interact.GetBindingDisplayString()}' to talk.";
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
        if (_playerInRange && DialogueEvents.Instance.InDialogue)
        {
            DialogueEvents.Instance.SubmitPressed();

            SoundEffectManager.Instance.Builder
                .SetSound(_submitSound)
                .SetSoundPosition(PlayerRef.Transform.position)
                .ValidateAndPlaySound();
        }
    }

    void Start()
    {
        _translator.OnInteractEvent += RequestDialogue;
        _translator.OnSubmitEvent += RequestSubmit;
    }
    void OnDestroy()
    {
        _translator.OnInteractEvent += RequestDialogue;
        _translator.OnSubmitEvent -= RequestSubmit;
    }
}
