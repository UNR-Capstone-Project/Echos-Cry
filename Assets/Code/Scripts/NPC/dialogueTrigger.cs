using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;
using Unity.Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    public static event Action onInteractWithDialogue;

    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject ToolTipPrefab;
    [SerializeField] private InputTranslator _inputTranslator;

    void Start()
    {
        _inputTranslator.OnInteractEvent += InteractCheck;
    }
    void OnDestroy()
    {
        _inputTranslator.OnInteractEvent += InteractCheck;
    }

    private void InteractCheck()
    {
        if (playerInRange && !DialogueManager.Instance.isDialoguePlaying)
        {
            DialogueManager.Instance.EnterDialogueMode(inkJSON);
            onInteractWithDialogue?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToolTipPrefab.GetComponent<ToolTip>().text =
                $"Press '{_inputTranslator.PlayerInputs.Gameplay.Interact.GetBindingDisplayString(InputBinding.MaskByGroup("KeyboardMouse"))}' to talk.";
            Instantiate(ToolTipPrefab, this.transform.position + new Vector3(0, 1, -1), Quaternion.identity);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;

            if (DialogueManager.Instance.isDialoguePlaying)
            {
                StartCoroutine(DialogueManager.Instance.ExitDialogueMode());
            }
        }
    }
}