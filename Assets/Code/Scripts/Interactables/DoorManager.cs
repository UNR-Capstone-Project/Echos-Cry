using UnityEngine;
using UnityEngine.InputSystem;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject ToolTipPrefab;

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToolTipPrefab.GetComponent<ToolTip>().text = $"Press '{InputTranslator.Instance.PlayerInputs.Gameplay.Interact.GetBindingDisplayString()}' to Open";
            Instantiate(ToolTipPrefab, this.transform.position + new Vector3(0, 2, -1), Quaternion.identity);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void OpenDoor()
    {
        if (playerInRange)
        {
            doorAnimator.SetTrigger("Interact");
        }
    }

    void Start()
    {
        InputTranslator.OnInteractEvent += OpenDoor;
    }
    private void OnDestroy()
    {
        InputTranslator.OnInteractEvent -= OpenDoor;
    }
}
