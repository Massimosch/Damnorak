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
            //where are we
            Vector2 Location = PlayerSettings.currentRoom.Location;

            //where are we going
            Location += new Vector2(-1, 0);

            if(LevelSettings.rooms.Exists(x => x.Location == Location))
            {
                Room R = LevelSettings.rooms.First(x => x.Location == Location);
                //disable the room that you were in
                Rooms.Find(PlayerSettings.currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
                //find the new room and activate it
                GameObject NewRoom = Rooms.Find(R.roomNumber.ToString()).gameObject;
                NewRoom.SetActive(true);
                //move the player
                PlayerSettings.Controller.enabled = false;
                PlayerSettings.transform.position = NewRoom.transform.Find("Doors").transform.Find("RightDoor").position + new Vector3(-RoomSpawnOffSet, 0, 0);
                PlayerSettings.Controller.enabled = true;

                ChangeRoomIcon(PlayerSettings.currentRoom, R);
                PlayerSettings.currentRoom = R;
                EnableDoors(R);
            }
        }

        if(hit.gameObject.name == "RightDoor")
        {
            //where are we
            Vector2 Location = PlayerSettings.currentRoom.Location;

            //where are we going
            Location += new Vector2(1, 0);

            if(LevelSettings.rooms.Exists(x => x.Location == Location))
            {
                Room R = LevelSettings.rooms.First(x => x.Location == Location);
                //disable the room that you were in
                Rooms.Find(PlayerSettings.currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
                //find the new room and activate it
                GameObject NewRoom = Rooms.Find(R.roomNumber.ToString()).gameObject;
                NewRoom.SetActive(true);
                //move the player
                PlayerSettings.Controller.enabled = false;
                PlayerSettings.transform.position = NewRoom.transform.Find("Doors").transform.Find("LeftDoor").position + new Vector3(RoomSpawnOffSet, 0, 0);
                PlayerSettings.Controller.enabled = true;

                ChangeRoomIcon(PlayerSettings.currentRoom, R);
                PlayerSettings.currentRoom = R;
                EnableDoors(R);
            }
        }

        if(hit.gameObject.name == "TopDoor")
        {
            //where are we
            Vector2 Location = PlayerSettings.currentRoom.Location;

            //where are we going
            Location += new Vector2(0, 1);

            if(LevelSettings.rooms.Exists(x => x.Location == Location))
            {
                Room R = LevelSettings.rooms.First(x => x.Location == Location);
                //disable the room that you were in
                Rooms.Find(PlayerSettings.currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
                //find the new room and activate it
                GameObject NewRoom = Rooms.Find(R.roomNumber.ToString()).gameObject;
                NewRoom.SetActive(true);
                //move the player
                PlayerSettings.Controller.enabled = false;
                PlayerSettings.transform.position = NewRoom.transform.Find("Doors").transform.Find("BottomDoor").position + new Vector3(0, 0, RoomSpawnOffSet);
                PlayerSettings.Controller.enabled = true;

                ChangeRoomIcon(PlayerSettings.currentRoom, R);
                PlayerSettings.currentRoom = R;
                EnableDoors(R);
            }
        }

        if(hit.gameObject.name == "BottomDoor")
        {
            //where are we
            Vector2 Location = PlayerSettings.currentRoom.Location;

            //where are we going
            Location += new Vector2(0, -1);

            if(LevelSettings.rooms.Exists(x => x.Location == Location))
            {
                Room R = LevelSettings.rooms.First(x => x.Location == Location);
                //disable the room that you were in
                Rooms.Find(PlayerSettings.currentRoom.roomNumber.ToString()).gameObject.SetActive(false);
                //find the new room and activate it
                GameObject NewRoom = Rooms.Find(R.roomNumber.ToString()).gameObject;
                NewRoom.SetActive(true);
                //move the player
                PlayerSettings.Controller.enabled = false;
                PlayerSettings.transform.position = NewRoom.transform.Find("Doors").transform.Find("TopDoor").position + new Vector3(0, 0, -RoomSpawnOffSet);
                PlayerSettings.Controller.enabled = true;

                ChangeRoomIcon(PlayerSettings.currentRoom, R);
                PlayerSettings.currentRoom = R;
                EnableDoors(R);
            }
        }



    }
}
