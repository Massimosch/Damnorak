using UnityEngine;

[CreateAssetMenu(fileName = "New Heart", menuName = "New Item/Heart")]
public class HeartItem : Item
{
    public float healthRestore = 1f;
    public float increaseHealth = 1f;

    public override void Use()
    {
        base.Use();
        PlayerSettings.MaxHealth += increaseHealth;
        PlayerSettings.Health = Mathf.Min(PlayerSettings.Health + healthRestore, PlayerSettings.MaxHealth);
        HealthScript.DrawHearts();
    }
}
