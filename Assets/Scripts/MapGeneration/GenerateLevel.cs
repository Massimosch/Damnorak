using System.Collections.Generic;
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
        StartRoom.roomSprite = LevelSettings.CurrentRoomIcon;
        StartRoom.exploredRoom = true;
        StartRoom.revealedRoom = true;
        StartRoom.roomNumber = 0;

        PlayerSettings.currentRoom = StartRoom;


        //Start room
        DrawRoomOnMap(StartRoom);
        //Left
        if(Random.value > LevelSettings.roomGenerationChance)
        {
            Room newRoom = new Room();
            newRoom.Location = new Vector2(-1, 0);
            newRoom.roomSprite = LevelSettings.DefaultRoomIcon;
            newRoom.roomNumber = RandomRoomNumber();
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
            newRoom.roomSprite = LevelSettings.DefaultRoomIcon;
            newRoom.roomNumber = RandomRoomNumber();
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
            newRoom.roomSprite = LevelSettings.DefaultRoomIcon;
            newRoom.roomNumber = RandomRoomNumber();
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
            newRoom.roomSprite = LevelSettings.DefaultRoomIcon;
            newRoom.roomNumber = RandomRoomNumber();
            if(!CheckIfRoomExists(newRoom.Location))
            {
                if(!CheckIfRoomsAroundGeneratedRoom(newRoom.Location, "Up"))
                {
                    GenerateRoom(newRoom);
                }
            }
        }

        GenerateBossRoom();

        bool treasure = GenerateSpecialRoom(LevelSettings.TreasureRoomIcon, 3);
        bool shop = GenerateSpecialRoom(LevelSettings.ShopRoomIcon, 2);

        if(!treasure || !shop)
        {
            Regenerate();
        }
        else
        {
            ChangeRooms.RevealRooms(StartRoom);
            ChangeRooms.ReDrawRevealedRooms();
        }
        
    }

    void Regenerate()
    {
        regenerating = true;
        failSafe = 0;
        Invoke(nameof(StopRegenerating), 1);
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        LevelSettings.rooms.Clear();
        Start();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Tab) && !regenerating)
        {
            Regenerate();
        }

        if(Input.GetKey(KeyCode.P) && !regenerating)
        {
            regenerating = true;
            Invoke(nameof(StopRegenerating), 1);

            string log = "Room List: \n-----------------------\n";

            foreach(Room room in LevelSettings.rooms)
            {
                log += "Room num: " + room.roomNumber + " Location: " + room.Location + "\n";
            }
            Debug.Log(log);
        }
    }

    int RandomRoomNumber()
    {
        return Random.Range(6, GameObject.Find("Rooms").transform.childCount);
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
            newRoom.roomSprite = LevelSettings.DefaultRoomIcon;
            newRoom.roomNumber = RandomRoomNumber();

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
            newRoom.roomSprite = LevelSettings.DefaultRoomIcon;
            
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
            newRoom.roomSprite = LevelSettings.DefaultRoomIcon;
            
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
            newRoom.roomSprite = LevelSettings.DefaultRoomIcon;
            
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

        foreach (Room room in LevelSettings.rooms)
        {
            float distance = Mathf.Abs(room.Location.x) + Mathf.Abs(room.Location.y);
            if (distance > MaxNumber && room.Location != new Vector2(0, 0)) // Vältetään StartRoom
            {
                MaxNumber = distance;
                FurthestRoom = room.Location;
            }
        }

        Room BossRoom = new Room();
        BossRoom.roomSprite = LevelSettings.BossRoomIcon;
        BossRoom.roomNumber = 1;

        Vector2[] directions = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, -1) };

        foreach (Vector2 dir in directions)
        {
            Vector2 newLocation = FurthestRoom + dir;
            if (!CheckIfRoomExists(newLocation) && !CheckIfRoomsAroundGeneratedRoom(newLocation, dir.ToString()))
            {
                BossRoom.Location = newLocation;
                DrawRoomOnMap(BossRoom);
                return;
            }
        }

        Debug.LogWarning("No valid location found for BossRoom, placing it at an available random spot.");
        
        foreach (Room room in LevelSettings.rooms)
        {
            foreach (Vector2 dir in directions)
            {
                Vector2 randomLocation = room.Location + dir;
                if (!CheckIfRoomExists(randomLocation))
                {
                    BossRoom.Location = randomLocation;
                    DrawRoomOnMap(BossRoom);
                    return;
                }
            }
        }
    }


    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    bool GenerateSpecialRoom(Sprite MapIcon, int RoomNumber)
    {
        List<Room> ShuffledList = new List<Room>(LevelSettings.rooms);

        Room SpecialRoom = new Room();
        SpecialRoom.roomSprite = MapIcon;
        SpecialRoom.roomNumber = RoomNumber;
        bool FoundAvailableLocation = false;

            foreach(Room room in ShuffledList)
            {
                Vector2 SpecialRoomLocation = room.Location;
                
                if(room.roomNumber < 5) continue;

                //LEFT
                if(!CheckIfRoomExists(SpecialRoomLocation + new Vector2(-1, 0)))
                {
                    if(!CheckIfRoomsAroundGeneratedRoom(SpecialRoomLocation + new Vector2(-1, 0), "Right"))
                    {
                        SpecialRoom.Location = SpecialRoomLocation + new Vector2(-1, 0);
                        FoundAvailableLocation = true;
                    }
                }
                //RIGHT
                else if(!CheckIfRoomExists(SpecialRoomLocation + new Vector2(1, 0)))
                {
                    if(!CheckIfRoomsAroundGeneratedRoom(SpecialRoomLocation + new Vector2(1, 0), "Left"))
                    {
                        SpecialRoom.Location = SpecialRoomLocation + new Vector2(1, 0);
                        FoundAvailableLocation = true;
                    }
                }
                //UP
                else if(!CheckIfRoomExists(SpecialRoomLocation + new Vector2(0, 1)))
                {
                    if(!CheckIfRoomsAroundGeneratedRoom(SpecialRoomLocation + new Vector2(0, 1), "Down"))
                    {
                        SpecialRoom.Location = SpecialRoomLocation + new Vector2(0, 1);
                        FoundAvailableLocation = true;
                    }
                }
                //DOWN
                else if(!CheckIfRoomExists(SpecialRoomLocation + new Vector2(0, -1)))
                {
                    if(!CheckIfRoomsAroundGeneratedRoom(SpecialRoomLocation + new Vector2(0, -1), "Up"))
                    {
                        SpecialRoom.Location = SpecialRoomLocation + new Vector2(0, -1);
                        FoundAvailableLocation = true;
                    }
                }

            if(FoundAvailableLocation)
            {
                DrawRoomOnMap(SpecialRoom);
                return true;
            }
        }

        return false;
    }

    void DrawRoomOnMap(Room room)
    {
        string TileName = "MapTile";
        if(room.roomNumber == 1) TileName = "BossRoomTile";
        if(room.roomNumber == 2) TileName = "ShopRoomTile";
        if(room.roomNumber == 3) TileName = "ItemRoomTile";
        GameObject MapTile = new GameObject(TileName);
        Image RoomImage = MapTile.AddComponent<Image>();
        RoomImage.sprite = room.roomSprite;
        room.roomImage = RoomImage;
        RectTransform rectTransform = RoomImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(LevelSettings.height, LevelSettings.width) * LevelSettings.iconScale;
        rectTransform.position = room.Location * (LevelSettings.iconScale * LevelSettings.height * LevelSettings.scale + (LevelSettings.padding * LevelSettings.height * LevelSettings.scale));
        RoomImage.transform.SetParent(transform, false);

        LevelSettings.rooms.Add(room);
        Debug.Log("Drawing Room: " + room.roomNumber + " at location " + room.Location);
    }
}
