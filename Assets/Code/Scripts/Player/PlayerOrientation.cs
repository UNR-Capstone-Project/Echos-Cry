using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOrientation : MonoBehaviour
{

    public bool IsRotating = true;
    private Vector3 aimDirection;
    private Quaternion aimRotation;
    public Vector3 Direction { get { return aimDirection; } }
    public Quaternion Rotation { get { return aimRotation; } }

    [SerializeField] private LayerMask groundLayer;

    private void Update()
    {
        if(IsRotating) UpdateRotation();
    }

    private void UpdateRotation()
    {
        Ray ray = CameraManager.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            //Calculates the direction between the mouse mapped to world space and the players position.
            aimDirection = (hit.point - transform.parent.position).normalized;

            aimDirection.y = 0;
            aimRotation = Quaternion.LookRotation(aimDirection);
            transform.rotation = aimRotation;
        }
    }
}
