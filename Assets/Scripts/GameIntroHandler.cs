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
    // Constants
    private const float TargetGlitchValue = 1f;
    [SerializeField] private const float GlitchRampUpDuration = 2f;

    // Serialized Fields
    [Header("Post Processing Components")]
    [SerializeField] private Volume sceneVolume;
    [SerializeField] private AnalogGlitchVolume analogGlitch;

    [Header("Video Components")]
    [SerializeField] private VideoPlayer introVideo;
    [SerializeField] private RawImage screen;

    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI introTextBox;
    [SerializeField] private float textTypeSpeed = 0.05f;
    [SerializeField] private string introTextFilePath = ""; // Only the file name

    [Header("Other Components")]
    [SerializeField] private GameObject volumeHandlerObject;
    [SerializeField] private Button startButton;

    // Private Fields
    private List<string> introTextLines;
    private int currentLineIndex;
    private bool isTyping;
    private bool isTextComplete;
    private bool hasRampUpStarted;

    private void Start()
    {
        InitializeComponents();
        startButton.interactable = false;
    }

    private void Update()
    {
        HandleVideoTransition();
        HandleTextTyping();

        if (isTextComplete) 
        {
            startButton.interactable = true;
        }
    }

    private void InitializeComponents()
    {
        introTextLines = LoadIntroText(introTextFilePath);
        introTextBox.text = string.Empty;

        if (sceneVolume.profile.TryGet(out analogGlitch))
        {
        }
    }

    private void HandleVideoTransition()
    {
        if (!introVideo.isPlaying && screen.color.a > 0f)
        {
            StartCoroutine(TransitionUtils.FadeTransparency(screen, 0f, 1f));
        }
        else if (screen.color.a == 0f && !volumeHandlerObject.activeSelf)
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
        yield return TypingUtils.TypeLine(introTextBox, introTextLines[currentLineIndex], textTypeSpeed, 1);
        currentLineIndex++;
        isTyping = false;
    }

    private List<string> LoadIntroText(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found: {filePath}");
            return new List<string>();
        }

        return File.ReadAllLines(filePath).ToList();
    }

    public void HandleSceneTransition()
    {
        if (isTextComplete && !hasRampUpStarted)
        {
            hasRampUpStarted = true;
            StartCoroutine(StartSceneTransition());
        }
    }

    private IEnumerator StartSceneTransition()
    {
        yield return StartCoroutine(TransitionUtils.GlitchRampUp(analogGlitch, TargetGlitchValue, GlitchRampUpDuration));
        yield return new WaitForSeconds(GlitchRampUpDuration / 2f);
        TransitionCanvasHandler.Instance.FadeOut();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(LoadScene("MainMenu"));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
