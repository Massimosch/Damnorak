using UnityEngine;

[CreateAssetMenu(fileName = "New Letter", menuName = "New Item/Letter")]
public class LetterItem : Item
{
    [TextArea(5, 10)]
    public string letterContent; 
    public AudioClip letterAudio; 

    public override void Use()
    {
        UIManager uiManager = FindFirstObjectByType<UIManager>();
        uiManager.ShowLetter(letterContent, letterAudio);
    }
}
