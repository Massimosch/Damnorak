using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Specifics")]
    //[SerializeField] private GameObject portalPrefab;
    //[SerializeField] private Transform portalSpawnPoint;
    [SerializeField] private string bossName = "Tormented Soul of Jailer";
    private BossHealthUI bossHealthUI;

    protected override void Start()
    {
        base.Start();

        bossHealthUI = FindFirstObjectByType<BossHealthUI>();
        if (bossHealthUI != null)
        {
            bossHealthUI.SetupUI(this, bossName);
        }
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Boss Defeated!");
        /*
        if (portalPrefab != null && portalSpawnPoint != null)
        {
            Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
        }
        */
    }
}

