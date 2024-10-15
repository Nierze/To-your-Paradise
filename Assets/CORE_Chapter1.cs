using System.Collections; 
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;
using UnityEngine.SceneManagement;


public class CORE_Chapter1 : DialogueCore
{
    [Header("Scene Resources")]
    [SerializeField] private AnalogGlitchVolume analogVolume;
    [SerializeField] private DigitalGlitchVolume digitalVolume;

    private void Start() 
    {
        volume.profile.TryGet(out analogVolume);
        volume.profile.TryGet(out digitalVolume);
        TransitionCanvasHandler.Instance.FadeIn();
    }
protected override IEnumerator MainGameLoop()
{
    AudioManager.Instance.StopAllAudio();
    StartCoroutine(TransitionCanvasHandler.Instance.FadeInAsynch());
    AudioManager.Instance.PlayAMBIENCE("WindAmbience");
    backgroundImage.sprite = backgroundImages[0];

    yield return Say(characters[0], "How many years has it been?");
    yield return Say(characters[0], "How many years have I wandered this endless, desolate road, the same weary steps over and over again?");
    yield return Say(characters[0], "It’s all the same…");
    yield return Say(characters[0], "No matter where I turn, the scenery never changes—");

    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    backgroundImage.sprite = backgroundImages[9];
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f));

    yield return Say(characters[0], "The corpse of a dead world.");
    yield return Say(characters[0], "But this time…");
    yield return Say(characters[0], "this time is different.");
    yield return Say(characters[0], "I can feel it.");
    yield return Say(characters[0], "The Genesis Point is near.");
    yield return Say(characters[0], "I was the last one…");
    yield return Say(characters[0], "The last survivor entrusted with reaching this place.");
    yield return Say(characters[0], "The others…");
    yield return Say(characters[0], "…they didn’t make it.");

    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    backgroundImage.sprite = backgroundImages[10];
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f));

    yield return Say(characters[0], "The Genesis Point. It was first discovered 15 years ago, just a whisper of hope in a hopeless world.");
    yield return Say(characters[0], "When the war ended, it left everything in ruins.");
    yield return Say(characters[0], "Cities crumbled into ash, the sky itself seemed to burn, and the earth was scarred beyond recognition.");
    yield return Say(characters[0], "There were no victors.");
    yield return Say(characters[0], "Only casualties.");
    yield return Say(characters[0], "The survivors were left to fend for themselves, scattered across the wasteland like lost embers, clinging desperately to life.");
    yield return Say(characters[0], "Some descended into madness, turning on each other for the smallest scraps of food.");
    yield return Say(characters[0], "We were once capable of so much…");
    yield return Say(characters[0], "…but the war showed us how quickly humanity could be reduced to desperation and cruelty.");
    yield return Say(characters[0], "But all of that happened before I was even born.");
    yield return Say(characters[0], "Now, the world is nothing but a silent, suffocating graveyard.");
    yield return Say(characters[0], "The air burns in your throat, and thick, choking clouds of dust swirl endlessly across the barren landscape.");
    
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    backgroundImage.sprite = backgroundImages[8];
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f));
    
    
    yield return Say(characters[0], "I was part of a small group—just a handful of people who managed to scrape together enough equipment to survive.");
    yield return Say(characters[0], "We were hardly more than a ragtag bunch, but we had each other, and for a while, that was enough.");
    yield return Say(characters[0], "We knew our days were numbered.");
    yield return Say(characters[0], "Supplies ran out faster than we could scavenge, and every day, the clock ticked down, reminding us that we had maybe 8 years left, if we were lucky.");
    yield return Say(characters[0], "One day, the head navigator noticed something strange.");
    yield return Say(characters[0], "The navigational equipment, which had been silent for so long, suddenly began to blink, fixated on a single point on the map.");
    
    
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    backgroundImage.sprite = backgroundImages[12];
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f));
    
    
    yield return Say(characters[0], "At first, we thought it was a glitch, but it kept blinking, as if calling out to us.");
    yield return Say(characters[0], "A signal.");
    yield return Say(characters[0], "A faint, flickering signal.");
    yield return Say(characters[0], "It wasn’t much, but in a world as dead as this, even a whisper was enough to make us hope.");
    yield return Say(characters[0], "Maybe it was a shelter, a bunker, or even a trace of other survivors.");
    yield return Say(characters[0], "We didn’t know what it was, but we called it the Genesis Point.");
    yield return Say(characters[0], "A new beginning.");
    yield return Say(characters[0], "And so, we set out, clinging to that fragile hope.");

    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    backgroundImage.sprite = backgroundImages[1];
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f));

    yield return Say(characters[0], "I was just a child when the journey began.");
    yield return Say(characters[0], "We walked for days, weeks, years…");
    yield return Say(characters[0], "…until the days blurred together, and the faces I knew began to disappear, one by one.");
    yield return Say(characters[0], "Now, I’m the only one left.");
    yield return Say(characters[0], "The last of us.");
    yield return Say(characters[0], "And here I am, still walking this road, still following that signal.");
    yield return Say(characters[0], "Each step echoes in the emptiness, a reminder that I am alone in a world stripped of life and laughter.");
    yield return Say(characters[0], "I keep moving forward, driven by the hope that maybe, just maybe, the Genesis Point holds the answers I seek—or the solace I crave.");
    
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    backgroundImage.sprite = backgroundImages[2];
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f));
    
    yield return Say(characters[0], "But as the dust storms howl around me, I can’t shake the feeling that the only thing waiting for me is the cold embrace of despair.");
    yield return Say(characters[0], "I am the last thread in a tapestry unraveled…");
    yield return Say(characters[0], "…a solitary soul lost in the echoes of a dying world.");

    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 4f));

    yield return Say(characters[0], "<<DUE TO LACK OF RESOURCES AND ASSETS, THE GAME STOPS HERE>>");
    yield return Say(characters[0], "For now...");
    yield return Say(characters[0], "Thank you for playing the demo!");
    

    yield return TransitionCanvasHandler.Instance.FadeOutAsynch();
    SceneManager.LoadScene("AnimatedSplashScreen");

}

}
