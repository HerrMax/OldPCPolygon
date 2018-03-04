using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Item item;
    private string data;
    public GameObject tooltip;

    private void Start()
    {
        //tooltip.SetActive(false);
    }

    private void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
        if (Input.GetKeyDown(KeyCode.Tab)) Deactivate();
    }

    public void Activate(Item item) 
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        if (item.RateOfFire == 0)
        {
            data = "<b>" + item.Title + "</b>\n\n" + item.Description;
        }
        else
        {
            data = "<b>" + item.Title + "</b>\n\n" + item.Description + "\n\n<b>Stats:</b>\n\nDamage:\t\t\t\t" + item.Damage + "\nRange:\t\t\t\t\t" + item.Range + "\nRate of Fire:\t\t\t" + item.RateOfFire + "\nMagazine Size:\t" + item.MagSize;
        }
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
