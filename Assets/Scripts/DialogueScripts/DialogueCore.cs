using System.Collections;  // Ensure you have this namespace
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogueCore : MonoBehaviour
{

    public static DialogueCore Instance { get; private set; }

    [Header("Scene Resources")]
    [SerializeField] protected Image backgroundImage;
    [SerializeField] protected List<Sprite> backgroundImages;

    [Header("Dialogue Essentials")]
    [SerializeField] protected TextMeshProUGUI dialogueSource;
    [SerializeField] protected TextMeshProUGUI dialogueSourceTitle;
    [SerializeField] protected TextMeshProUGUI dialogueContent;
    [SerializeField] protected List<CharacterData> characters;

    [Header("Player Preferences")]
    [SerializeField] protected float typeSpeed;
    [SerializeField] protected float glitchDuration;




    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void Start()
    {
        this.InitializeResources();
        StartCoroutine(MainGameLoop());
    }

    public void InitializeResources()
    {
        typeSpeed = PlayerPrefs.GetFloat("TypingSpeed", 0.05f);  // Default value 0.05f if not set
        glitchDuration = PlayerPrefs.GetFloat("GlitchDuration", 0.2f);  // Default value 0.2f if not set
        dialogueSource = GameObject.Find("Dialogue Source").GetComponent<TextMeshProUGUI>();
        dialogueContent = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        backgroundImage = GameObject.Find("Background Image").GetComponent<Image>();

    }

    protected IEnumerator Say(CharacterData character, string dialogue)
    {
        // Set the character's name and title on the UI
        dialogueSource.text = character.characterName;

        // Start the typing effect and wait for it to finish
        yield return TypingUtils.TypeLineGlitch(dialogueContent, dialogue, typeSpeed, 0, glitchDuration, 0.2f);

        // Wait for player input (e.g., pressing space) to continue
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }

    protected abstract IEnumerator MainGameLoop(); 
}
