using UnityEngine;

public static class PlayerSettings
{
    public static string State = "Idle";
    public static Transform transform;
    public static Animator animator;
    public static float Speed = 0.1f;
    public static float AttackSpeed = 30;

    public static GameObject Attack;
    public static GameObject AttackExplosion;

    public static Camera playerCamera;

    public static Room currentRoom;
    public static CharacterController Controller;

}
