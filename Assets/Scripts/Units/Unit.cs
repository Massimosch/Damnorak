using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed = 20f;
    public float turnSpeed = 3;
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
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        while (followingPath)
        {
            Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
            if (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                }
                else
                {
                    pathIndex++;
                }

                if (followingPath)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                    transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
                }
            }
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
