using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private TMP_Text keyText;

    [Header("Letter UI")]
    [SerializeField] private GameObject letterPanel;
    [SerializeField] private TMP_Text letterText;
    [SerializeField] private Button closeButton;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TMP_Text subtitleText;

    private void Start()
    {
        UpdateUI();

        if (letterPanel != null)
        {
            letterPanel.SetActive(false);
        }

        if (subtitleText != null)
        {
            subtitleText.gameObject.SetActive(false);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseLetter);
        }
    }

    private void Update()
    {
        Time.timeScale = 1f;
        UpdateUI();
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
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

    // --- Letter UI Methods ---
    public void ShowLetter(string content, AudioClip audioClip)
    {
        if (letterText != null && letterPanel != null)
        {
            letterText.text = content;
            letterPanel.SetActive(true);
        }

        // Play audio if available
        if (audioClip != null && audioSource != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            StartCoroutine(DisplaySubtitles(content, audioClip.length));
        }
    }

    private IEnumerator DisplaySubtitles(string content, float duration)
    {
        if (subtitleText != null)
        {
            subtitleText.text = content;
            subtitleText.gameObject.SetActive(true);

            yield return new WaitForSeconds(duration);

            subtitleText.gameObject.SetActive(false);
        }
    }

    public void CloseLetter()
    {
        if (letterPanel != null)
        {
            letterPanel.SetActive(false);
        }

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (subtitleText != null)
        {
            subtitleText.gameObject.SetActive(false);
        }
    }
}
