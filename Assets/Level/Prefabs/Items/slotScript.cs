using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class slotScript : MonoBehaviour{
    [SerializeField] private Image m_icon;
    [SerializeField] private GameObject m_stackObj;
    [SerializeField] private TextMeshProUGUI m_num;

    private void Start()
    {
        InventoryManager.Instance.AddInventorySlot(this);
    }

    public void Set(InventoryItem item){
        if(item == null){
            m_stackObj = null;
            m_num.text = "0";
            m_icon.sprite = null;
            return;
        }
        m_icon.sprite = item.data.icon;
        if (item.stackSize < 1){
            m_stackObj = null;
            m_num.text = "0";
            return;
        }else{
            m_stackObj = item.data.prefab;
        }
        m_num.text = item.stackSize.ToString();
    }
}