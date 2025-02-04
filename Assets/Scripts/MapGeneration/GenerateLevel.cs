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
    bool regenerating = false;
    int failSafe = 0;

    void Awake()
    {
        LevelSettings.DefaultRoomIcon = emptyRoom;
        LevelSettings.BossRoomIcon = bossRoom;
        LevelSettings.CurrentRoomIcon = currentRoom;
        LevelSettings.ShopRoomIcon = shopRoom;
        LevelSettings.TreasureRoomIcon = treasureRoom;
        LevelSettings.UnexploredRoomIcon = unexploredRoom;
    }

    void Start()
    {
        Room StartRoom = new Room();
        StartRoom.Location = new Vector2(0, 0);
        StartRoom.roomImage = LevelSettings.CurrentRoomIcon;


        //Start room
        DrawRoomOnMap(StartRoom);
        //Left
        if(Random.value > LevelSettings.roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(-1, 0);
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            if(!CheckIfRoomExists(newRoom.Location))
            {
                if(!CheckIfRoomsAroundGeneratedRoom(newRoom.Location, "Right"))
                {
                    GenerateRoom(newRoom);
                }
            }
        }
        //Right
        if(Random.value > LevelSettings.roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(1, 0);
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            if(!CheckIfRoomExists(newRoom.Location))
            {
                if(!CheckIfRoomsAroundGeneratedRoom(newRoom.Location, "Left"))
                {
                    GenerateRoom(newRoom);
                }
            }
        }
        //Up
        if(Random.value > LevelSettings.roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(0, 1);
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            if(!CheckIfRoomExists(newRoom.Location))
            {
                if(!CheckIfRoomsAroundGeneratedRoom(newRoom.Location, "Down"))
                {
                    GenerateRoom(newRoom);
                }
            }
        }
        //Down
        if(Random.value > LevelSettings.roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(0, -1);
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            if(!CheckIfRoomExists(newRoom.Location))
            {
                if(!CheckIfRoomsAroundGeneratedRoom(newRoom.Location, "Up"))
                {
                    GenerateRoom(newRoom);
                }
            }
        }

        GenerateBossRoom();
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Tab) && !regenerating)
        {
            regenerating = true;
            Invoke(nameof(StopRegenerating), 1);
            for(int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            LevelSettings.rooms.Clear();

            Start();
        }
    }

    void StopRegenerating()
    {
        regenerating = false;
    }

    bool CheckIfRoomExists(Vector2 v)
    {
        return (LevelSettings.rooms.Exists(x => x.Location == v));
    }

    bool CheckIfRoomsAroundGeneratedRoom(Vector2 v, string direction)
    {
        switch(direction)
        {
            case "Right":
            {
                //Check Down left and up
                if(LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x - 1 ,v.y)) ||
                    LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x , v.y -1)) ||
                    LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x ,v.y + 1)))
                    return true;
                break;
            }
            case "Left":
            {
                //Check down right and up
                if(LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x + 1 ,v.y)) ||
                    LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x , v.y -1)) ||
                    LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x ,v.y + 1)))
                    return true;                   
                break;
            }
            case "Up":
            {
                //Check down right and left
                if(LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x - 1 ,v.y)) ||
                    LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x - 1 , v.y)) ||
                    LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x ,v.y - 1)))
                    return true;                    
                break;
            }   
            case "Down":
            {
                //Check up left and right
                if(LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x ,v.y + 1)) ||
                    LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x - 1 , v.y)) ||
                    LevelSettings.rooms.Exists(x => x.Location == new Vector2(v.x - 1 ,v.y)))
                    return true;
                break;
            }             
        }

        return false;
    }

    void GenerateRoom(Room room)
    {
        DrawRoomOnMap(room);
        failSafe++;
        if(failSafe > 50)
        {
            return;
        }

        //Left
        if(Random.value > LevelSettings.roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(-1, 0) + room.Location;
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;

            if(!CheckIfRoomExists(newRoom.Location))
            {
                if(!CheckIfRoomsAroundGeneratedRoom(newRoom.Location, "Right"))
                {
                    if(Mathf.Abs(newRoom.Location.x) < LevelSettings.RoomLimit && Mathf.Abs(newRoom.Location.y) < LevelSettings.RoomLimit)
                        GenerateRoom(newRoom);
                }
            }
        }
        //Right
        if(Random.value > LevelSettings.roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(1, 0) + room.Location;
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            
            if(!CheckIfRoomExists(newRoom.Location))
            {
                if(!CheckIfRoomsAroundGeneratedRoom(newRoom.Location, "Left"))
                {
                    if(Mathf.Abs(newRoom.Location.x) < LevelSettings.RoomLimit && Mathf.Abs(newRoom.Location.y) < LevelSettings.RoomLimit)
                        GenerateRoom(newRoom);
                }
            }
        }
        //Up
        if(Random.value > LevelSettings.roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(0, 1) + room.Location;
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            
            if(!CheckIfRoomExists(newRoom.Location))
            {
                if(!CheckIfRoomsAroundGeneratedRoom(newRoom.Location, "Down"))
                {
                    if(Mathf.Abs(newRoom.Location.x) < LevelSettings.RoomLimit && Mathf.Abs(newRoom.Location.y) < LevelSettings.RoomLimit)
                        GenerateRoom(newRoom);
                }
            }
        }
        //Down
        if(Random.value > LevelSettings.roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(0, -1) + room.Location;
            newRoom.roomImage = LevelSettings.DefaultRoomIcon;
            
            if(!CheckIfRoomExists(newRoom.Location))
            {
                if(!CheckIfRoomsAroundGeneratedRoom(newRoom.Location, "Up"))
                {
                    if(Mathf.Abs(newRoom.Location.x) < LevelSettings.RoomLimit && Mathf.Abs(newRoom.Location.y) < LevelSettings.RoomLimit)
                        GenerateRoom(newRoom);
                }
            }
        }
    }

    void GenerateBossRoom()
    {
        float MaxNumber = 0f;
        Vector2 FurthestRoom = Vector2.zero;

        foreach(Room room in LevelSettings.rooms)
        {
            if(Mathf.Abs(room.Location.x) + Mathf.Abs(room.Location.y) >= MaxNumber)
            {
                MaxNumber = Mathf.Abs(room.Location.x) + Mathf.Abs(room.Location.y);
                FurthestRoom = room.Location;
            }
        }

        Room BossRoom = new Room();
        BossRoom.roomImage = LevelSettings.BossRoomIcon;
        BossRoom.roomNumber = 3;

        //LEFT
        if(!CheckIfRoomExists(FurthestRoom + new Vector2(-1, 0)))
        {
            if(!CheckIfRoomsAroundGeneratedRoom(FurthestRoom + new Vector2(-1, 0), "Right"))
            {
                BossRoom.Location = FurthestRoom + new Vector2(-1, 0);
            }
        }
        //RIGHT
        else if(!CheckIfRoomExists(FurthestRoom + new Vector2(1, 0)))
        {
            if(!CheckIfRoomsAroundGeneratedRoom(FurthestRoom + new Vector2(1, 0), "Left"))
            {
                BossRoom.Location = FurthestRoom + new Vector2(1, 0);
            }
        }
        //UP
        else if(!CheckIfRoomExists(FurthestRoom + new Vector2(0, 1)))
        {
            if(!CheckIfRoomsAroundGeneratedRoom(FurthestRoom + new Vector2(0, 1), "Down"))
            {
                BossRoom.Location = FurthestRoom + new Vector2(0, 1);
            }
        }
        //DOWN
        else if(!CheckIfRoomExists(FurthestRoom + new Vector2(0, -1)))
        {
            if(!CheckIfRoomsAroundGeneratedRoom(FurthestRoom + new Vector2(0, -1), "Up"))
            {
                BossRoom.Location = FurthestRoom + new Vector2(0, -1);
            }
        }

        DrawRoomOnMap(BossRoom);
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

        LevelSettings.rooms.Add(room);
    }
}
