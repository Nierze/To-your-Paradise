using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Video; 
using TMPro;

public class GameIntroHandler : MonoBehaviour
{
    
    public VideoPlayer introVideo;
    public RawImage screen;
    
    


        
    public GameObject volumeHandlerObject;


    //////////////////////////////////////
    /// Intro Text

    // Text box to display the intro text
    public TextMeshProUGUI introTextBox;

    // List to store the lines of the intro text
    public List<string> introTextLines;
    private int currentLineIndex = 0; 

    // Variables to control the typing speed and glitch effect
    public float textTypeSpeed;  
    public float glitchEffectCount;  
    private bool isTyping = false;
        
    

    // Start is called before the first frame update
    void Start()
    {
        // Read the intro text file
        introTextLines = ReadFileLines("Assets/Dialogues/IntroText.scrpt");
        introTextBox.text = ""; // Clear the text box

    }

    void Update()
    {
        if (introVideo.isPlaying == false && screen.color.a != 0f)
        {
            StartCoroutine(TransitionUtils.FadeTransparency(screen, 0f, 1f));
        }

        if (screen.color.a == 0f)
        {
            volumeHandlerObject.SetActive(true);
        }

        if (introVideo.isPlaying == false && !isTyping && screen.color.a == 0f)
        {
            if (currentLineIndex < introTextLines.Count)
            {
                StartCoroutine(TypeOutIntro());
            }
        }

        // if (currentLineIndex == introTextLines.Count)
        // {

        //     introTextBox.text = "type ";
        //     StartCoroutine(TypingUtils.TypeLine(introTextBox, "Press Space to Start", textTypeSpeed, glitchEffectCount, 1, 0)); 
        // }
        
    }

    // Coroutine to type each line from the list
    IEnumerator TypeOutIntro()
    {
        isTyping = true;

        if (currentLineIndex < introTextLines.Count)
        {
            // Get the current line to type
            string currentLine = introTextLines[currentLineIndex];
            
            // Use the TypeLine method from TypingUtils to type it out
            yield return StartCoroutine(TypingUtils.TypeLine(introTextBox, currentLine, textTypeSpeed, glitchEffectCount, 1, 0));
            
            // Wait for the player to press space to continue to the next line
            currentLineIndex++;
        }

        isTyping = false;
    }

    // Method to read the text file
    public List<string> ReadFileLines(string filePath)
    {
        List<string> lines = new List<string>();

        if (File.Exists(filePath))
        {
            lines.AddRange(File.ReadAllLines(filePath));
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }

        return lines;
    }
}