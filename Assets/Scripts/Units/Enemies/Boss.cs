using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Specifics")]
    [SerializeField] private string bossName = "Boss";
    [SerializeField] private BossHealthUI bossHealthUI;

    protected override void Start()
    {
        base.Start();

        // Find UI properly and hide it initially
        if (bossHealthUI == null)
        {
            bossHealthUI = FindFirstObjectByType<BossHealthUI>();
        }

        if (bossHealthUI != null)
        {
            bossHealthUI.gameObject.SetActive(false);
        }
    }

    public void OnBossAggro()
    {
        if (bossHealthUI != null)
        {
            bossHealthUI.SetupUI(this, bossName);
            Debug.Log($"{bossName} has entered the battle!");
        }
        else
        {
            Debug.LogError("BossHealthUI not found!");
        }
    }

    public override void Die()
    {
        base.Die();

        if (bossHealthUI != null)
        {
            bossHealthUI.gameObject.SetActive(false);
        }

        Debug.Log($"{bossName} defeated!");
    }
}
