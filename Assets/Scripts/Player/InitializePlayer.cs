using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    public Camera playerCamera;
    public Animator PlayerAnimator;
    public GameObject PlayerAttack;
    public GameObject PlayerAttackExplosion;

    void Start()
    {
        PlayerSettings.playerCamera = playerCamera;
        PlayerSettings.animator = PlayerAnimator;
        PlayerSettings.Attack = PlayerAttack;
        PlayerSettings.AttackExplosion = PlayerAttackExplosion;
    }

}
