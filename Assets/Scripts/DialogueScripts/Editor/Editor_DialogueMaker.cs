using UnityEngine;
using UnityEditor;
public class Editor_DialogueMaker : EditorWindow
{
    [MenuItem("Tools/Dialogue Maker")]
    public static void ShowWindow()
    {
        GetWindow<Editor_DialogueMaker>("Dialogue Maker");
    }

    void OnGUI() 
    {
        
    }
}
