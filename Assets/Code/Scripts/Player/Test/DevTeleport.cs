using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DevTeleport : MonoBehaviour
{
    [SerializeField] private InputTranslator translator;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        translator.OnTeleportEvent += TeleportPlayer;
    }
    private void OnDestroy()
    {
        translator.OnTeleportEvent += TeleportPlayer;
    }

    private void TeleportPlayer(bool isPressed)
    {
        Ray ray = CameraManager.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ground))
        {
            playerTransform.position = hit.point;
        }
    }
}
