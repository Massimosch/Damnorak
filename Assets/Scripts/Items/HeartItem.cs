using UnityEngine;

[CreateAssetMenu(fileName = "New Heart", menuName = "New Item/Heart")]
public class HeartItem : Item
{
    public float healthRestore = 1f;

    public override void Use()
    {
        base.Use();
        PlayerSettings.Health = Mathf.Min(PlayerSettings.Health + healthRestore, PlayerSettings.MaxHealth);
        HealthScript.DrawHearts();
        Debug.Log($"{itemName} used! Health: {PlayerSettings.Health}/{PlayerSettings.MaxHealth}");
    }
}
