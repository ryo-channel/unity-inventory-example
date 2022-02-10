using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraft : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotPanel;
    public Transform requiredItemsSlotPanel;
    public Inventory inventory;
    public Button craftButton;

    private ItemDatabase itemDB;
    private List<Item> craftables;

    private void Awake()
    {
        itemDB = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        craftables = itemDB.items.FindAll(item => item.craftable);
        foreach (var item in craftables)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            instance.GetComponentInChildren<UIItem>().UpdateItem(item);
            instance.GetComponentInChildren<UIItem>().OnClick.AddListener(OnClickItem);
        }
        craftButton.interactable = false;
    }

    // クラフトに必要なアイテムの表示を全て削除する
    private void ClearRequiredItems()
    {
        // requiredItemsSlotPanelの子要素を全て削除する
        foreach(Transform child in requiredItemsSlotPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnClickItem(UIItem uiItem)
    {
        if (uiItem.item.craftable)
        {
            ClearRequiredItems();
            bool isCraftable = true;
            foreach (var item in uiItem.item.requiredItems)
            {
                GameObject instance = Instantiate(slotPrefab);
                instance.transform.SetParent(requiredItemsSlotPanel);
                instance.GetComponentInChildren<UIItem>().UpdateItem(item);
                isCraftable = isCraftable && inventory.CheckForItem(item.id) != null;
            }
            craftButton.interactable = isCraftable;
            craftButton.onClick.AddListener(() => {
                inventory.RemoveItems(uiItem.item.requiredItems);
                inventory.GiveItem(uiItem.item.id);
                craftButton.interactable = false;
            });
        }
    }
}
