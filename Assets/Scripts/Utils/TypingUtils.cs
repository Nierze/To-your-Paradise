using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class TypingUtils
{
    private static string randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]{}|;':\",.<>/?";

    // Typing function with glitch effect
    public static IEnumerator TypeLine(TextMeshProUGUI dialogueText, string dialogue, float textTypeSpeed, float glitchEffectCount, int appendNewLine, float glitchEffectSpeed)
    {
        // If appendNewLine is 0, clear the current text, otherwise start a new line
        if (appendNewLine == 0)
        {
            dialogueText.text = "";
        }
        else
        {
            dialogueText.text += "\n"; // Add a new line before the new text
        }

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;

            // Call the GlitchEffect coroutine to apply the glitch effect
            yield return GlitchEffect(dialogueText, glitchEffectCount, glitchEffectSpeed);

            // Replace the last random character with the actual letter
            dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1) + letter;

            // Pause between each letter for typing effect
            yield return new WaitForSeconds(textTypeSpeed);
        }
    }

    // Glitch effect that randomly changes the current letter
    private static IEnumerator GlitchEffect(TextMeshProUGUI  dialogueText, float glitchEffectCount, float glitchEffectSpeed)
    {
        float effectCurrentCount = glitchEffectCount;

        while (effectCurrentCount > 0)
        {
            char randomChar = randomChars[Random.Range(0, randomChars.Length)];
            dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1) + randomChar;

            yield return new WaitForSeconds(glitchEffectSpeed); // Time between glitch changes
            effectCurrentCount--;
        }
    }
}
