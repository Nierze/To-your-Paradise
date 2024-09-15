using System;
using System.Collections.Generic;
using System.IO;

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

        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);

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
            Console.WriteLine($"File at {fileName} does not exist.");
        }

        return dialogueEntries;
    }
}
