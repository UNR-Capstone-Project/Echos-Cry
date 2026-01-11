using UnityEngine;

public class exitShop : MonoBehaviour
{
    public GameObject shopCanvas;
    [SerializeField] private InputTranslator _translator;
    public void close(){
        Debug.Log("close shop");
        shopCanvas.SetActive(false);
    }
    private void Start()
    {
        //_translator.OnCloseShopEvent += close;
    }
    private void OnDestroy()
    {
        //_translator.OnCloseShopEvent -= close;
    }
}
