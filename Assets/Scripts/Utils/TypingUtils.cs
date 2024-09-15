using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class TypingUtils
{
    private static string randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]{}|;':\",.<>/?";

    // Typing function with glitch effect
    public static IEnumerator TypeLine(Text dialogueText, string dialogue, float textTypeSpeed, float glitchEffectCount)
    {
        // Clear the text initially
        dialogueText.text = "";
        
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;

            // Call the GlitchEffect coroutine to apply the glitch effect
            yield return GlitchEffect(dialogueText, glitchEffectCount);

            // Replace the last random character with the actual letter
            dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1) + letter;

            // Pause between each letter for typing effect
            yield return new WaitForSeconds(textTypeSpeed);
        }
    }

    // Glitch effect that randomly changes the current letter
    private static IEnumerator GlitchEffect(Text dialogueText, float glitchEffectCount)
    {
        float effectCurrentCount = glitchEffectCount;

        while (effectCurrentCount > 0)
        {
            char randomChar = randomChars[Random.Range(0, randomChars.Length)];
            dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - 1) + randomChar;

            yield return new WaitForSeconds(0.01f); // Time between glitch changes
            effectCurrentCount--;
        }
    }
}
