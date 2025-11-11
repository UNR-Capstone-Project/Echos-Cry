using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerDirection : MonoBehaviour
{
    [SerializeField] DecalProjector playerAimMarker;

    private Vector3 aimDirection;
    private Quaternion aimingRotation;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        int groundMask = LayerMask.GetMask("Ground");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3 hitPoint = hit.point;

            aimDirection = (hitPoint - transform.parent.position).normalized; //Calculates the direction between the mouse mapped to world space and the players position.
            aimDirection.y = 0;

            aimingRotation = Quaternion.LookRotation(aimDirection);

            Quaternion decalRotation = Quaternion.Euler(new Vector3(90, aimingRotation.eulerAngles.y, 0));
            transform.rotation = decalRotation;
        }
    }

    public Vector3 GetAimDirection() { return aimDirection; }
    public Quaternion GetAimRotation() { return aimingRotation; }
}
