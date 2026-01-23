using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOrientation : MonoBehaviour
{
    private static Vector3 aimDirection;
    private static Quaternion aimRotation;
    public static Vector3 Direction { get { return aimDirection; } }
    public static Quaternion Rotation { get { return aimRotation; } }

    [SerializeField] private LayerMask groundLayer;

    void Update()
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
