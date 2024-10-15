using System.Collections;  // Ensure you have this namespace
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;
using UnityEngine.SceneManagement;

public abstract class DialogueCore : MonoBehaviour
{

    public static DialogueCore Instance { get; private set; }

    [Header("Scene Resources")]
    [SerializeField] protected Volume volume;
    [SerializeField] protected Image backgroundImage;
    [SerializeField] protected List<AudioFile> AudioFiles;
    [SerializeField] protected List<Sprite> backgroundImages;

    [Header("Dialogue Essentials (INITIALIZED)")]
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
        this.InitializeResources();
        
        StartCoroutine(MainGameLoop());
    }

    protected virtual void Start()
    {

    }

    public void InitializeResources()
    {
        AudioManager.Instance.InitializeAudioFiles(AudioFiles);
        typeSpeed = PlayerPrefs.GetFloat("TypingSpeed", 0.01f);  // Default value 0.05f if not set
        glitchDuration = PlayerPrefs.GetFloat("GlitchDuration", 0.2f);  // Default value 0.2f if not set
        dialogueSource = GameObject.Find("Dialogue Source").GetComponent<TextMeshProUGUI>();
        dialogueContent = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        backgroundImage = GameObject.Find("Background Image").GetComponent<Image>();
        volume = GameObject.Find("PostProcess").GetComponent<Volume>();

    }

    protected IEnumerator Say(CharacterData character, string dialogue)
    {
        // Set the character's name and title on the UI
        dialogueSource.text = character.characterName;

        // Start the typing effect and wait for it to finish
        yield return TypingUtils.TypeLineGlitch(dialogueContent, dialogue, typeSpeed, 0, glitchDuration, 0.01f);

        // Wait for player input (e.g., pressing space) to continue
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }

    protected abstract IEnumerator MainGameLoop(); 
}
