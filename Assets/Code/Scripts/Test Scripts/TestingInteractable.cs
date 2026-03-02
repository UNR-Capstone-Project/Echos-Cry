using UnityEngine;

public class TestingInteractable : MonoBehaviour, IInteractable
{
    Material material;
    private bool toggle;
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;   
    }
    public void Execute()
    {
        toggle = !toggle;
        if (toggle) material.SetColor("_BaseColor", Color.green);
        else material.SetColor("_BaseColor", Color.red);
    }
}
