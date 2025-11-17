using UnityEngine;

public class shopkeeper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject shopCanvas;
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("open shop");
            shopCanvas.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
