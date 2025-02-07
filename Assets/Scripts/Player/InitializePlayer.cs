using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    public Camera playerCamera;
    public Animator PlayerAnimator;
    public GameObject PlayerAttack;
    public GameObject PlayerAttackExplosion;
    public Transform PlayerTransform;
    public CharacterController PlayerController;

    void Start()
    {
        PlayerSettings.playerCamera = playerCamera;
        PlayerSettings.animator = PlayerAnimator;
        PlayerSettings.Attack = PlayerAttack;
        PlayerSettings.AttackExplosion = PlayerAttackExplosion;
        PlayerSettings.transform = PlayerTransform;
        PlayerSettings.Controller = PlayerController;
    }

}
