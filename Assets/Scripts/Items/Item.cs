using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int maxStack = 99;

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }
}
