using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;
    public bool craftable;
    public List<Item> requiredItems = new List<Item>();
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    // 各プロパティでItemを作るメソッド
    public Item(int id, string title, string description, Dictionary<string, int> stats, bool craftable = false, List<Item> requiredItems = null)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.craftable = craftable;
        this.requiredItems = requiredItems;
        this.icon = Resources.Load<Sprite>("Sprites/Items/"+title);
        this.stats = stats;
    }

    // 別のItemから新しいItemを作るメソッド
    public Item(Item item)
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.craftable = item.craftable;
        this.requiredItems = item.requiredItems;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + title);
        this.stats = item.stats;
    }
}
