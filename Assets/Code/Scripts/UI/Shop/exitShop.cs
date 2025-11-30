using UnityEngine;

public class exitShop : MonoBehaviour
{
    public GameObject shopCanvas;
    public void close(){
        Debug.Log("close shop");
        shopCanvas.SetActive(false);
    }
    private void Start()
    {
        InputTranslator.OnCloseShopEvent += close;
    }
    private void OnDestroy()
    {
        InputTranslator.OnCloseShopEvent -= close;
    }
}
