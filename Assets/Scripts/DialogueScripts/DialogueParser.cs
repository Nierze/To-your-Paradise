using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueParser
{
    // Class to represent a dialogue entry with source and dialogue text
    public class DialogueEntry
    {
        public string Source { get; set; }
        public string Dialogue { get; set; }

        public DialogueEntry(string source, string dialogue)
        {
            Source = source;
            Dialogue = dialogue;
        }
    }

    // Static function to parse the dialogue file and return a list of DialogueEntry pairs
    public static List<DialogueEntry> ParseDialogueFile(string fileName)
    {
        List<DialogueEntry> dialogueEntries = new List<DialogueEntry>();

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            string currentSource = "";
            string currentDialogue = "";

            foreach (string line in lines)
            {
                if (line.StartsWith("[SOURCE:"))
                {
                    // Extract source between [SOURCE: and ]
                    currentSource = line.Substring(8, line.Length - 9); // Removes [SOURCE:] and the closing ]
                }
                else if (line.StartsWith("[DIALOGUE:"))
                {
                    // Extract dialogue between [DIALOGUE: and ]
                    currentDialogue = line.Substring(10, line.Length - 11); // Removes [DIALOGUE:] and the closing ]

                    // Add the pair to the list
                    dialogueEntries.Add(new DialogueEntry(currentSource, currentDialogue));
                }
            }
        }
        else
        {
            Debug.LogError($"File at {filePath} does not exist.");
        }

        return dialogueEntries;
    }
    
}
