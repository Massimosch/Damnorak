using UnityEngine;

public class EnemyStrikingDistanceCheck : MonoBehaviour
{
    public GameObject PlayerTarget {get; set;}
    private Unit _enemy;

    void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<Unit>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.SetStrikingDistanceBool(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.SetStrikingDistanceBool(false);
        }
    }
}
