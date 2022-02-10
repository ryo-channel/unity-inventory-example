using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> charactorItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;

    private void Start()
    {
        GiveItem("Diamond Ore");
        GiveItem(1);
        GiveItem(2);
        RemoveItem(1);
        GiveItem(4);
        CheckForItem(itemDatabase.GetItem(4));
        GiveItem(6);
        inventoryUI.gameObject.SetActive(false);
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        charactorItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public void GiveItem(string title)
    {
        Item itemToAdd = itemDatabase.GetItem(title);
        charactorItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public Item CheckForItem(int id)
    {
        Debug.Log(string.Format("Check for item {0}: {1}", id, charactorItems.Find(item => item.id == id) != null));
        return charactorItems.Find(item => item.id == id);
    }

    public Item CheckForItem(Item item)
    {
        Debug.Log(string.Format("Check for item {0}: {1}", item.id, charactorItems.Find(i => i.id == item.id) != null));
        return charactorItems.Find(i => i.id == item.id);
    }

    public void RemoveItem(int id) {
        Item item = CheckForItem(id);
        if (item != null)
        {
            charactorItems.Remove(item);
            inventoryUI.RemoveItem(item);
            Debug.Log("Item Removed: " + item.title);
        }
    }

    public void RemoveItems(List<Item> items)
    {
        foreach (var item in items)
        {
            Item item1 = CheckForItem(item);
            if (item1 != null)
            {
                charactorItems.Remove(item1);
                inventoryUI.RemoveItem(item1);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }
    }
}
