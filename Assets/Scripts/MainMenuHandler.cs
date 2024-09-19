using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using URPGlitch.Runtime.AnalogGlitch;


public class MainMenuHandler : MonoBehaviour
{

    [Header("Post Processing Components")]
    [SerializeField] private Volume sceneVolume;
    [SerializeField] private AnalogGlitchVolume analogGlitch;

    [Header("UI Components")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private List<Sprite> backgroundImages;
    [SerializeField] private RectTransform menuPanel;

    //////////////////////////////////////////////
    /// Menu Scroll Clapms
    private float menuItemCount;
    private float itemWidth;
    private float leftMax;
    private float rightMax;

    void Start()
    {
        if (!sceneVolume.profile.TryGet(out analogGlitch))
        {
            Debug.LogError("Analog Glitch Volume not found in the Scene Volume Profile");
        }
        TransitionCanvasHandler.Instance.FadeIn();
        StartCoroutine(menuGlitchTransition(2f));

        //////////////////////////////////////////////
        /// Menu Scroll Clapms
        menuItemCount = 3;
        itemWidth = 750f;
        leftMax = (menuItemCount * itemWidth) / 2;
        rightMax = -((menuItemCount * itemWidth) / 2 - itemWidth);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))  // REMINDER: Edit left and right max when changing menu items
        {
            if (rightMax != menuPanel.anchoredPosition.x)
            {
                scrollMenuPanel(-750f);
            }
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            if (leftMax != menuPanel.anchoredPosition.x) 
            {
                scrollMenuPanel(750f);
            }
        }
    }


    private IEnumerator  menuGlitchTransition(float outDuration) 
    {
        yield return TransitionUtils.ChangeAnalogGlitchVolume(analogGlitch, 1f, 1f, 1f, 1f, 0.05f);
        yield return TransitionUtils.ChangeAnalogGlitchVolume(analogGlitch, 0.02f, 0.03f, 0.02f, 0.05f, outDuration);
    }

    void scrollMenuPanel(float x)
    {
        StartCoroutine(menuGlitchTransition(0.6f));
        MoveRectTransformByX(menuPanel, x);
        backgroundImage.sprite = backgroundImages[Random.Range(0, backgroundImages.Count)];
    }


    void MoveRectTransformByX(RectTransform rectTransform, float deltaX)
    {
        Vector2 currentPosition = rectTransform.anchoredPosition;
        currentPosition.x += deltaX;
        rectTransform.anchoredPosition = currentPosition;
    }
}
