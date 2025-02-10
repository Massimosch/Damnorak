using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    public Camera playerCamera;
    public Animator PlayerAnimator;
    public GameObject PlayerAttack;
    public Transform PlayerTransform;
    public CharacterController PlayerController;
    public GameObject AttackExplosion;
    public GameObject SecretRoomExplosion;

    void Start()
    {
        PlayerSettings.playerCamera = playerCamera;
        PlayerSettings.animator = PlayerAnimator;
        PlayerSettings.Attack = PlayerAttack;
        PlayerSettings.transform = PlayerTransform;
        PlayerSettings.Controller = PlayerController;
        PlayerSettings.AttackExplosion = AttackExplosion;
        LevelSettings.SecretRoomExplosion = SecretRoomExplosion;
    }

}
