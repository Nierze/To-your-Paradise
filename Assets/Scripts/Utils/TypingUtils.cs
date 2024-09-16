using System.Collections;
using UnityEngine;
using TMPro;

public static class TypingUtils
{
    private const string GlitchCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+-=[]{}|;':\",.<>/?";

    // Typing effect with optional glitch effect
    public static IEnumerator TypeLineGlitch(TextMeshProUGUI textComponent, string message, float typingSpeed, int addNewLine, float glitchDuration, float glitchSpeed)
    {
        // Handle text resetting or appending
        textComponent.text = (addNewLine == 0) ? string.Empty : textComponent.text + "\n";

        foreach (char letter in message.ToCharArray())
        {
            textComponent.text += letter;

            // Apply glitch effect
            yield return ApplyGlitchEffect(textComponent, glitchDuration, glitchSpeed);

            // Restore the correct letter after glitch
            textComponent.text = ReplaceLastCharacter(textComponent.text, letter);

            // Pause for typing effect
            yield return new WaitForSeconds(typingSpeed);
        }
    }

        // Simple typing effect without glitch
    public static IEnumerator TypeLine(TextMeshProUGUI textComponent, string message, float typingSpeed, int addNewLine)
    {
        // Handle text resetting or appending
        textComponent.text = (addNewLine == 0) ? string.Empty : textComponent.text + "\n";

        foreach (char letter in message.ToCharArray())
        {
            textComponent.text += letter;

            // Pause for typing effect
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Applies a glitch effect by temporarily replacing the current letter
    private static IEnumerator ApplyGlitchEffect(TextMeshProUGUI textComponent, float duration, float speed)
    {
        while (duration > 0)
        {
            char randomChar = GlitchCharacters[Random.Range(0, GlitchCharacters.Length)];
            textComponent.text = ReplaceLastCharacter(textComponent.text, randomChar);

            yield return new WaitForSeconds(speed);
            duration--;
        }
    }

    // Helper function to replace the last character of the string
    private static string ReplaceLastCharacter(string text, char newChar)
    {
        return text.Substring(0, text.Length - 1) + newChar;
    }
}
