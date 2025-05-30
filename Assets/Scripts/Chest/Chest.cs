using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform spawnPoint;
    private bool isOpened = false;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        if (isOpened) return;

        isOpened = true;

        animator.enabled = true;

        SpawnRandomItem();
    }

    private void SpawnRandomItem()
    {
        if (itemPrefabs.Length == 0 || spawnPoint == null) return;

        int randomIndex = Random.Range(0, itemPrefabs.Length);
        GameObject selectedItemPrefab = itemPrefabs[randomIndex];

        Instantiate(selectedItemPrefab, spawnPoint.position, Quaternion.identity);

        Debug.Log($"Spawned: {selectedItemPrefab.name}");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WaitForOpen());
        }
    }

    private IEnumerator WaitForOpen()
    {
        while (!isOpened)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenChest();
            }
            yield return null;
        }
    }
}

