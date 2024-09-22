using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_Quit : MenuButtonParentClass
{
    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
