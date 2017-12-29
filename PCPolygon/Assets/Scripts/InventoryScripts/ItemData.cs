using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IEndDragHandler
{
    public Item item;
    public int amount;
    public int slot;

    private Inventory inv;
    private Tooltip tooltip;
    private Vector2 offset;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
    }

    //Moving Items
    public void OnPointerDown(PointerEventData eventData)
    {
        if (item!= null)
        {
            offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
            transform.SetParent(transform.parent.parent);
            transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(inv.slots[slot].transform);
        transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.SetParent(inv.slots[slot].transform);
        transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            transform.position = eventData.position - offset;
        }
    }
    
    //Tooltip stuff
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }

}
