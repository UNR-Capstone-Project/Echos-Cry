using UnityEngine;

public class shopkeeper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject shopCanvas;
    private void OpenShop()
    {
        InputTranslator.Instance.PlayerInputs.ShopMenu.Enable();
        InputTranslator.Instance.PlayerInputs.Gameplay.Disable();
        shopCanvas.SetActive(true);
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("open shop");
            OpenShop();
        }
    }
}
