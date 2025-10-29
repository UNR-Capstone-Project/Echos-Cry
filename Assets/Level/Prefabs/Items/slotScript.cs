using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class slotScript : MonoBehaviour{
[SerializeField]
private Image m_icon;

[SerializeField]
private GameObject m_stackObj;

[SerializeField]
private TextMeshProUGUI m_num;

public void Set(inventoryItem item){
    Debug.Log(item.data.icon);
    Debug.Log(m_icon.sprite);
    m_icon.sprite = item.data.icon;
    Debug.Log("check 2");
    if (item.stackSize < 1){
        Debug.Log("<1");
        m_stackObj.SetActive(false);
        return;
    }else{
        Debug.Log("else");
        m_stackObj = item.data.prefab;
    }
    
    m_num.text = item.stackSize.ToString();
}
}