using System.Collections;
using UnityEngine;

public class TestGlobalController : MonoBehaviour
{
    public int testint = 0;
    private bool isCoroutineRunning = false;

    void Start()
    {
    }

    void Update()
    {
        if (testint == 0 && !isCoroutineRunning)
        {
            StartCoroutine(HandleUserChoice());
        }

        // suppose pickChoice()

        // codes here are blocked till pickChoice() is done
    }

    private IEnumerator HandleUserChoice()
    {
        isCoroutineRunning = true;

        yield return UserChoiceHandler.Instance.ShowDialogueChoices(3);

        testint = UserChoiceHandler.Instance.GetUserChoice();

        isCoroutineRunning = false; // Allow for future coroutine calls, if necessary
    }

    // pickChoice() implement

}
