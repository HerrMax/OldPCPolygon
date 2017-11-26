using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        database = GetComponent<ItemDatabase>();

        slotAmount = 15;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;
        for(int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<ItemSlot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddItem(3000);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AddItem(3001);
        }
    }

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
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].ID == -1)
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
