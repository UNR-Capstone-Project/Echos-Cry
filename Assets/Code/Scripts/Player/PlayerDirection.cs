using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerDirection : MonoBehaviour
{
    //[SerializeField] DecalProjector playerAimMarker;

    private Vector3 aimDirection;
    private Quaternion aimingRotation;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            aimDirection = (hit.point - transform.parent.position).normalized; //Calculates the direction between the mouse mapped to world space and the players position.
            aimDirection.y = 0;
            aimingRotation = Quaternion.LookRotation(aimDirection) * Quaternion.AngleAxis(90f, Vector3.right);
            transform.rotation = aimingRotation;
        }
    }

    public Vector3 GetAimDirection() { return aimDirection; }
    public Quaternion GetAimRotation() { return aimingRotation; }
}
