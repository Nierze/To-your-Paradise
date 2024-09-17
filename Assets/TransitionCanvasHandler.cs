using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransitionCanvasHandler : MonoBehaviour
{

    private static TransitionCanvasHandler _instance;
    public static TransitionCanvasHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TransitionCanvasHandler>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("TransitionCanvasHandler");
                    _instance = singletonObject.AddComponent<TransitionCanvasHandler>();
                }
            }

            return _instance;
        }
    }

    [SerializeField] private Image fadePanel;
    [SerializeField] private Animator fadePanelAnim;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        //fadePanel.canvasRenderer.SetAlpha(0f);
    }

    public void FadeIn()
    {
        fadePanelAnim.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        fadePanelAnim.SetTrigger("FadeOut");
    }
}
