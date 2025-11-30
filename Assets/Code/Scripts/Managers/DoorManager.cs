using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
