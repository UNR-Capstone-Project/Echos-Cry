using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public GameObject shopCanvas;
    public void Exit(){
        shopCanvas.SetActive(false);
    }
}
