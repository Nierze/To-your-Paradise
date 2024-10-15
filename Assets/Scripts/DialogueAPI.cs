using UnityEngine;
using TMPro;

public class DialogueAPI : MonoBehaviour
{
    public static DialogueAPI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI dialogueSource;
    [SerializeField] private TextMeshProUGUI dialogueSourceTitle;
    [SerializeField] private TextMeshProUGUI dialogueText;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    // public void Say(CharacterData charData, string dialogue) 
    // {

    //     dialogueSource.text = charData.characterName;
    //     dialogueSourceTitle.text = charData.characterTitle;

    //     // PlayerPref Variables
    //     float typeSpeed = PlayerPrefs.GetFloat("TypingSpeed");
    //     float duration = PlayerPrefs.GetFloat("GlitchDuration");
    //     TypingUtils.TypeLineGlitch(dialogueText, dialogue, typeSpeed, 0, duration, 0.05f);
    // }



}
