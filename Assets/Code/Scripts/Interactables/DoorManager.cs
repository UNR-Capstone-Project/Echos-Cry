using UnityEngine;
using UnityEngine.InputSystem;

public abstract class DoorManager : MonoBehaviour
{
    [SerializeField] protected Animator doorAnimator;
    [SerializeField] protected GameObject ToolTipPrefab;
    [SerializeField] protected InputTranslator translator;
    [SerializeField] protected AudioSystem.soundEffect doorOpenSoundEffect;

    //[SerializeField] private WaveManager waveManager;

    protected bool playerInRange = false;
    protected bool isOpen = false;
    protected bool isLocked = false;

    protected virtual void OnTriggerEnter(Collider other)
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

    protected virtual void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }

    protected virtual void OpenDoor()
    {
        if (playerInRange && !isLocked && !isOpen)
        {
            SoundEffectManager.Instance.Builder
            .SetSound(doorOpenSoundEffect)
            .SetSoundPosition(transform.position)
            .ValidateAndPlaySound();

            doorAnimator.SetTrigger("Interact");
            isOpen = true;
        }
    }

    protected virtual void Start()
    {
        //if (isWaveBased && waveManager != null)
        //{
        //    waveManager.OnAllWavesCompleted += () => { isLocked = false; };
        //}

        translator.OnInteractEvent += OpenDoor;
    }

    protected virtual void OnDestroy()
    {
        //if (isWaveBased && waveManager != null)
        //{
        //   waveManager.OnAllWavesCompleted -= () => { isLocked = false; };
        //}

        translator.OnInteractEvent -= OpenDoor;
    }
}
