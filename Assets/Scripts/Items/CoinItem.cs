using UnityEngine;

[CreateAssetMenu(fileName = "New Coin", menuName = "New Item/Coin")]
public class CoinItem : Item
{
    public int value = 1;

    public override void Use()
    {
        base.Use();
        Debug.Log($"Collected {value} coins!");
        // Here you can add coin logic like PlayerSettings.Coins += value;
    }
}
