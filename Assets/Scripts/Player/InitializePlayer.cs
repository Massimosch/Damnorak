using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    public Camera playerCamera;
    public Animator PlayerAnimator;
    public GameObject PlayerAttack;
    public Transform PlayerTransform;
    public CharacterController PlayerController;
    public GameObject AttackExplosion;

    void Start()
    {
        PlayerSettings.playerCamera = playerCamera;
        PlayerSettings.animator = PlayerAnimator;
        PlayerSettings.Attack = PlayerAttack;
        PlayerSettings.transform = PlayerTransform;
        PlayerSettings.Controller = PlayerController;
        PlayerSettings.AttackExplosion = AttackExplosion;
    }

}
