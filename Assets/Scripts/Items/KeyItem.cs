using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "New Item/Key")]
public class KeyItem : Item
{
    public override void Use()
    {
        base.Use();
        Debug.Log($"{itemName} collected! Used to open doors.");
        PlayerSettings.Keys++;
    }
}

