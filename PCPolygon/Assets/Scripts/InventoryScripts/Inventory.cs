﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject inventoryPanel;
    public GameObject inventorySlotPanel;
    public GameObject toolbarSlotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    //Sets up basic system for items to go in
    void Start()
    {
        database = GetComponent<ItemDatabase>();

        slotAmount = 15;
        //inventoryPanel = GameObject.Find("Inventory Panel");
        //inventorySlotPanel = inventoryPanel.transform.Find("Inventory Slot Panel").gameObject;
        //toolbarSlotPanel = inventoryPanel.transform.Find("Toolbar Slot Panel").gameObject;

        for (int i = 0; i < 5; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<ItemSlot>().id = i;
            slots[i].transform.SetParent(toolbarSlotPanel.transform);
        }
        
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i+5].GetComponent<ItemSlot>().id = i+5;
            slots[i+5].transform.SetParent(inventorySlotPanel.transform);
        }
    }

    //Test code to test adding items
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            AddItem(3000);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AddItem(3001);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            AddItem(3002);
        }
        if (Input.GetKeyDown(KeyCode.Q))
            DestroyItemById(3000);*/
    }

    /// <summary>
    /// Adds an item to the inventory
    /// </summary>
    /// <param name="id"></param>
    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        //lazy kid system. not very good. if we ever do optimization we need to change this
        if (itemToAdd.Stackable && DoesItemAlreadyExist(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1 && (id <= itemToAdd.Type || id >= 5))
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = itemObj.transform.parent.position;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.transform.parent.name = itemToAdd.Title;
                    break;
                }
            }
        }     
    }

    #region WIP
    /// <summary>
    /// Used for destroying an entire stack of items
    /// </summary>
    /// <param name="itemID"></param>
    void DestroyItemById(int itemID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == itemID)
            {
                items[i] = new Item();
                break;
            }
        }
        Debug.Log("Can't find object by ID. Might want to try something else");
    }

    /// <summary>
    /// Used for destroying a certain amount of items from a stack
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="destroyAmount"></param>
    void DestroyItemById(int itemID, int destroyAmount)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == itemID)
            {
                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                data.amount -= destroyAmount;
                data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                if (slots[i].transform.GetChild(0).GetComponent<ItemData>().amount < 0)
                {
                    Debug.Log("Attempted to delete " + Math.Abs(data.amount) + " too many items of ID:" + itemID);
                    data.amount = 0;
                }
                if (data.amount == 0)
                    items[i] = new Item();
                break;
            }
        }
    }
    #endregion

    //Tests to see if the item already exists.
    bool DoesItemAlreadyExist(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
                return true;
        }
        return false;
    }
}
