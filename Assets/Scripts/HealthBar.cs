using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _image.fillAmount = currentHealth / maxHealth;
    }
}
