using System.Collections;
using UnityEngine;


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
    
    
    public void UseItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index];
        selectedItemIndex = index;
        switch (selectedItem.item.type)
        {
            case ItemType.Health :
                condition.Heal(selectedItem.item.value);
                break;
            case ItemType.Speed :
                float plusSpeed = selectedItem.item.value;
                StartCoroutine(SpeedBuff(plusSpeed));
                break;
            case ItemType.Jump :
                float plusJump = selectedItem.item.value;
                StartCoroutine(JumpBuff(plusJump));
                break;
        }
        slots[selectedItemIndex].Clear();
        ClearSelectedItemWindow();
    }
    IEnumerator SpeedBuff(float plusSpeed)
    {
        CharacterManager.Instance.Player.controller.moveSpeed += plusSpeed;
        slots[selectedItemIndex].Clear();
        ClearSelectedItemWindow();
        Debug.Log("스피드 버프 시작");
        yield return new WaitForSeconds(3f);
        CharacterManager.Instance.Player.controller.moveSpeed -= plusSpeed;
        Debug.Log("스피드 버프 종료");
    }
    
    IEnumerator JumpBuff(float plusJump)
    {
        CharacterManager.Instance.Player.controller.jumpPower += plusJump;
        slots[selectedItemIndex].Clear();
        ClearSelectedItemWindow();

        Debug.Log("점프 버프 시작");
        yield return new WaitForSeconds(3f);
        CharacterManager.Instance.Player.controller.jumpPower -= plusJump;
        Debug.Log("점프 버프 종료");
    }
    //이제 와서 바꾸기 뭐하지만 나중에는 버프를 따로 만들고
    //그 안에서 버프를 또 enum을 활용하여 구분하기
}
