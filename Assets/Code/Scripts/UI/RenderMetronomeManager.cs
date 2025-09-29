using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RenderMetronomeManager : MonoBehaviour
{
    [SerializeField] private GameObject pendulumObject;

    private Animator pendulumAnimator;

    void Start()
    {
        pendulumAnimator = pendulumObject.GetComponent<Animator>();
    }

    public void SetPendulumSpeed(float multiplier)
    {
        pendulumAnimator.Play("PendulumSwing", -1, 0f); //Reset animation
        pendulumAnimator.SetFloat("speedMultiplier", multiplier / 2);
    }
}
