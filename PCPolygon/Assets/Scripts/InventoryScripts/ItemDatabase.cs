﻿using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    //Loads .json and calls a method to create a database
    private void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }

    /// <summary>
    /// Locates an item in the inventory by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Item FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
            if (database[i].ID == id)
                return database[i];

        Debug.Log("Item not found by ID. Should have a look at that boi...");
        return null;
    }

    /// <summary>
    /// Creates a database from the .json file
    /// </summary>
    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            try
            {
                database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["stats"]["damage"], (int)itemData[i]["stats"]["range"], (int)itemData[i]["stats"]["rateOfFire"], (bool)itemData[i]["stats"]["isAuto"], (int)itemData[i]["stats"]["magSize"], itemData[i]["description"].ToString(), (bool)itemData[i]["stackable"], (int)itemData[i]["rarity"], itemData[i]["slug"].ToString(), (int)itemData[i]["type"]));
            }
            catch
            {
                database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), itemData[i]["description"].ToString(), (bool)itemData[i]["stackable"], (int)itemData[i]["rarity"], itemData[i]["slug"].ToString(), (int)itemData[i]["type"]));
            }
            
        }
    }
}


//Setting up the basics for each item
public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Damage { get; set; }
    public int Range { get; set; }
    public int RateOfFire { get; set; }
    public int MagSize { get; set; }
    public bool IsAuto { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }
    public int Type { get; set; }

    //For adding an Item normally
    public Item(int id, string title, int damage, int range, int rateOfFire, bool isAuto, int magsize, string description, bool stackable, int rarity, string slug, int type)
    {
        ID = id;
        Title = title;
        Damage = damage;
        Range = range;
        RateOfFire = rateOfFire;
        MagSize = magsize;
        IsAuto = isAuto;
        Description = description;
        Stackable = stackable;
        Rarity = rarity;
        Slug = slug;
        Sprite = Resources.Load<Sprite>("InventorySprites/Icons/" + slug);
        Type = type;
    }

    public Item(int id, string title, string description, bool stackable, int rarity, string slug, int type)
    {
        ID = id;
        Title = title;
        Description = description;
        Stackable = stackable;
        Rarity = rarity;
        Slug = slug;
        Sprite = Resources.Load<Sprite>("InventorySprites/Icons/" + slug);
        Type = type;
    }

    //For adding a null item
    public Item()
    {
        ID = -1;
    }
}