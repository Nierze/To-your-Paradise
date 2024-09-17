using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch.Runtime.AnalogGlitch;

public class MainMenuHandler : MonoBehaviour
{

    [Header("Post Processing Components")]
    [SerializeField] private Volume sceneVolume;
    [SerializeField] private AnalogGlitchVolume analogGlitch;
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
        
    }


    private IEnumerator  menuGlitchTransition(float outDuration) 
    {
        yield return TransitionUtils.ChangeAnalogGlitchVolume(analogGlitch, 1f, 1f, 1f, 1f, 0.2f);
        yield return TransitionUtils.ChangeAnalogGlitchVolume(analogGlitch, 0.01f, 0.03f, 0.01f, 0.04f, outDuration);
    }
}
