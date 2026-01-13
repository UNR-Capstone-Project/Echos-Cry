using UnityEngine;
using UnityEngine.InputSystem;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject ToolTipPrefab;
    [SerializeField] private InputTranslator translator;
    [SerializeField] private bool isLocked = false;

    [SerializeField] private bool isWaveBased = false;
    [SerializeField] private WaveManager waveManager;

    private bool playerInRange = false;
    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
        {
            if (!isLocked)
            {
                ToolTipPrefab.GetComponent<ToolTip>().text =
                    $"Press '{translator.PlayerInputs.Gameplay.Interact.GetBindingDisplayString()}' to Open";
                Instantiate(ToolTipPrefab, this.transform.position + new Vector3(0, 2, -1), Quaternion.identity);
            }
            else
            {
                ToolTipPrefab.GetComponent<ToolTip>().text = "This Door is Locked.";
                Instantiate(ToolTipPrefab, this.transform.position + new Vector3(0, 2, -1), Quaternion.identity);
            }

            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }

    private void OpenDoor()
    {
        if (playerInRange && !isLocked)
        {
            doorAnimator.SetTrigger("Interact");
            isOpen = true;
        }
    }

    void Start()
    {
        if (isWaveBased && waveManager != null)
        {
            waveManager.OnAllWavesCompleted += () => { isLocked = false; };
        }

        translator.OnInteractEvent += OpenDoor;
    }
    private void OnDestroy()
    {
        if (isWaveBased && waveManager != null)
        {
            waveManager.OnAllWavesCompleted -= () => { isLocked = false; };
        }

        translator.OnInteractEvent -= OpenDoor;
    }
}
