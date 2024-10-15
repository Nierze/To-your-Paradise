using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MM_StartButton : MenuButtonParentClass
{
    // [SerializeField] private GameObject CoreToLoad; // The prefab you want to instantiate

    public void StartGame()
    {
        // Start the fade-out and scene loading process
        StartCoroutine(LoadGameAfterFade());
    }


    private IEnumerator LoadGameAfterFade()
    {
        // Start the fade-out effect
        yield return TransitionCanvasHandler.Instance.FadeOutAsynch();

        SceneManager.LoadScene("PrologueIntro");
    }

    // private IEnumerator LoadGameAfterFade()
    // {
    //     // Start the fade-out effect
    //     yield return TransitionCanvasHandler.Instance.FadeOutAsynch();

    //     // Register a callback to instantiate CoreToLoad after the scene is loaded
    //     SceneManager.sceneLoaded += OnSceneLoaded;

    //     // Load the scene after the fade-out completes
    //     SceneManager.LoadScene("DialogueSceneTemplate");
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // Check if the loaded scene is the correct one
    //     if (scene.name == "DialogueSceneTemplate")
    //     {
    //         // Instantiate CoreToLoad in the new scene
    //         Instantiate(CoreToLoad);
    //     }

    //     // Unsubscribe from the event after use to avoid multiple instantiations
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }
}
