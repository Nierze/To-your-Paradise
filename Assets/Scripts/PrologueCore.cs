using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;
using UnityEngine.SceneManagement;

public class PrologueCore : MonoBehaviour
{
    [Header("Scene Resources")]
    [SerializeField] private Volume prologueVolume;
    [SerializeField] private AnalogGlitchVolume analogVolume;
    [SerializeField] private DigitalGlitchVolume digitalVolume;

    [Header("Scene Texts")]
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI randomText1;
    [SerializeField] private TextMeshProUGUI randomText2;
    [SerializeField] private TextMeshProUGUI randomText3;
    [SerializeField] private TextMeshProUGUI randomText4;
    [SerializeField] private TextMeshProUGUI randomText5;

    [Header("Scene Choice Variables")]
    public bool choiceYes;



    private float typeSpeed;
    private float glitchDuration; 

    private void Start() 
    {
        prologueVolume.profile.TryGet(out analogVolume);
        prologueVolume.profile.TryGet(out digitalVolume);

        randomText1.text = "";
        randomText2.text = "";
        randomText3.text = "";
        randomText4.text = "";
        randomText5.text = "";
        typeSpeed = PlayerPrefs.GetFloat("TypingSpeed", 0.05f);  // Default value 0.05f if not set
        glitchDuration = PlayerPrefs.GetFloat("GlitchDuration", 0.2f);  // Default value 0.2f if not set
        TransitionCanvasHandler.Instance.FadeIn();
        StartCoroutine(MainGameLoop());
    }


private IEnumerator MainGameLoop()
{

    yield return Say("");

    yield return Say("Everything in the universe is in motion, ");
    yield return Say5("...but motion requires a mover.");
    randomText5.text = "";

    yield return Say("There must be a first mover that is not itself moved by anything else.");

    yield return Say("Every effect has a cause, ");
    yield return Say5("...and a chain of causes cannot go back infinitely.");
    randomText5.text = "";

    digitalVolume.intensity.value = 0.04f;

    yield return Say("Therefore, there must be a first uncaused cause that started everything.");

    yield return Say("Everything in the world is contingent, ");
    yield return Say5("...meaning it can either exist or not exist.");
    randomText5.text = "";

    analogVolume.colorDrift.value = 0.04f;

    yield return Say("Since things exist, there must be a necessary being whose existence is not contingent upon anything else.");

    yield return Say("We observe varying degrees of qualities like goodness, truth, and beauty in the world.");
    yield return Say5("These gradations imply a maximum, an ultimate source of perfection.");
    randomText5.text = "";

    yield return Say("The order and purpose found in nature suggest a design which points to an intelligent designer.");

    yield return Say("All that exist are a part of him.");

    yield return Say("For he is the source of all things.");

    analogVolume.verticalJump.value = 0.04f;

    yield return Say("To go against such a being...");
    yield return Say5("...is to go against the very foundations of this reality.");
    randomText5.text = "";

    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    AudioManager.Instance.PlaySFX("ArrowButtonPress");
    mainText.text = "";
    randomText1.text = "Even knowing so...";
    randomText2.text = "Even knowing so...";
    randomText3.text = "Even knowing so...";
    randomText4.text = "Even knowing so...";
    randomText5.text = "Even knowing so...";
    yield return TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f);
    yield return Say("Even knowing so...");

    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    AudioManager.Instance.PlaySFX("ArrowButtonPress");
    mainText.text = "";
    randomText1.text = "Would you go against such being?";
    randomText2.text = "Would you go against such being?";
    randomText3.text = "Would you go against such being?";
    randomText4.text = "Would you go against such being?";
    randomText5.text = "Would you go against such being?";
    yield return TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f);
    yield return Say("Would you go against such being?");
    
    // Spawn choices
    UserChoiceHandler.Instance.ChangeChoiceText(0, "Yes");
    UserChoiceHandler.Instance.ChangeChoiceText(1, "No");
    yield return UserChoiceHandler.Instance.ShowDialogueChoices(2);

    choiceYes = (UserChoiceHandler.Instance.GetUserChoice() == 1) ? true : false;

    mainText.text = "";
    randomText1.text = "";
    randomText2.text = "";
    randomText3.text = "";
    randomText4.text = "";
    randomText5.text = "";
    
    if (!choiceYes)
    {
        yield return badEnd();
    }
        
    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    AudioManager.Instance.PlaySFX("ArrowButtonPress");
    mainText.text = "";
    randomText1.text = "Even if it means going against everyone?";
    randomText2.text = "Even if it means going against everyone?";
    randomText3.text = "Even if it means going against everyone?";
    randomText4.text = "Even if it means going against everyone?";
    randomText5.text = "Even if it means going against everyone?";
    yield return TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f);
    yield return Say("Even if it means going against everyone?");

    // Spawn choices
    UserChoiceHandler.Instance.ChangeChoiceText(0, "Yes");
    UserChoiceHandler.Instance.ChangeChoiceText(1, "No");
    yield return UserChoiceHandler.Instance.ShowDialogueChoices(2);

    choiceYes = (UserChoiceHandler.Instance.GetUserChoice() == 1) ? true : false;

    if (!choiceYes)
    {
        yield return badEnd();
    }
    

    StartCoroutine(TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 1f, 1f, 1f, 1f, 0.01f));
    AudioManager.Instance.PlaySFX("ArrowButtonPress");
    mainText.text = "";
    randomText1.text = "Even if it means going against your beloved?";
    randomText2.text = "Even if it means going against your beloved?";
    randomText3.text = "Even if it means going against your beloved?";
    randomText4.text = "Even if it means going against your beloved?";
    randomText5.text = "Even if it means going against your beloved?";
    yield return TransitionUtils.ChangeAnalogGlitchVolume(analogVolume, 0.02f, 0.03f, 0.02f, 0.05f, 1f);
    yield return Say("Even if it means going against your beloved?");

    // Spawn choices
    UserChoiceHandler.Instance.ChangeChoiceText(0, "Yes");
    UserChoiceHandler.Instance.ChangeChoiceText(1, "No");
    yield return UserChoiceHandler.Instance.ShowDialogueChoices(2);

    choiceYes = (UserChoiceHandler.Instance.GetUserChoice() == 1) ? true : false;

    if (!choiceYes)
    {
        yield return badEnd();
    }

    AudioManager.Instance.PlaySFX("ArrowButtonPress");
    randomText1.text = "";
    randomText2.text = "";
    randomText3.text = "";
    randomText4.text = "";
    randomText5.text = "";
    yield return Say("Even if it means going against yourself?");

    // Spawn choices
    UserChoiceHandler.Instance.ChangeChoiceText(0, "Yes");
    UserChoiceHandler.Instance.ChangeChoiceText(1, "No");
    yield return UserChoiceHandler.Instance.ShowDialogueChoices(2);

    choiceYes = (UserChoiceHandler.Instance.GetUserChoice() == 1) ? true : false;

    if (!choiceYes)
    {
        yield return badEnd();
    }


    yield return Say("Very well...");
    yield return Say("May you find the paradise you sought.");
    yield return TransitionCanvasHandler.Instance.FadeOutAsynch();
    SceneManager.LoadScene("CoreTesting");

}


