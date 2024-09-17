using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using URPGlitch.Runtime.AnalogGlitch;

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
    /// Glitch Ramp Up Method

    public static IEnumerator GlitchRampUp(AnalogGlitchVolume glitchVolume, float targetValue, float duration)
    {
        var glitchProperties = new[]
        {
            new { Property = glitchVolume.scanLineJitter, StartValue = glitchVolume.scanLineJitter.value },
            new { Property = glitchVolume.verticalJump, StartValue = glitchVolume.verticalJump.value },
            new { Property = glitchVolume.horizontalShake, StartValue = glitchVolume.horizontalShake.value },
            new { Property = glitchVolume.colorDrift, StartValue = glitchVolume.colorDrift.value }
        };

        // Check if any glitch property needs to ramp up
        if (!glitchProperties.Any(g => g.StartValue < targetValue)) yield break;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            foreach (var glitch in glitchProperties)
            {
                if (glitch.StartValue < targetValue)
                {
                    glitch.Property.value = Mathf.Lerp(glitch.StartValue, targetValue, t);
                }
            }
            yield return null;
        }

        // Ensure all properties reach the target value
        foreach (var glitch in glitchProperties)
        {
            if (glitch.StartValue < targetValue)
                glitch.Property.value = targetValue;
        }
    }

    //////////////////////////////////////
    /// Glitch Ramp Down Method

    public static IEnumerator GlitchRampDown(AnalogGlitchVolume glitchVolume, float[] initialHighValues, float[] originalValues, float duration)
    {
        var glitchProperties = new[]
        {
            new { Property = glitchVolume.scanLineJitter, StartValue = initialHighValues[0], OriginalValue = originalValues[0] },
            new { Property = glitchVolume.verticalJump, StartValue = initialHighValues[1], OriginalValue = originalValues[1] },
            new { Property = glitchVolume.horizontalShake, StartValue = initialHighValues[2], OriginalValue = originalValues[2] },
            new { Property = glitchVolume.colorDrift, StartValue = initialHighValues[3], OriginalValue = originalValues[3] }
        };

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            foreach (var glitch in glitchProperties)
            {
                glitch.Property.value = Mathf.Lerp(glitch.StartValue, glitch.OriginalValue, t);
            }
            yield return null;
        }

        foreach (var glitch in glitchProperties)
        {
            glitch.Property.value = glitch.OriginalValue;
        }
    }

    //////////////////////////////////////
    /// Change Analog Glitch Volume Method

    public static IEnumerator ChangeAnalogGlitchVolume(AnalogGlitchVolume glitchVolume, 
                                                    float targetScanLineJitter, 
                                                    float targetVerticalJump, 
                                                    float targetHorizontalShake, 
                                                    float targetColorDrift, 
                                                    float duration)
    {
        var glitchProperties = new[]
        {
            new { Property = glitchVolume.scanLineJitter, StartValue = glitchVolume.scanLineJitter.value, TargetValue = targetScanLineJitter },
            new { Property = glitchVolume.verticalJump, StartValue = glitchVolume.verticalJump.value, TargetValue = targetVerticalJump },
            new { Property = glitchVolume.horizontalShake, StartValue = glitchVolume.horizontalShake.value, TargetValue = targetHorizontalShake },
            new { Property = glitchVolume.colorDrift, StartValue = glitchVolume.colorDrift.value, TargetValue = targetColorDrift }
        };

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            foreach (var glitch in glitchProperties)
            {
                glitch.Property.value = Mathf.Lerp(glitch.StartValue, glitch.TargetValue, t);
            }
            yield return null;
        }

        // Ensure all properties reach their target values
        foreach (var glitch in glitchProperties)
        {
            glitch.Property.value = glitch.TargetValue;
        }
    }

}
