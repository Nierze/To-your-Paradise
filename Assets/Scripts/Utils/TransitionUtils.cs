using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class TransitionUtils
{
    //////////////////////////////////////
    /// Fade Methods

    // Fade function that works with Graphic (Image, RawImage) and SpriteRenderer
    public static IEnumerator FadeTransparency(Component component, float targetAlpha, float duration)
    {
        // Determine the type of the component and get the initial alpha
        float startAlpha = GetAlpha(component);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            SetAlpha(component, newAlpha);
            yield return null;
        }

        SetAlpha(component, targetAlpha);
    }

    // Method to set alpha of a Component and its children
    public static void SetAlpha(Component component, float alpha)
    {
        if (component is Graphic graphic)
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;

            // Handle child Graphics components
            foreach (Transform child in graphic.transform)
            {
                Graphic childGraphic = child.GetComponent<Graphic>();
                if (childGraphic != null)
                {
                    Color childColor = childGraphic.color;
                    childColor.a = alpha;
                    childGraphic.color = childColor;
                }
            }
        }
        else if (component is SpriteRenderer spriteRenderer)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }

    // Get the alpha value of a Component
    private static float GetAlpha(Component component)
    {
        if (component is Graphic graphic)
        {
            return graphic.color.a;
        }
        else if (component is SpriteRenderer spriteRenderer)
        {
            return spriteRenderer.color.a;
        }
        return 1f; // Default alpha value if not found
    }

    //////////////////////////////////////
}
