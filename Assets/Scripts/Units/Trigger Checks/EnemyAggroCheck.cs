using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    public GameObject PlayerTarget {get; set;}
    private Unit _enemy;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<Unit>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.SetAggroStatus(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.SetAggroStatus(false);
        }        
    }
}
