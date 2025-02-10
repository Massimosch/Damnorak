using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public static class LevelSettings
{
    public static float height = 500;
    public static float width = 500;
    public static float scale = 1f;
    public static float iconScale = 0.07f;
    public static float padding = 0.01f;

    public static float roomGenerationChance = 0.5f;
    public static int RoomLimit = 5;
    
    public static Sprite TreasureRoomIcon;
    public static Sprite BossRoomIcon;
    public static Sprite ShopRoomIcon;
    public static Sprite UnexploredRoomIcon;
    public static Sprite DefaultRoomIcon;
    public static Sprite CurrentRoomIcon;
    public static Sprite SecretRoomIcon;

    public static GameObject SecretRoomExplosion;

    public static List<Room> rooms = new List<Room>();

    public static float RoomChangeTimer = 1f;

}

public class Room
{
    public int roomNumber = 6;
    public int levelNumber = 0;
    public Vector2 Location;
    public Image roomImage;
    public Sprite roomSprite;
    public bool revealedRoom;
    public bool exploredRoom;
}