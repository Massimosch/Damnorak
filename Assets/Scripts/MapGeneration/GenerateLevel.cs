using UnityEngine;
using UnityEngine.UI;

public class GenerateLevel : MonoBehaviour
{
    public Sprite currentRoom;
    public Sprite bossRoom;
    public Sprite emptyRoom;
    public Sprite shopRoom;
    public Sprite treasureRoom;
    public Sprite unexploredRoom;

    void Start()
    {
        LevelSettings.DefaultRoomIcon = emptyRoom;
        LevelSettings.BossRoomIcon = bossRoom;
        LevelSettings.CurrentRoomIcon = currentRoom;
        LevelSettings.ShopRoomIcon = shopRoom;
        LevelSettings.TreasureRoomIcon = treasureRoom;
        LevelSettings.UnexploredRoomIcon = unexploredRoom;

        Room StartRoom = new Room();
        StartRoom.Location = new Vector2(0, 0);
        StartRoom.roomImage = LevelSettings.CurrentRoomIcon;


        //Start room
        DrawRoomOnMap(StartRoom);
        //Left
        if(Random.value > 0.5f)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(-1, 0);
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            GenerateRoom(newRoom);
        }
        //Right
        if(Random.value > 0.5f)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(1, 0);
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            GenerateRoom(newRoom);
        }
        //Up
        if(Random.value > 0.5f)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(0, 1);
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            GenerateRoom(newRoom);
        }
        //Down
        if(Random.value > 0.5f)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(0, -1);
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            GenerateRoom(newRoom);
        }
        
    }

    void GenerateRoom(Room room)
    {

    }

    void DrawRoomOnMap(Room room)
    {
        GameObject MapTile = new GameObject("MapTile");
        Image RoomImage = MapTile.AddComponent<Image>();
        RoomImage.sprite = room.roomImage;
        RectTransform rectTransform = RoomImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(LevelSettings.height, LevelSettings.width) * LevelSettings.iconScale;
        rectTransform.position = room.Location * (LevelSettings.iconScale * LevelSettings.height * LevelSettings.scale + (LevelSettings.padding * LevelSettings.height * LevelSettings.scale));
        RoomImage.transform.SetParent(transform, false);
    }
}
