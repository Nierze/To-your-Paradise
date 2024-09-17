using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
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

    private List<string> introTextLines;
    private int currentLineIndex;
    private bool isTyping;
    private bool isTextComplete;
    private bool hasRampUpStarted;

    private const float TargetGlitchValue = 1f;
    private const float GlitchRampUpDuration = 4f;

    private void Start()
    {
        InitializeComponents();
    }

    private void Update()
    {
        HandleVideoTransition();
        HandleTextTyping();
        HandleSceneTransition();
    }

    private void InitializeComponents()
    {
        introTextLines = LoadIntroText(introTextFilePath);
        introTextBox.text = string.Empty;

        if (sceneVolume.profile.TryGet(out analogGlitch))
        {
            // Initialization of analogGlitch if needed
        }
    }

    private void HandleVideoTransition()
    {
        if (!introVideo.isPlaying && screen.color.a > 0f)
        {
            StartCoroutine(TransitionUtils.FadeTransparency(screen, 0f, 1f));
        }

        if (screen.color.a == 0f)
        {
            volumeHandlerObject.SetActive(true);
        }
    }

    private void HandleTextTyping()
    {
        if (introVideo.isPlaying || isTyping || screen.color.a > 0f) return;

        if (currentLineIndex < introTextLines.Count)
        {
            StartCoroutine(TypeOutCurrentLine());
        }
        else
        {
            isTextComplete = true;
        }
    }

    private IEnumerator TypeOutCurrentLine()
    {
        isTyping = true;
        yield return StartCoroutine(TypingUtils.TypeLine(introTextBox, introTextLines[currentLineIndex], textTypeSpeed, 1));
        currentLineIndex++;
        isTyping = false;
    }

    private List<string> LoadIntroText(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return new List<string>();
        }

        return File.ReadAllLines(filePath).ToList();
    }

    private void HandleSceneTransition()
    {
        if (isTextComplete && Input.anyKeyDown && !hasRampUpStarted)
        {
            StartCoroutine(StartSceneTransition());
            hasRampUpStarted = true;
        }
    }

    private IEnumerator StartSceneTransition()
    {

        StartCoroutine(TransitionUtils.GlitchRampUp(analogGlitch, TargetGlitchValue, GlitchRampUpDuration));
        yield return new WaitForSeconds(GlitchRampUpDuration / 2f);
        TransitionCanvasHandler.Instance.FadeOut();
        yield return new WaitForSeconds(1f);
        
        yield return StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
