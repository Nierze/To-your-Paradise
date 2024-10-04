using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserChoiceHandler : MonoBehaviour
{
    // Singleton instance
    public static UserChoiceHandler Instance { get; private set; }

    [Header("Choices")]
    [SerializeField] private List<GameObject> choices = new List<GameObject>(); // List for choices

    private int userChoice = 0;

    [Header("Choices Texts")]
    [SerializeField] private List<string> choiceTexts = new List<string>(); // List for choice texts

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        foreach (var choice in choices)
        {
            choice.SetActive(false);
        }
    }

    private void Update()
    {
        // Update the texts for each choice
        for (int i = 0; i < choices.Count; i++)
        {
            if (choices[i].activeSelf) // Only update active choices
            {
                choices[i].GetComponentInChildren<TextMeshProUGUI>().text = choiceTexts[i];
            }
        }
    }

    public IEnumerator ShowDialogueChoices(int optionCount)
    {
        userChoice = 0;

        // Activate the number of choices based on optionCount
        for (int i = 0; i < optionCount; i++)
        {
            if (i <= choices.Count) // Ensure we don't exceed the list length
            {
                choices[i].SetActive(true);
            }
        }

        // Wait until the user makes a choice
        while (userChoice == 0)
        {
            yield return null; // Wait for the next frame and check again
        }

        // Disable all choices after selection
        foreach (var choice in choices)
        {
            choice.SetActive(false);
        }

        Debug.Log("User choice: " + userChoice);
    }

    public int GetUserChoice()
    {
        return userChoice;
    }

    public void Choose(int choiceIndex)
    {
        Debug.Log("pressed");
        if (choiceIndex > 0 && choiceIndex <= choices.Count)
        {
            userChoice = choiceIndex;
        }
    }
}
