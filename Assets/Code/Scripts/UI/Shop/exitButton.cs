using UnityEngine;
/// Original Author: Abby
/// All Contributors Since Creation: Abby
/// Last Modified By:
public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject shopCanvas;
    public void exit(){
        shopCanvas.SetActive(false);
    }
}
