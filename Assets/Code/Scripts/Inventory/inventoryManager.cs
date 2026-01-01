using UnityEngine;
using System.Collections.Generic;
/// Original Author: Abby
/// All Contributors Since Creation: Abby
/// Last Modified By:
public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;
    public static InventoryManager Instance { get { return _instance; } }

    private Dictionary<inventoryItemData, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory { get; private set;}  

    //public PlayerStats player;

    [SerializeField] private InventoryDisplay _inventoryDisplay;
    [SerializeField] private InputTranslator _inputTranslator;

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

        _inputTranslator.OnItem1Event += UseItem1;
        _inputTranslator.OnItem2Event += UseItem2;
        _inputTranslator.OnItem3Event += UseItem3;
        _inputTranslator.OnItem4Event += UseItem4;
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
    private void UseItem(int index)
    {
        if (inventory.Count <= 0 || inventory.Count <= index) return;

        InventoryItem usedItem = inventory[index];
        if (usedItem == null || usedItem.data == null) return;

        if (usedItem.data.id == "healthP")
        {
            if (PlayerStats.CurrentHealth == PlayerStats.MaxHealth) return;
            healthPotion();
        }
        else if (usedItem.data.id == "shieldP")
        {
            shieldPotion();
        }
        else if (usedItem.data.id == "attackP")
        {
            attackPotion();
        }
        else if (usedItem.data.id == "speedP")
        {
            speedPotion();
        }
        Remove(usedItem.data);
    }

    private void UseItem1(){
        UseItem(0);
    }
    private void UseItem2(){
        UseItem(1);
    }
    private void UseItem3(){
        UseItem(2);
    }
    private void UseItem4(){
        UseItem(3);
    }
    void OnDestroy(){
        _inputTranslator.OnItem1Event -= UseItem1;
        _inputTranslator.OnItem2Event -= UseItem2;
        _inputTranslator.OnItem3Event -= UseItem3;
        _inputTranslator.OnItem4Event -= UseItem4;
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
