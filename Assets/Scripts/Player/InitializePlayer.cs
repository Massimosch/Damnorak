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
    //public GameObject PlayerStaff;
    public Sprite FullHeart;
    public Sprite HalfHeart;
    public Sprite EmptyHeart;
    public GameObject HealthPanel;
    public GameObject DamagePanel;
    public RuntimeAnimatorController HeartAnimatorController;
    public GameObject GameOverPanel;

    void Start()
    {
        PlayerSettings.playerCamera = playerCamera;
        PlayerSettings.animator = PlayerAnimator;
        PlayerSettings.Attack = PlayerAttack;
        PlayerSettings.transform = PlayerTransform;
        PlayerSettings.Controller = PlayerController;
        PlayerSettings.AttackExplosion = AttackExplosion;
        PlayerSettings.FullHeart = FullHeart;
        PlayerSettings.EmptyHeart = EmptyHeart;
        PlayerSettings.HalfHeart = HalfHeart;
        PlayerSettings.DamagePanel = DamagePanel;
        PlayerSettings.GameOverPanel = GameOverPanel;
        PlayerSettings.HeartPanel = HealthPanel;
        PlayerSettings.HeartAnimator = HeartAnimatorController;


        LevelSettings.SecretRoomExplosion = SecretRoomExplosion;
    }
}
