using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGlobalController : MonoBehaviour
{
    public int testint = 0;

    // Update is called once per frame
    void Update()
    {
        if (testint == 0) 
        {
            StartCoroutine(UserChoiceHandler.Instance.ShowDialogueChoices(3));
            testint = UserChoiceHandler.Instance.GetUserChoice();
        }

    }
}
