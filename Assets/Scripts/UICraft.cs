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

    // ①スタート時、クラフトできるアイテムのスロットを生成する
    private void Awake()
    {    
        itemDB = GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>();
        craftables = itemDB.items.FindAll(item => item.craftable); // itemDB内でクラフト可能なアイテムのみ選択する
        foreach (var item in craftables)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            instance.GetComponentInChildren<UIItem>().UpdateItem(item);
            instance.GetComponentInChildren<UIItem>().OnClick.AddListener(OnClickItem);  // アイテムがクリックされた時動かす関数(内容はOnClickItem参照)
        }
        craftButton.interactable = false;   // クラフトするボタンは無効にしておく
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

    // ②クラフト可能なアイテムリストのアイテムをクリックしたら、必要なアイテムリストにアイテムを描画していく
    public void OnClickItem(UIItem uiItem)
    {
        // 一応、クラフトできるアイテムかどうかをチェック（たぶんいらない）
        if (uiItem.item.craftable)
        {
            // まずは、クラフトに必要なものリストのPanelをきれいにする
            ClearRequiredItems();

            // クラフト可能かどうかのフラグ
            bool isCraftable = true;
            foreach (var item in uiItem.item.requiredItems)
            {
                // アイテムスロットの生成・描画
                GameObject instance = Instantiate(slotPrefab);
                instance.transform.SetParent(requiredItemsSlotPanel);
                instance.GetComponentInChildren<UIItem>().UpdateItem(item);
                // インベントリに必要なアイテムを持っていなかったらフラグをfalseにする
                isCraftable = isCraftable && inventory.CheckForItem(item.id) != null;
            }

            // isCraftableフラグがtrueの時はボタンが押せるようにする。falseの時はボタンが押せなくなる。
            craftButton.interactable = isCraftable;

            // クラフトボタンが押された時の処理。
            craftButton.onClick.AddListener(() => {
                // インベントリからアイテムを削除
                inventory.RemoveItems(uiItem.item.requiredItems);
                // インベントリにクラフトしたアイテムを追加
                inventory.GiveItem(uiItem.item.id);
                // クラフトボタンを無効にする
                craftButton.interactable = false;
            });
        }
    }
}
