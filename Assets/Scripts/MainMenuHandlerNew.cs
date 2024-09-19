using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using URPGlitch.Runtime.AnalogGlitch;

public class MainMenuHandlerNew : MonoBehaviour
{
    [Header("Post Processing Components")]
    [SerializeField] private Volume sceneVolume;
    [SerializeField] private AnalogGlitchVolume analogGlitch;

    [Header("UI Components")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private List<MenuItem> menuItems;
    [SerializeField] private GameObject menuOptionContainer;
    private int menuIndex = 0;

    [System.Serializable]
    public struct MenuItem
    {
        public GameObject menuItem;
        public Sprite backgroundImage;
    }

    void Start()
    {
        if (!sceneVolume.profile.TryGet(out analogGlitch))
        {
            Debug.LogError("Analog Glitch Volume not found in the Scene Volume Profile");
        }
        TransitionCanvasHandler.Instance.FadeIn();
        StartCoroutine(menuGlitchTransition(2f));
        ReplaceMenuOptionContainer(menuItems[menuIndex].menuItem);
    }

    void Update()
    {

    }

    private IEnumerator menuGlitchTransition(float outDuration)
    {
        yield return TransitionUtils.ChangeAnalogGlitchVolume(analogGlitch, 1f, 1f, 1f, 1f, 0.05f);
        yield return TransitionUtils.ChangeAnalogGlitchVolume(analogGlitch, 0.02f, 0.03f, 0.02f, 0.05f, outDuration);
    }

public void moveOptionLeft() 
{
    StartCoroutine(menuGlitchTransition(0.5f));
    menuIndex = (menuIndex - 1 + menuItems.Count) % menuItems.Count;
    backgroundImage.sprite = menuItems[menuIndex].backgroundImage;
    ReplaceMenuOptionContainer(menuItems[menuIndex].menuItem);
}

public void moveOptionRight() 
{
    StartCoroutine(menuGlitchTransition(0.5f));
    menuIndex = (menuIndex + 1) % menuItems.Count;
    backgroundImage.sprite = menuItems[menuIndex].backgroundImage;
    ReplaceMenuOptionContainer(menuItems[menuIndex].menuItem);
}


private void ReplaceMenuOptionContainer(GameObject newMenuOption)
{
    // Destroy the current menu option container
    if (menuOptionContainer != null)
    {
        Destroy(menuOptionContainer);
    }

    // Instantiate the new menu option and make it the new container
    menuOptionContainer = Instantiate(newMenuOption, menuOptionContainer.transform.parent);

    // Optionally set its position/rotation/scale to match the original
    menuOptionContainer.transform.localPosition = Vector3.zero;
    menuOptionContainer.transform.localRotation = Quaternion.identity;
    menuOptionContainer.transform.localScale = Vector3.one;

    // Make sure the new container is active
    menuOptionContainer.SetActive(true);
}
}
