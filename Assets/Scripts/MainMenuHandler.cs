using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using URPGlitch.Runtime.AnalogGlitch;


public class MainMenuHandler : MonoBehaviour
{

    [Header("Post Processing Components")]
    [SerializeField] private Volume sceneVolume;
    [SerializeField] private AnalogGlitchVolume analogGlitch;

    [Header("UI Components")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private List<Sprite> backgroundImages;
    void Start()
    {
        if (!sceneVolume.profile.TryGet(out analogGlitch))
        {
            Debug.LogError("Analog Glitch Volume not found in the Scene Volume Profile");
        }
        TransitionCanvasHandler.Instance.FadeIn();
        StartCoroutine(menuGlitchTransition(2f));
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(menuGlitchTransition(0.6f));
            backgroundImage.sprite = backgroundImages[Random.Range(0, backgroundImages.Count)];
        }
    }


    private IEnumerator  menuGlitchTransition(float outDuration) 
    {
        yield return TransitionUtils.ChangeAnalogGlitchVolume(analogGlitch, 1f, 1f, 1f, 1f, 0.05f);
        yield return TransitionUtils.ChangeAnalogGlitchVolume(analogGlitch, 0.02f, 0.03f, 0.02f, 0.05f, outDuration);
    }
}
