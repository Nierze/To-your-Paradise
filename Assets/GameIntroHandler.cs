using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class GameIntroHandler : MonoBehaviour
{
    [Header("Video Components")]
    [SerializeField] private VideoPlayer introVideo;
    [SerializeField] private RawImage screen;

    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI introTextBox;
    [SerializeField] private float textTypeSpeed = 0.05f;
    [SerializeField] private string introTextFilePath = "Assets/Dialogues/IntroText.scrpt";

    [Header("Other Components")]
    [SerializeField] private GameObject volumeHandlerObject;

    private List<string> introTextLines = new List<string>();
    private int currentLineIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        // Load intro text lines from the file
        introTextLines = LoadIntroText(introTextFilePath);
        introTextBox.text = string.Empty; // Clear the text box
    }

    private void Update()
    {
        HandleVideoTransition();
        HandleTextTyping();
    }

    // Handle the video transition and fade
    private void HandleVideoTransition()
    {
        if (!introVideo.isPlaying && screen.color.a != 0f)
        {
            StartCoroutine(TransitionUtils.FadeTransparency(screen, 0f, 1f));
        }

        if (screen.color.a == 0f)
        {
            volumeHandlerObject.SetActive(true);
        }
    }

    // Handle the typing of text lines
    private void HandleTextTyping()
    {
        if (!introVideo.isPlaying && !isTyping && screen.color.a == 0f)
        {
            if (currentLineIndex < introTextLines.Count)
            {
                StartCoroutine(TypeOutCurrentLine());
            }
        }
    }

    // Coroutine to type out the current line
    private IEnumerator TypeOutCurrentLine()
    {
        isTyping = true;
        string currentLine = introTextLines[currentLineIndex];

        yield return StartCoroutine(TypingUtils.TypeLine(introTextBox, currentLine, textTypeSpeed, 1));
        currentLineIndex++;
        isTyping = false;
    }

    // Load intro text lines from a file
    private List<string> LoadIntroText(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return new List<string>();
        }

        return new List<string>(File.ReadAllLines(filePath));
    }
}
