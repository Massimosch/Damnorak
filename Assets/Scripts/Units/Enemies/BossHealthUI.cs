using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] private Slider healthBar;
    private Boss boss;

    public void SetupUI(Boss boss, string bossName)
    {
        this.boss = boss;
        bossNameText.text = bossName;
        healthBar.maxValue = boss.MaxHealth;
        healthBar.value = boss.CurrentHealth;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (boss != null)
        {
            healthBar.value = boss.CurrentHealth;

            if (boss.CurrentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

