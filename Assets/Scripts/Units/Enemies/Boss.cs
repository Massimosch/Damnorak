using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Specifics")]
    [SerializeField] private string bossName = "Tormented Soul of Jailer";
    private BossHealthUI bossHealthUI;

    protected override void Start()
    {
        base.Start();

        // Find UI but don't activate it
        bossHealthUI = FindFirstObjectByType<BossHealthUI>();
        if (bossHealthUI != null)
        {
            bossHealthUI.gameObject.SetActive(false);  // Ensure UI starts hidden
        }
    }

    // Enable UI when aggroed
    public void OnBossAggro()
    {
        if (bossHealthUI != null)
        {
            bossHealthUI.SetupUI(this, bossName);
            bossHealthUI.gameObject.SetActive(true);  // Activate UI on aggro
            Debug.Log($"{bossName} has entered the battle!");
        }
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Boss Defeated!");

        // Hide UI when boss dies
        if (bossHealthUI != null)
        {
            bossHealthUI.gameObject.SetActive(false);
        }
    }
}
