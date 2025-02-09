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

    void EnableDoors(Room R)
    {
        Transform T = Rooms.Find(R.roomNumber.ToString());
        Transform Doors = T.Find("Doors");

        for(int i = 0; i < Doors.childCount; i++)
        {
            Doors.GetChild(i).gameObject.SetActive(false);
        }

        //Chek what doors should be active

        //Left
        {
            Vector2 NewPosition = R.Location + new Vector2(-1, 0);
            if(LevelSettings.rooms.Exists(x => x.Location == NewPosition))
            {
                Doors.Find("LeftDoor").gameObject.SetActive(true);
            }
        }
        //Up
        {
            Vector2 NewPosition = R.Location + new Vector2(0, 1);
            if(LevelSettings.rooms.Exists(x => x.Location == NewPosition))
            {
                Doors.Find("TopDoor").gameObject.SetActive(true);
            }
        }
        //Down
        {
            Vector2 NewPosition = R.Location + new Vector2(0, -1);
            if(LevelSettings.rooms.Exists(x => x.Location == NewPosition))
            {
                Doors.Find("BottomDoor").gameObject.SetActive(true);
            }
        }
        //Right
        {
            Vector2 NewPosition = R.Location + new Vector2(1, 0);
            if(LevelSettings.rooms.Exists(x => x.Location == NewPosition))
            {
                Doors.Find("RightDoor").gameObject.SetActive(true);
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (changeRoomCooldown || hit.gameObject.layer == LayerMask.NameToLayer("Floor"))
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

            PlayerSettings.currentRoom.roomSprite = LevelSettings.CurrentRoomIcon;
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

    void CheckDoor(Vector2 NewLocation, string Direction, Vector3 RoomOffset)
    {
        //where are we
        Vector2 Location = PlayerSettings.currentRoom.Location;

        //where are we going
        Location += NewLocation;

        if (LevelSettings.rooms.Exists(x => x.Location == Location))
        {
            Room R = LevelSettings.rooms.First(x => x.Location == Location);
            //disable the room that you were in
            Rooms.Find(PlayerSettings.currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
            //find the new room and activate it
            GameObject NewRoom = Rooms.Find(R.roomNumber.ToString()).gameObject;
            NewRoom.SetActive(true);
            //move the player
            PlayerSettings.Controller.enabled = false;
            PlayerSettings.transform.position = NewRoom.transform.Find("Doors").transform.Find(Direction).position + RoomOffset;
            PlayerSettings.Controller.enabled = true;

            ChangeRoomIcon(PlayerSettings.currentRoom, R);
            PlayerSettings.currentRoom = R;
            EnableDoors(R);

            PlayerSettings.currentRoom.exploredRoom = true;

            RevealRooms(R);
            ReDrawRevealedRooms();
        }
    }
}
