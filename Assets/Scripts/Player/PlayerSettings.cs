using UnityEngine;

public static class PlayerSettings
{
    public static string State = "Idle";
    public static bool Invincible = false;
    public static Transform transform;
    public static Animator animator;
    public static GameObject PlayerStaff;


    public static int Keys = 3;
    public static int Crystals = 5;
    public static float Speed = 0.1f;
    public static float Health = 3f;
    public static float MaxHealth = 3f;
    public static float AttackSpeed = 30;

    public static GameObject Attack;
    public static GameObject AttackExplosion;

    public static Camera playerCamera;

    public static Room currentRoom;
    public static CharacterController Controller;

    public static GameObject HeartPanel;
    public static GameObject DamagePanel;
    public static Sprite FullHeart;
    public static Sprite HalfHeart;
    public static Sprite EmptyHeart;
    public static RuntimeAnimatorController HeartAnimator;

}
