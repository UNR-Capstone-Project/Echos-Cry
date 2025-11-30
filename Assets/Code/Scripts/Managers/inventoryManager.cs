using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;
    public static InventoryManager Instance { get { return _instance; } }

    private Dictionary<inventoryItemData, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory { get; private set; }

    [SerializeField] private InventoryDisplay _inventoryDisplay;

    private void Awake()
    {
        if (_instance != null)
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
        Debug.Log("Health");
    }
    private void shieldPotion(){
        Debug.Log("Shield");
    }
    private void UseItem1(){
        //at index 0
        int i = 0;
        foreach(InventoryItem item in InventoryManager.Instance.inventory){
            if (i == 0){
                if(item.data.id == "healthP"){
                    healthPotion();
                    Remove(item.data);
                    //remove 1
                }else if(item.data.id == "shieldP"){
                    shieldPotion();
                    Remove(item.data);
                    //remove 1
                }
            }else{
                i++;
            }
        }
    }
    private void UseItem2(){
        //at index 1
    }
    private void UseItem3(){
        //at index 2
    }
    private void UseItem4(){
        //at index 3
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
        Debug.Log("item added :3");
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
