using UnityEngine;

[CreateAssetMenu(fileName = "New Crystal", menuName = "New Item/Crystal")]
public class CrystalItem : Item
{
    public int value = 1;

    public override void Use()
    {
        base.Use();
        Debug.Log($"Collected {value} crystals!");
    }
}

