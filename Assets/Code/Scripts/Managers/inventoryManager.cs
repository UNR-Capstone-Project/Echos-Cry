using UnityEngine;
using System.Collections.Generic;
public class inventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Dictionary<inventoryItemData, inventoryItem> m_itemDictionary;
    public List<inventoryItem> inventory { get; private set; }
    void Start()
    {
        inventory = new List<inventoryItem>();
        m_itemDictionary = new Dictionary<inventoryItemData, inventoryItem>();
    }
    public inventoryItem Get(inventoryItemData referenceData){
        if(m_itemDictionary.TryGetValue(referenceData, out inventoryItem value)){
            return value;
        }
        return null;
    }
    public void Add(inventoryItemData referenceData){
        if(m_itemDictionary.TryGetValue(referenceData, out inventoryItem value)){
            value.AddToStack();
        }else{
            inventoryItem newItem = new inventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
        Debug.Log("item added :3");
    }
    public void Remove(inventoryItemData referenceData){
        if(m_itemDictionary.TryGetValue(referenceData, out inventoryItem value)){
            value.RemoveFromStack();
            if(value.stackSize == 0){
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
public class inventoryItem{
    public inventoryItemData data {get; private set; }
    public int stackSize {get; private set;}

    public inventoryItem(inventoryItemData source){
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
