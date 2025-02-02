using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Camera cam;

    public float moveSpeed = 0.1f;
    public float camSpeed = 150f;
    public float rotationSpeed = 50f;
    public float horizontalAxis, verticalAxis;

    void Update()
    {
        PlayerMoving(horizontalAxis, verticalAxis);

        PlayerRotating(horizontalAxis, verticalAxis);
    }

    void LateUpdate()
    {
        CamMovement();
    }


    void PlayerMoving(float horizontalAxis, float verticalAxis)
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }
    }

    void PlayerRotating(float horizontalAxis, float verticalAxis)
    {
        Vector3 Move = new Vector3(horizontalAxis, 0, verticalAxis) * moveSpeed;
        controller.Move(Move);

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDistance;
        if (playerPlane.Raycast(ray, out hitDistance))
        {
            Vector3 point = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(point - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void CamMovement()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 69, 0); //Cam start Pos
        Vector3 updatedPosition = Vector3.MoveTowards(cam.transform.position, startPosition, camSpeed * Time.deltaTime); //Cam Follow Speed
        cam.transform.position = updatedPosition;
    }
}
