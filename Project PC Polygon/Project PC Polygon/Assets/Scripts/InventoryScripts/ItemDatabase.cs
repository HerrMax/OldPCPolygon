using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    private void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }

    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
            if (database[i].ID == id)
                return database[i];

        Debug.Log("Item not found by ID. Should have a look at that boi...");
        return null;
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["stats"]["damage"], (int)itemData[i]["stats"]["range"], (int)itemData[i]["stats"]["rateOfFire"], (int)itemData[i]["stats"]["magSize"], itemData[i]["description"].ToString(), (bool)itemData[i]["stackable"], (int)itemData[i]["rarity"], itemData[i]["slug"].ToString()));
        }
    }
}



public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Damage { get; set; }
    public int Range { get; set; }
    public int RateOfFire { get; set; }
    public int MagSize { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }


    public Item(int id, string title, int damage, int range, int rateOfFire, int magsize, string description, bool stackable, int rarity, string slug)
    {
        ID = id;
        Title = title;
        Damage = damage;
        Range = range;
        RateOfFire = rateOfFire;
        MagSize = magsize;
        Description = description;
        Stackable = stackable;
        Rarity = rarity;
        Slug = slug;
        Sprite = Resources.Load<Sprite>("InventorySprites/Icons/" + slug);
    }

    public Item()
    {
        ID = -1;
    }
}
