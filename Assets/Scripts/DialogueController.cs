using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    ////////////////////////////////////////////////
    /// Singleton Pattern
    private static DialogueController instance;
    public static DialogueController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueController>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("DialogueController");
                    instance = singleton.AddComponent<DialogueController>();
                }
            }
            return instance;
        }
    }

    /////////////////////////////////////////////////
    /// Variables
    
    public TextMeshProUGUI DialogueSource;
    public TextMeshProUGUI DialogueText;

    // Use DialogueEntry array instead of string array for lines
    public List<DialogueParser.DialogueEntry> dialogueEntries; // Array of DialogueEntry objects
    private int index; // Index of the current line
    public float textTypeSpeed; // Speed of text typing
    public float glitchEffectCount; // Number of a letter will change before the actual letter

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Assuming you have a way to load the dialogue entries from a file
        // For demonstration, I'm assuming dialogueEntries is populated with DialogueEntry objects
        
        dialogueEntries = DialogueParser.ParseDialogueFile("Assets/Dialogues/testScript.scrpt");
        DialogueText.text = string.Empty;
        DialogueSource.text = string.Empty;
        startDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DialogueText.text == dialogueEntries[index].Dialogue)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                DialogueText.text = dialogueEntries[index].Dialogue;
            }
        }
    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (index < dialogueEntries.Count - 1)
        {
            index++;
            DialogueText.text = string.Empty;
            DialogueSource.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            DialogueText.text = string.Empty;
            DialogueSource.text = string.Empty;
            index = 0;
            StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        string randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        DialogueSource.text = dialogueEntries[index].Source; // Show the source (speaker)
        
        foreach (char letter in dialogueEntries[index].Dialogue.ToCharArray())
        {
            DialogueText.text += letter;

            // Call the GlitchEffect coroutine
            yield return StartCoroutine(GlitchEffect(randomChars));

            // Replace the last random character with the actual letter
            DialogueText.text = DialogueText.text.Substring(0, DialogueText.text.Length - 1) + letter;

            yield return new WaitForSeconds(textTypeSpeed);
        }
    }

    // Separate GlitchEffect function
    IEnumerator GlitchEffect(string randomChars)
    {
        float effectCurrentCount = glitchEffectCount;

        while (effectCurrentCount > 0)
        {
            char randomChar = randomChars[Random.Range(0, randomChars.Length)];
            DialogueText.text = DialogueText.text.Substring(0, DialogueText.text.Length - 1) + randomChar;

            yield return new WaitForSeconds(0.01f); // Adjust this value as needed
            effectCurrentCount--;
        }
    }
}