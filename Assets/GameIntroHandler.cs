using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Video; 

public class GameIntroHandler : MonoBehaviour
{
    
    public VideoPlayer introVideo;
    public RawImage screen;
    void Update()
    {
        if(introVideo.isPlaying == false)
        {
            StartCoroutine(TransitionUtils.FadeTransparency(screen, 0f, 1f));
        }
    }
}
