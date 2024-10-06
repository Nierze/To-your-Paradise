using System.Collections;
using UnityEngine;

public class TestDialogueCore : DialogueCore
{
    protected override IEnumerator MainGameLoop()
    {
        yield return StartCoroutine(Say(characters[0], "hello"));
        yield return StartCoroutine(Say(characters[0], "Testing"));
        yield return StartCoroutine(Say(characters[0], "Seems to be working"));
        yield return StartCoroutine(Say(characters[0], 
            "Iorep ipsum dolor nictaris, lorem quentis flectori vimus ordantia." +
            "Fustra lactis fabrio, venatis quirom disparet telluma." +
            "Comvolan dictrae singulum torpus vivendos flectora mirandes." +
            "Quisque ornatus laminaris tortaros praelectum curavit languores."));
    }


}
