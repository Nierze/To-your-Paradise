using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatedIntro : MonoBehaviour {

    private string movie = "AlterZeroAnimation.mp4";

    void Start () 
    {
        StartCoroutine(streamVideo(movie));
    }

    private IEnumerator streamVideo(string video)
    {
        Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.Fill);
        yield return new WaitForEndOfFrame ();
        SceneManager.LoadScene ("Main Menu");
    }
}