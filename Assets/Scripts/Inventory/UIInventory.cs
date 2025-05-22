using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public Transform slotPanel;
    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;

    private int curEquipIndex;

    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        CharacterManager.Instance.Player.addItem += AddItem;
        Debug.Log("시작 더하기");
        slots = new ItemSlot[2];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
            slots[i].Clear();
        }
        ClearSelectedItemWindow();
    }

	void ClearSelectedItemWindow()
    {
        selectedItem = null;
    }

    public void AddItem()
    {
        Debug.Log("아이템 더하기");
        ItemData data = CharacterManager.Instance.Player.itemData;
        
        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = data;
            UpdateUI();
        }
        //else문으로 null일 때 ItemObject에 Destroy를 막던가 드롭 포지션을 만들어서 다시 떨구든가 해야함
        CharacterManager.Instance.Player.itemData = null;
    }

    public void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data)
            {
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }
    
    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index];
        selectedItemIndex = index;
    }
}
