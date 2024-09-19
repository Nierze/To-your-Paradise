using System.Collections;
using UnityEngine;
using TMPro;

public abstract class MenuButtonParentClass : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI buttonText;
    [SerializeField] private float transitionDuration = 0.05f; // Duration of the smooth transition

    private Coroutine hoverCoroutine;

    public virtual void onHoverEnter()
    {
        if (buttonText != null)
        {
            // Stop any ongoing transition
            if (hoverCoroutine != null)
            {
                StopCoroutine(hoverCoroutine);
            }

            // Start a new smooth transition to the hover state
            hoverCoroutine = StartCoroutine(SmoothTransition(buttonText.fontSize, buttonText.fontSize + 5, FontStyles.Bold | FontStyles.UpperCase));
        }
    }

    public virtual void onHoverExit()
    {
        if (buttonText != null)
        {
            // Stop any ongoing transition
            if (hoverCoroutine != null)
            {
                StopCoroutine(hoverCoroutine);
            }

            // Start a new smooth transition back to the normal state
            hoverCoroutine = StartCoroutine(SmoothTransition(buttonText.fontSize, buttonText.fontSize - 5, FontStyles.UpperCase));
        }
    }

    private IEnumerator SmoothTransition(float startSize, float endSize, FontStyles endStyle)
    {
        float elapsedTime = 0f;
        buttonText.fontStyle = endStyle;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate the font size
            buttonText.fontSize = Mathf.Lerp(startSize, endSize, elapsedTime / transitionDuration);

            yield return null;
        }

        // Ensure the final values are set
        buttonText.fontSize = endSize;
        
    }
}
