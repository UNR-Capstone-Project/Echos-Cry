using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject shopCanvas;
    public void exit(){
        shopCanvas.SetActive(false);
    }
}
