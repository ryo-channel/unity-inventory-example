using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefabs;
    public Transform slotPanel;
    public UIItem selectedItem;
    public Tooltip tootip;

    public int numOfSlots = 16;

    private void Awake()
    {
        for (int i = 0; i < numOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefabs);
            instance.transform.SetParent(slotPanel);
            instance.GetComponentInChildren<UIItem>().OnClick.AddListener(OnClickItem);
            instance.GetComponentInChildren<UIItem>().OnHoverStart.AddListener(OnPointerEnterToItem);
            instance.GetComponentInChildren<UIItem>().OnHoverEnd.AddListener(OnPointerExitFromItem);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        uiItems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i => i.item == item), null);
    }

    public void OnClickItem(UIItem uiItem)
    {
        if (uiItem.item != null)
        {
            // 選択されたアイテムを表示するスロットにすでにアイテムがある場合
            if (this.selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(uiItem.item);
                uiItem.UpdateItem(clone);
            }
            // 選択されたアイテムを表示するスロットが空の場合
            else
            {
                selectedItem.UpdateItem(uiItem.item);
                uiItem.UpdateItem(null);
            }
        }
        else if (selectedItem.item != null)
        {
            uiItem.UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }

    public void OnPointerEnterToItem(UIItem uiItem)
    {
        if (uiItem.item != null)
        {
            tootip.GenerateTooltip(uiItem.item);
        }
    }

    public void OnPointerExitFromItem(UIItem uiItem)
    {
        tootip.gameObject.SetActive(false);
    }


}
