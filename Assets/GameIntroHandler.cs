using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;

public class GameIntroHandler : MonoBehaviour
{
    [Header("Post Processing Components")]
    [SerializeField] private Volume sceneVolume;
    [SerializeField] private AnalogGlitchVolume analogGlitch;
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
    private bool isTextComplete = false;  // New flag to track if the text is fully displayed
    private bool hasRampUpStarted = false;  // Flag to ensure RampUpGlitchEffects is only called once

    private void Start()
    {
        // Load intro text lines from the file
        introTextLines = LoadIntroText(introTextFilePath);
        introTextBox.text = string.Empty; // Clear the text box

        // Set the volume handler object to inactive
        if (sceneVolume.profile.TryGet(out analogGlitch))
        {
            // analogGlitch.scanLineJitter.value = 0.5f;
            // Debug.Log("Analog Glitch scanLineJitter set to 0.5");
        }
    }

    private void Update()
    {
        HandleVideoTransition();
        HandleTextTyping();
        HandleSceneTransition();  // Check for key press after text is complete
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
            else
            {
                isTextComplete = true;  // Set flag when all lines are typed
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

    // Handle scene transition when text is complete and a key is pressed
    private void HandleSceneTransition()
    {
        if (isTextComplete && Input.anyKeyDown)  // Check if typing is complete and any key is pressed
        {
            if (!hasRampUpStarted)
            {
                StartCoroutine(StartSceneTransition());  // Call StartSceneTransition coroutine when key is pressed
                hasRampUpStarted = true;
            }
        }
    }

    // Coroutine to handle scene transition after ramping up glitch effects
    private IEnumerator StartSceneTransition()
    {
        // Start the ramp-up effect
        yield return StartCoroutine(RampUpGlitchEffects(0.7f, 3f));

        // After the ramp-up effect is complete, load the new scene
        yield return StartCoroutine(LoadScene());
    }

    // Coroutine to load the new scene asynchronously
    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator RampUpGlitchEffects(float targetValue, float duration)
    {
        float elapsedTime = 0f;

        // Store properties in an array to apply the same logic to all of them
        var glitchProperties = new[]
        {
            new { property = analogGlitch.scanLineJitter, startValue = analogGlitch.scanLineJitter.value },
            new { property = analogGlitch.verticalJump, startValue = analogGlitch.verticalJump.value },
            new { property = analogGlitch.horizontalShake, startValue = analogGlitch.horizontalShake.value },
            new { property = analogGlitch.colorDrift, startValue = analogGlitch.colorDrift.value }
        };

        // If none of the properties need ramping, exit early
        if (!glitchProperties.Any(g => g.startValue < targetValue)) yield break;

        // Ramp up the properties over time
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            foreach (var glitch in glitchProperties)
            {
                if (glitch.startValue < targetValue)
                {
                    glitch.property.value = Mathf.Lerp(glitch.startValue, targetValue, elapsedTime / duration);
                }
            }
            yield return null;
        }

        // Ensure final values are set to the target
        foreach (var glitch in glitchProperties)
        {
            if (glitch.startValue < targetValue)
                glitch.property.value = targetValue;
        }
    }
}
