using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    public GameObject PlayerTarget {get; set;}
    private Enemy _enemy;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.SetAggroStatus(true);
        }

        if (_enemy is Boss boss)
        {
            boss.OnBossAggro();
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
