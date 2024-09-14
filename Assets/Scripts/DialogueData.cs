using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData
{
    public string Source { get; set; }
    public string Dialogue { get; set; }
    DialogueData( DialogueParser.DialogueEntry dialogueEntry )
    {
        Source = dialogueEntry.Source;
        Dialogue = dialogueEntry.Dialogue;
    }
}
