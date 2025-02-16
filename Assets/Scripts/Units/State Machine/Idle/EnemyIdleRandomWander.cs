using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]

public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    #region Idle Variables
    public float RandomMovementRange = 5f;
	public float RandomMovementSpeed = 1f;
    private Vector3 _targetPos;
	#endregion


    public override void DoAnimationTriggerEventLogic(Unit.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        enemy.target = enemy.waypoints[0];
    }



    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (enemy.waypoints.Length == 0)
        {
            Debug.LogError("Waypoints missing!");
            return;
        }

        float distance = (enemy.transform.position - enemy.target.position).sqrMagnitude;

        if (distance < 1f) // 🔥 Jos saavutettiin waypoint
        {
            enemy.StartCoroutine(WaitAndChangeWaypoint()); // 🔥 Käynnistä viiveen sisältävä coroutine
        }
    }



    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        Vector3 direction = (_targetPos - enemy.transform.position).normalized;
        enemy.transform.position += direction * RandomMovementSpeed * Time.deltaTime;
    }

    public override void Initialize(GameObject gameObject, Unit enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private IEnumerator WaitAndChangeWaypoint()
    {
        Debug.Log("Waypoint saavutettu, odotetaan 1s ennen vaihtoa...");
        yield return new WaitForSeconds(1f); // 🔥 Odotetaan 1 sekunti ennen vaihtoa

        int previousWaypoint = enemy.currentWaypointIndex;

        // 🔥 Valitaan uusi satunnainen waypoint
        if (enemy.waypoints.Length > 1)
        {
            do
            {
                enemy.currentWaypointIndex = Random.Range(0, enemy.waypoints.Length);
            } while (enemy.currentWaypointIndex == previousWaypoint); // 🔥 Varmistetaan, ettei valita samaa waypointia
        }

        // 🔥 Varmistetaan, että uusi target päivitetään
        enemy.target = enemy.waypoints[enemy.currentWaypointIndex];

        Debug.Log($"Uusi Waypoint valittu: {previousWaypoint} → {enemy.currentWaypointIndex} ({enemy.target.name})");

        // 🔥 Pakotetaan reitin päivitys heti uuden waypointin mukaisesti
        enemy.StartCoroutine(enemy.UpdatePath());
    }
}