private IEnumerator badEnd() 
{
    randomText1.text = "";
    randomText2.text = "";
    randomText3.text = "";
    randomText4.text = "";
    randomText5.text = "";
    yield return Say("I see...");
    yield return Say("Such choice is nothing to be ashamed of.");
    yield return Say("Afterall...");
    yield return Say5("...man could never go against God");
    yield return TransitionCanvasHandler.Instance.FadeOutAsynch();
    SceneManager.LoadScene("MainMenu");
    
}
















    private IEnumerator Say(string dialogue)
    {
        yield return TypingUtils.TypeLineGlitch(mainText, dialogue, typeSpeed, 0, glitchDuration, 0.0f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }
    private IEnumerator Say5(string dialogue)
    {
        yield return TypingUtils.TypeLineGlitch(randomText5, dialogue, typeSpeed, 0, glitchDuration, 0.0f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }


    private IEnumerator SayMultiple(string dialogue)
    {
        yield return TypingUtils.TypeLineGlitch(mainText, dialogue, typeSpeed, 0, glitchDuration, 0.0f);
        yield return TypingUtils.TypeLineGlitch(randomText1, dialogue, typeSpeed, 0, glitchDuration, 0.0f);
        yield return TypingUtils.TypeLineGlitch(randomText2, dialogue, typeSpeed, 0, glitchDuration, 0.0f);
        yield return TypingUtils.TypeLineGlitch(randomText3, dialogue, typeSpeed, 0, glitchDuration, 0.0f);
        yield return TypingUtils.TypeLineGlitch(randomText4, dialogue, typeSpeed, 0, glitchDuration, 0.0f);
        yield return TypingUtils.TypeLineGlitch(randomText5, dialogue, typeSpeed, 0, glitchDuration, 0.0f);
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }
}
