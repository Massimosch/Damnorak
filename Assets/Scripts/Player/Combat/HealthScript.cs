using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{
    public Sprite FullHeart;
    public Sprite HalfHeart;
    public Sprite EmptyHeart;
    public GameObject Panel;

    void Start()
    {
        DrawHearts();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.I))
        {
            TakeDamage(1);
        }

        if(Input.GetKey(KeyCode.O))
        {
            TakeDamage(2);
        }  

        if(Input.GetKey(KeyCode.P))
        {
            TakeDamage(3);
        }  

        if(Input.GetKey(KeyCode.H))
        {
            PlayerSettings.Health = PlayerSettings.MaxHealth;
            DrawHearts();
        }     
    }

    public static IEnumerator WaitAndReDrawHearts()
    {
        PlayerSettings.DamagePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PlayerSettings.DamagePanel.SetActive(false);

        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < PlayerSettings.HeartPanel.transform.childCount; ++i)
        {
            Destroy(PlayerSettings.HeartPanel.transform.GetChild(i).gameObject);
        }
        DrawHearts();
    }

    public static IEnumerator Uninvincible()
    {
        yield return new WaitForSeconds(.5f);
        PlayerSettings.Invincible = false;
    }

    public static IEnumerator Die()
    {
        PlayerSettings.DamagePanel.SetActive(true);
        yield return new WaitForSeconds(0f);

        for (int i = 0; i < PlayerSettings.HeartPanel.transform.childCount; ++i)
        {
            Destroy(PlayerSettings.HeartPanel.transform.GetChild(i).gameObject);
        }

        //PlayerSettings.animator.Play("Die");
        PlayerSettings.Invincible = true;
        Destroy(PlayerSettings.transform.GetComponent<PlayerMovement>());
        Destroy(PlayerSettings.transform.GetComponent<PlayerAttack>());
        Destroy(PlayerSettings.transform.GetComponent<ChangeRooms>());
        //Destroy(PlayerSettings.transform.GetComponent<BombScript>());
        PlayerSettings.DamagePanel.SetActive(false);
    }

    public static void TakeDamage(float damage)
    {
        if (!PlayerSettings.Invincible)
        {
            PlayerSettings.Invincible = true;
            CoroutineManager.Instance.StartCoroutine(Uninvincible());
            for(int i = 1; i <= damage; ++i)
            {
                Instantiate(PlayerSettings.HeartPanel.transform.GetChild((int)PlayerSettings.Health - i), 
                PlayerSettings.HeartPanel.transform).GetComponent<Animator>().Play("HeartAnimation");
                PlayerSettings.HeartPanel.transform.GetChild((int)PlayerSettings.Health - i).GetComponent<Image>().sprite = PlayerSettings.EmptyHeart;
            }

            PlayerSettings.Health -= damage;

            if(PlayerSettings.Health <= 0)
            {
                CoroutineManager.Instance.StartCoroutine(Die());
            }
            else
            {
                CoroutineManager.Instance.StartCoroutine(WaitAndReDrawHearts());
            }
        }
    }
    public static void DrawHeart(Sprite Type, int num)
    {
        GameObject Heart = new GameObject("Heart");
        Image HeartImage = Heart.AddComponent<Image>();
        HeartImage.sprite = Type;
        RectTransform rectTransform = Heart.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(PlayerSettings.HeartPanel.GetComponent<RectTransform>().sizeDelta.x / 10, PlayerSettings.HeartPanel.GetComponent<RectTransform>().sizeDelta.y / 3);
        Animator animator = Heart.AddComponent<Animator>();
        animator.runtimeAnimatorController = PlayerSettings.HeartAnimator;

        float XPos = 0;
        float YPos = -5;
        if (num <= 9)
        {
            XPos = num * PlayerSettings.HeartPanel.GetComponent<RectTransform>().sizeDelta.x / 10;
        }
        else
        {
            XPos = (num-10) * PlayerSettings.HeartPanel.GetComponent<RectTransform>().sizeDelta.x / 10;
            YPos = -5 - PlayerSettings.HeartPanel.GetComponent<RectTransform>().sizeDelta.x / 10 - 5;
        }

        rectTransform.position = new Vector2(XPos, YPos);
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        HeartImage.transform.SetParent(PlayerSettings.HeartPanel.transform, false);
    }

    public static void DrawHearts()
    {
        for(int i = 0; i < PlayerSettings.Health; i++)
        {
            DrawHeart(PlayerSettings.FullHeart, i);
        }

        if(PlayerSettings.Health % 1 != 0)
        {
            DrawHeart(PlayerSettings.HalfHeart, (int)PlayerSettings.Health);
        }

        for(float i = (int)PlayerSettings.Health; i <= PlayerSettings.MaxHealth-1; ++i)
        {
            DrawHeart(PlayerSettings.EmptyHeart, (int)i);
        }
    }

}

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;

    public static CoroutineManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("CoroutineManager");
                _instance = go.AddComponent<CoroutineManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }
}
