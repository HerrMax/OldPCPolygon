using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

        if(id <= droppedItem.item.Type || id >= 5)
        {
            if (inv.items[id].ID == -1)
            {
                inv.items[droppedItem.slot] = new Item();
                inv.items[id] = droppedItem.item;
                droppedItem.slot = id;
            }
            else if (droppedItem.slot != id)
            {
                Transform item = transform.GetChild(0);
                if (droppedItem.slot <= item.GetComponent<ItemData>().item.Type || droppedItem.slot >= 5)
                {
                    item.GetComponent<ItemData>().slot = droppedItem.slot;
                    item.transform.SetParent(inv.slots[droppedItem.slot].transform);
                    item.transform.position = inv.slots[droppedItem.slot].transform.position;

                    inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
                    inv.items[id] = droppedItem.item;

                    droppedItem.slot = id;
                    droppedItem.transform.SetParent(transform);
                    droppedItem.transform.position = transform.position;
                }
            }
        }
    }
}
