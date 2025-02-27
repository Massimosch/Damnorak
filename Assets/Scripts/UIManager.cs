using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private TMP_Text keyText;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (crystalText != null)
        {
            crystalText.text = " " + PlayerSettings.Crystals;
        }

        if (keyText != null)
        {
            keyText.text = " " + PlayerSettings.Keys;
        }
    }
}
