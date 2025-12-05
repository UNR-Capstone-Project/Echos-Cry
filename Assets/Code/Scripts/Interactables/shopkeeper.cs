using UnityEngine;
using UnityEngine.InputSystem;

public class shopkeeper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject shopCanvas;
    private bool playerInRange = false;
    [SerializeField] private GameObject ToolTipPrefab;

    private void OpenShop()
    {
        InputTranslator.Instance.PlayerInputs.ShopMenu.Enable();
        InputTranslator.Instance.PlayerInputs.Gameplay.Disable();
        shopCanvas.SetActive(true);
        VolumeManager.Instance.SetDepthOfField(true);
    }
    private void CloseShop(){
        shopCanvas.SetActive(false);
        VolumeManager.Instance.SetDepthOfField(false);
    }

    void OnTriggerEnter(Collider other)
    {
        ToolTipPrefab.GetComponent<ToolTip>().text = $"Press '{InputTranslator.Instance.PlayerInputs.Gameplay.Interact.GetBindingDisplayString()}' to Shop";
        Instantiate(ToolTipPrefab, this.transform.position + new Vector3(0, 1, -1), Quaternion.identity);
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
