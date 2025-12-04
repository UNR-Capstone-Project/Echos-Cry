using UnityEngine;
using System.Collections.Generic;
public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;
    public static InventoryManager Instance { get { return _instance; } }

    private Dictionary<inventoryItemData, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory { get; private set;}  

    //public PlayerStats player;

    [SerializeField] private InventoryDisplay _inventoryDisplay;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }

    public void AddInventorySlot(slotScript _slotScript)
    {
        for (int i = 0; i < _inventoryDisplay.slotScriptArray.Length; i++)
        {
            if (_inventoryDisplay.slotScriptArray[i] == null)
            {
                _inventoryDisplay.slotScriptArray[i] = _slotScript;
                return;
            }
        }
    }

    void Start()
    {
        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<inventoryItemData, InventoryItem>();

        InputTranslator.OnItem1Event += UseItem1;
        InputTranslator.OnItem2Event += UseItem2;
        InputTranslator.OnItem3Event += UseItem3;
        InputTranslator.OnItem4Event += UseItem4;
    }
    private void healthPotion(){
        PlayerStats.OnDamageHealed(10f);
        Debug.Log("Health");
    }
    private void shieldPotion(){
        Debug.Log("Shield");
    }
    private void attackPotion(){
        Debug.Log("Attack");
    }
    private void speedPotion(){
        Debug.Log("Speed");
    }
    private void UseItem1(){
        //at index 0
        int i = 0;
        InventoryItem usedItem = null;
        foreach(InventoryItem item in InventoryManager.Instance.inventory){
            if (i == 0 && item.data != null){
                usedItem = new InventoryItem(item.data);
                if(item.data.id == "healthP"){
                    healthPotion();
                }else if(item.data.id == "shieldP"){
                    shieldPotion();
                }else if(item.data.id == "attackP"){
                    attackPotion();
                }else if(item.data.id == "speedP"){
                    speedPotion();
                }
            }else{
                i++;
            }
        }
        if(usedItem != null) Remove(usedItem.data);
    }
    private void UseItem2(){
        //at index 1
        int i = 0;
        InventoryItem usedItem = null;
        foreach(InventoryItem item in InventoryManager.Instance.inventory){
            if (i == 1 && item.data != null)
            {
                usedItem = new InventoryItem(item.data);
                if(item.data.id == "healthP"){
                    healthPotion();
                }else if(item.data.id == "shieldP"){
                    shieldPotion();
                }else if(item.data.id == "attackP"){
                    attackPotion();
                }else if(item.data.id == "speedP"){
                    speedPotion();
                }
            }else{
                i++;
            }
        }
        if (usedItem != null) Remove(usedItem.data);
    }
    private void UseItem3(){
        //at index 2
        int i = 0;
        InventoryItem usedItem = null;
        foreach(InventoryItem item in InventoryManager.Instance.inventory){
            if (i == 2 && item.data != null)
            {
                usedItem = new InventoryItem(item.data);
                if(item.data.id == "healthP"){
                    healthPotion();
                }else if(item.data.id == "shieldP"){
                    shieldPotion();
                }else if(item.data.id == "attackP"){
                    attackPotion();
                }else if(item.data.id == "speedP"){
                    speedPotion();
                }
            }else{
                i++;
            }
        }
        if (usedItem != null) Remove(usedItem.data);
    }
    private void UseItem4(){
        //at index 3
        int i = 0;
        InventoryItem usedItem = null;
        foreach(InventoryItem item in InventoryManager.Instance.inventory){
            if (i == 3 && item.data != null)
            {
                usedItem = new InventoryItem(item.data);
                if(item.data.id == "healthP"){
                    healthPotion();
                }else if(item.data.id == "shieldP"){
                    shieldPotion();
                }else if(item.data.id == "attackP"){
                    attackPotion();
                }else if(item.data.id == "speedP"){
                    speedPotion();
                }
            }else{
                i++;
            }
        }
        if (usedItem != null) Remove(usedItem.data);
    }
    void OnDestroy(){
        InputTranslator.OnItem1Event -= UseItem1;
        InputTranslator.OnItem2Event -= UseItem2;
        InputTranslator.OnItem3Event -= UseItem3;
        InputTranslator.OnItem4Event -= UseItem4;
    }
    public InventoryItem Get(inventoryItemData referenceData){
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){
            return value;
        }
        return null;
    }
    public void Add(inventoryItemData referenceData){
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){
            value.AddToStack();
        }else{
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
    }
    public void Remove(inventoryItemData referenceData){
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){
            value.RemoveFromStack();
            if(value.stackSize == 0){
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
    }
}
public class InventoryItem{
    public inventoryItemData data {get; private set; }
    public int stackSize {get; private set;}

    public InventoryItem(inventoryItemData source){
        data = source;
        AddToStack();
    }

    public void AddToStack(){
        stackSize++;
    }

    public void RemoveFromStack(){
        stackSize--;
    }
}
