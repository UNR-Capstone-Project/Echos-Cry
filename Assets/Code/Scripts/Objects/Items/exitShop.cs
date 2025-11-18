using UnityEngine;

public class exitShop : MonoBehaviour
{
    public GameObject shopCanvas;
    public void close(){
        Debug.Log("close shop");
        shopCanvas.SetActive(false);
    }
}
