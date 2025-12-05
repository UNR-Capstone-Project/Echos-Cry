using UnityEngine;
/// Original Author: Abby
/// All Contributors Since Creation: Abby
/// Last Modified By:
public class shopkeeper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject shopCanvas;
    private bool playerInRange = false;

    private void OpenShop()
    {
        InputTranslator.Instance.PlayerInputs.ShopMenu.Enable();
        InputTranslator.Instance.PlayerInputs.Gameplay.Disable();
        shopCanvas.SetActive(true);
    }
    private void CloseShop(){
        shopCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }

    void RequestOpenShop()
    {
        if (playerInRange)
        {
            OpenShop();
        }
    }

    void Start(){
        //InputTranslator.OnShopEvent += OpenShop;
        InputTranslator.OnCloseShopEvent += CloseShop;
        InputTranslator.OnInteractEvent += RequestOpenShop;
    }
    void OnDestroy(){
        //InputTranslator.OnShopEvent -= OpenShop;
        InputTranslator.OnCloseShopEvent -= CloseShop;
        InputTranslator.OnInteractEvent -= RequestOpenShop;
    }
}
