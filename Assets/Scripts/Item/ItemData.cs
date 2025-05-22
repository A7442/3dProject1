using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Health,
    Stamina,
    Speed,
    Jump
}
[CreateAssetMenu(fileName ="Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    public string displayName;
    public string description;
    public ItemType type;
    public float value;
    public Sprite icon;
}
