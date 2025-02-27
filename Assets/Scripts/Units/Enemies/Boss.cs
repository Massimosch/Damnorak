using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Specifics")]
    [SerializeField] private string bossName = "Boss";
    [SerializeField] private BossHealthUI bossHealthUI;

    [Header("Portal Activation")]
    [SerializeField] private GameObject portalObject;  // Assign an existing portal in the scene

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

        // Ensure the portal is initially deactivated
        if (portalObject != null)
        {
            portalObject.SetActive(false);
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

        // Activate the portal instead of instantiating it
        ActivatePortal();
    }

    private void ActivatePortal()
    {
        if (portalObject != null)
        {
            portalObject.SetActive(true);
            Debug.Log("Portal activated!");
        }
        else
        {
            Debug.LogError("Portal object not assigned!");
        }
    }
}
