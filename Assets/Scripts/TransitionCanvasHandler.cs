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

    [SerializeField] private GameObject fadePanelObject;
    private Image fadePanel;
    [SerializeField] private GameObject transitionCanvas;
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
        transitionCanvas = gameObject;
        fadePanel = fadePanelObject.GetComponent<Image>();
    }

    public void FadeIn()
    {
        transitionCanvas.GetComponent<GraphicRaycaster>().enabled = false;
        fadePanelAnim.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        transitionCanvas.GetComponent<GraphicRaycaster>().enabled = true;
        fadePanelAnim.SetTrigger("FadeOut");
    }

        public IEnumerator FadeInAsynch()
    {
        transitionCanvas.GetComponent<GraphicRaycaster>().enabled = false;
        fadePanelAnim.SetTrigger("FadeIn");

        // Wait for the animation to complete
        yield return new WaitForSeconds(1f); // Adjust the time as needed
    }

    public IEnumerator FadeOutAsynch()
    {
        transitionCanvas.GetComponent<GraphicRaycaster>().enabled = true;
        fadePanelAnim.SetTrigger("FadeOut");

        // Wait for the animation to complete
        yield return new WaitForSeconds(1f); // Adjust the time as needed
    }
}
