using System.Linq;
using Unity.Collections;
using UnityEngine;

public class ChangeRooms : MonoBehaviour
{
    public Transform Rooms;
    public float RoomSpawnOffSet = 10;
    private Sprite previousImage;
    void Start()
    {
        previousImage = LevelSettings.DefaultRoomIcon;
        EnableDoors(PlayerSettings.currentRoom);
    }

    void ChangeRoomIcon(Room CurrentRoom, Room NewRoom)
    {
        CurrentRoom.roomImage.sprite = previousImage;
        previousImage = NewRoom.roomImage.sprite;
        NewRoom.roomImage.sprite = LevelSettings.CurrentRoomIcon;
    }

    bool changeRoomCooldown = false;

    void EndChangeRoomCooldown()
    {
        changeRoomCooldown = false;
    }

    public void EnableDoors(Room R)
    {
        Transform T = Rooms.Find(R.roomNumber.ToString());
        Transform Doors = T.Find("Doors");

        for (int i = 0; i < Doors.childCount; i++)
        {
            Doors.GetChild(i).gameObject.SetActive(false);
            if (Doors.GetChild(i).TryGetComponent(out Animator animator))
            {
                animator.SetBool("Open", false);
            }
        }

        
        if (PlayerSettings.currentRoom.Cleared)
        {
            Debug.Log($"Enemy count in room {R.roomNumber}: {LevelSettings.EnemyCount}");
            Debug.Log($"Room Cleared Status: {PlayerSettings.currentRoom.Cleared}");

            OpenDoorIfExists(R.Location + new Vector2(-1, 0), "LeftDoor", Doors);
            OpenDoorIfExists(R.Location + new Vector2(0, 1), "TopDoor", Doors);
            OpenDoorIfExists(R.Location + new Vector2(0, -1), "BottomDoor", Doors);
            OpenDoorIfExists(R.Location + new Vector2(1, 0), "RightDoor", Doors);
        }
    }

    void OpenDoorIfExists(Vector2 position, string doorName, Transform doors)
    {
        if (LevelSettings.rooms.Exists(x => x.Location == position))
        {
            Transform door = doors.Find(doorName);
            if (door != null)
            {
                door.gameObject.SetActive(true);
                if (door.TryGetComponent(out Animator animator))
                {
                    animator.SetBool("Open", true); // Only open if cleared
                }
            }
        }
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (changeRoomCooldown || hit.gameObject.layer == LayerMask.NameToLayer("Floor") || PlayerSettings.currentRoom.Cleared != true)
        {
            return;
        }
        else
        {
            changeRoomCooldown = true;
            Invoke(nameof(EndChangeRoomCooldown), LevelSettings.RoomChangeTimer);
        }

        if(hit.gameObject.name == "LeftDoor")
        {
            CheckDoor(new Vector2(-1, 0), "RightDoor", new Vector3(-RoomSpawnOffSet, 0, 0));
        }

        if (hit.gameObject.name == "RightDoor")
        {
            CheckDoor(new Vector2(1, 0), "LeftDoor", new Vector3(RoomSpawnOffSet, 0, 0));
        }

        if(hit.gameObject.name == "TopDoor")
        {
            CheckDoor(new Vector2(0, 1), "BottomDoor", new Vector3(0, 0, RoomSpawnOffSet));
        }

        if(hit.gameObject.name == "BottomDoor")
        {
            CheckDoor(new Vector2(0, -1), "TopDoor", new Vector3(0, 0, -RoomSpawnOffSet));
        }
    }

    public static void ReDrawRevealedRooms()
    {
        foreach (Room room in LevelSettings.rooms)
        {
            if(!room.revealedRoom && !room.exploredRoom)
            {
                room.roomImage.color = new Color(1, 1, 1, 0);
            }
            if(room.revealedRoom && !room.exploredRoom && room.roomNumber > 5)
            {
                room.roomImage.sprite = LevelSettings.UnexploredRoomIcon;
            }
            if(room.exploredRoom && room.roomNumber > 5)
            {
                room.roomImage.sprite = LevelSettings.DefaultRoomIcon;
            }
            if(room.exploredRoom || room.revealedRoom)
            {
                room.roomImage.color = new Color(1, 1, 1, 1);
            }

            PlayerSettings.currentRoom.roomImage.sprite = LevelSettings.CurrentRoomIcon;
        }
    }

    public static void RevealRooms(Room R)
    {
        foreach(Room room in LevelSettings.rooms)
        {
            if(room.Location == R.Location + new Vector2(-1,0))
            {
                room.revealedRoom = true;
            }

            if(room.Location == R.Location + new Vector2(1,0))
            {
                room.revealedRoom = true;
            }

            if(room.Location == R.Location + new Vector2(0,1))
            {
                room.revealedRoom = true;
            }

            if(room.Location == R.Location + new Vector2(0,-1))
            {
                room.revealedRoom = true;
            }
        }
    }

    public void CheckDoor(Vector2 NewLocation, string Direction, Vector3 RoomOffset)
    {
        Vector2 Location = PlayerSettings.currentRoom.Location;
        Location += NewLocation;

        if (LevelSettings.rooms.Exists(x => x.Location == Location))
        {
            Room R = LevelSettings.rooms.First(x => x.Location == Location);

            // Disable old room and activate new one
            Rooms.Find(PlayerSettings.currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
            GameObject NewRoom = Rooms.Find(R.roomNumber.ToString()).gameObject;
            NewRoom.SetActive(true);

            PlayerSettings.Controller.enabled = false;
            Transform doorTransform = NewRoom.transform.Find($"Doors/{Direction}");

            if (doorTransform != null)
            {
                PlayerSettings.transform.position = doorTransform.position + RoomOffset;
            }

            PlayerSettings.Controller.enabled = true;

            // Update room and UI elements
            ChangeRoomIcon(PlayerSettings.currentRoom, R);
            PlayerSettings.currentRoom = R;
            EnableDoors(R);
            PlayerSettings.currentRoom.exploredRoom = true;

            // Reveal nearby rooms and handle enemies
            RevealRooms(R);
            ReDrawRevealedRooms();

            Transform Enemies = NewRoom.transform.Find("Enemies");
            LevelSettings.EnemyCount = (Enemies != null) ? Enemies.childCount : 0;
            PlayerSettings.currentRoom.Cleared = (LevelSettings.EnemyCount == 0);
            if (Enemies != null)
            {
                Debug.Log($"Found Enemies object in {NewRoom.name}. Child count: {Enemies.childCount}");
                foreach (Transform enemy in Enemies)
                {
                    Debug.Log($"Enemy: {enemy.name}, Active: {enemy.gameObject.activeInHierarchy}");
                }
            }
            else
            {
                Debug.Log("Enemies object not found!");
            }



            // Update door animations based on enemy count
            EnableDoorAnimations(NewRoom.transform.Find("Doors"));
        }
    }

    public void EnableDoorAnimations(Transform doors)
    {
        string[] doorNames = { "LeftDoor", "RightDoor", "TopDoor", "BottomDoor" };

        foreach (string doorName in doorNames)
        {
            Transform door = doors.Find(doorName);
            if (door != null && door.TryGetComponent(out Animator animator))
            {
                animator.SetBool("Open", PlayerSettings.currentRoom.Cleared);
            }
        }
    }

}
