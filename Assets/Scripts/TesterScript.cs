using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using URPGlitch.Runtime.AnalogGlitch;

public class TesterScript : MonoBehaviour
{

    public TextMeshProUGUI text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Path.Combine(Application.streamingAssetsPath, "test.txt");
    }
}
