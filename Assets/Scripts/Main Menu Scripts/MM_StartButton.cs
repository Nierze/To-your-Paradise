using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MM_StartButton : MenuButtonParentClass
{
    public void StartGame()
    {
        // Start the fade-in and scene loading process
        StartCoroutine(LoadGameAfterFade());
    }

    private IEnumerator LoadGameAfterFade()
    {
        // Start the fade-in effect
        yield return TransitionCanvasHandler.Instance.FadeOutAsynch();

        // Load the scene after the fade-in completes
        SceneManager.LoadScene("SampleScene");
    }
}
