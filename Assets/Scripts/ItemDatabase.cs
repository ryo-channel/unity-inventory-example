using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();


    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return this.items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return this.items.Find(item => item.title == itemName);
    }

    // itemsリストを作成する
    void BuildDatabase()
    {
        items = new List<Item>()
        {
            new Item(0, "Diamond Sword", "A sword with diamond",
            new Dictionary<string, int>
            {
                {"Power", 15 },
                {"Defence", 10 },
            },
            craftable:true,
            requiredItems:new List<Item>
            {
                new Item(3, "Diamond Ore", "A beautiful diamond",
                new Dictionary<string, int>
                {
                    {"Value", 444 },
                }),
                new Item(6, "Wood Stick", "Wooden stick.",
                new Dictionary<string, int>()
                ),
            }
            ),
            new Item(1, "Diamond Pick", "A pickel with diamond",
            new Dictionary<string, int>
            {
                {"Power", 10 },
                {"Mining", 500 },
            }),
            new Item(2, "Silver Pick", "A pickel with silver",
            new Dictionary<string, int>
            {
                {"Power", 5 },
                {"Mining", 350 },
            }),
            new Item(3, "Diamond Ore", "A beautiful diamond",
            new Dictionary<string, int>
            {
                {"Value", 444 },
            }),
            new Item(4, "Gold Ore", "A expensive gold",
            new Dictionary<string, int>
            {
                {"Value", 333 },
            }),
            new Item(5, "Emerald Ore", "A human loved emerald.",
            new Dictionary<string, int>
            {
                {"Value", 222 },
            }),
            new Item(6, "Wood Stick", "Wooden stick.",
            new Dictionary<string, int>()
            ),
        };
    }
   
}
