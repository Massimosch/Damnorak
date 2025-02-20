using UnityEngine;

public class Boss : Enemy
{
    //[SerializeField] private GameObject portalPrefab;
    //[SerializeField] private Transform portalSpawnPoint;

    public override void Die()
    {
        base.Die();
        Debug.Log("Boss Defeated!");

        /*
        if (portalPrefab != null && portalSpawnPoint != null)
        {
            Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Portal prefab or spawn point not set.");
        }
        */
    }
}
