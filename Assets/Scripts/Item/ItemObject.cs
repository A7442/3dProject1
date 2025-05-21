using UnityEngine;

public interface IInteractable
{
    public string GetInteractPrompt();
    public void OnInteracting();
}

public class ItemObject : MonoBehaviour,IInteractable
{
    public ItemData data;
    
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteracting()//상호작용을 했을 때(E키를 누른 후 실행)
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        //여기에 먹지 못하면 못먹는 것 넣어줘도 될듯
        Destroy(gameObject);
    }
}
