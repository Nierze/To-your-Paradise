using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCore2 : DialogueCore
{
    protected override IEnumerator MainGameLoop()
    {
        StartCoroutine(TransitionCanvasHandler.Instance.FadeInAsynch());
        yield return StartCoroutine(Say(characters[0], "hello2"));
        yield return StartCoroutine(Say(characters[0], "Testing2"));
        yield return StartCoroutine(Say(characters[0], "Seems to be working"));


        backgroundImage.sprite = backgroundImages[0];

        yield return StartCoroutine(Say(characters[0], 
            "Iorep ipsum dolor nictaris, lorem quentis flectori vimus ordantia." +
            "Fustra lactis fabrio, venatis quirom disparet telluma." +
            "Comvolan dictrae singulum torpus vivendos flectora mirandes." +
            "Quisque ornatus laminaris tortaros praelectum curavit languores."));
    }
}
