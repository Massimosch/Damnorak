using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            item.Use();
            Destroy(gameObject);
        }
    }
}
