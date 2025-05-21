using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;
    public UIInventory inventory;
    public Image icon;
    
    public int index;
    
    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
    }

    public void OnClickButton()
    {
        //inventory.SelectItem(index);
    }
}
