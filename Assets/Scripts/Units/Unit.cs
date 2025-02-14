using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed = 20f;
    public float turnDst = 5f;
    
    Path path;


    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(waypoints, transform.position, turnDst);
            StopCoroutine(nameof(FollowPath));
            StartCoroutine(nameof(FollowPath));
        }
    }

    IEnumerator FollowPath()
    {
        while (true)
        {
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}
