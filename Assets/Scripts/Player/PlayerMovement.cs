using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float moveSpeed = 0.1f;
    public float camSpeed = 150f;
    public float rotationSpeed = 50f;
    public float horizontalAxis;
    public float verticalAxis;


    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        PlayerMoving(horizontalAxis, verticalAxis);
        PlayerRotating();
    }

    void LateUpdate()
    {
        CamMovement();
    }


    void PlayerMoving(float horizontalAxis, float verticalAxis)
    {
        // Luo liikevektori (ei muuta y-koordinaattia)
        Vector3 move = new Vector3(horizontalAxis, 0, verticalAxis) * moveSpeed;

        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            PlayerSettings.animator.SetFloat("Speed", 1);
        }
        else
        {
            PlayerSettings.animator.SetFloat("Speed", 0);
        }

        // Pakotetaan y-koordinaatti pysymään nollassa
        move.y = 0f;

        // Liikuta pelaajaa
        controller.Move(move);
    }



    void PlayerRotating()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (playerPlane.Raycast(ray, out float hitDistance))
        {
            Vector3 point = ray.GetPoint(hitDistance);
            Quaternion targetRotation = Quaternion.LookRotation(point - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void CamMovement()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 69, 0); //Cam start Pos
        Vector3 updatedPosition = Vector3.MoveTowards(PlayerSettings.playerCamera.transform.position, startPosition, camSpeed * Time.deltaTime); //Cam Follow Speed
        PlayerSettings.playerCamera.transform.position = updatedPosition;
    }
}
