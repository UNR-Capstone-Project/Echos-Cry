using AudioSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class shopkeeper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool playerInRange = false;
    [SerializeField] private GameObject ToolTipPrefab;
    [SerializeField] private soundEffect shopOpenSFX;
    [SerializeField] private InputTranslator translator;

    private void OpenShop()
    {
        translator.PlayerInputs.ShopMenu.Enable();
        translator.PlayerInputs.Gameplay.Disable();

        VolumeManager.Instance.SetDepthOfField(true);
        soundEffectManager.Instance.Builder
            .setSound(shopOpenSFX)
            .setSoundPosition(this.transform.position)
            .ValidateAndPlaySound();

        MenuManager.Instance.SetMenu("Shop");
    }
    private void CloseShop(){
        MenuManager.Instance.SetMenu("HUD");
        VolumeManager.Instance.SetDepthOfField(false);
    }

    void OnTriggerEnter(Collider other)
    {
        ToolTipPrefab.GetComponent<ToolTip>().text = 
            $"Press '{translator.PlayerInputs.Gameplay.Interact.GetBindingDisplayString()}' to Shop";
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
        //translator.OnShopEvent += OpenShop;
        translator.OnCloseShopEvent += CloseShop;
        translator.OnInteractEvent += RequestOpenShop;
    }
    void OnDestroy(){
        //translator.OnShopEvent -= OpenShop;
        translator.OnCloseShopEvent -= CloseShop;
        translator.OnInteractEvent -= RequestOpenShop;
    }
}
