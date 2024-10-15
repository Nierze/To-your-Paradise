using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioSource AMBIENCE;
    [SerializeField] private List<AudioFile> audioFiles = new List<AudioFile>();

    [SerializeField] private float fadeDuration = 2f; // Duration for fading in/out

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BGM.clip = GetAudioClip("BGM");
        AMBIENCE.clip = GetAudioClip("AMBIENCE");
        BGM.Play();
        AMBIENCE.Play();
    }

    public void PlaySFX(string name)
    {
        SFX.clip = GetAudioClip(name);
        SFX.Play();
    }

    public void PlayBGM(string name)
    {
        BGM.clip = GetAudioClip(name);
        BGM.Play();
    }

    public void PlayAMBIENCE(string name)
    {
        AMBIENCE.clip = GetAudioClip(name);
        AMBIENCE.Play();
    }

    public void StopSFX()
    {
        SFX.Stop();
    }

    public void StopBGM()
    {
        BGM.Stop();

    }

    public void StopAMBIENCE()
    {
        AMBIENCE.Stop();
    }

    public void StopAllAudio()
    {
        StopSFX();
        StopBGM();
        StopAMBIENCE();
    }

    public void PlayBGMWithFade(string name)
    {
        StartCoroutine(FadeOutAndInBGM(name));
    }

    private IEnumerator FadeOutAndInBGM(string newBgmName)
    {
        // Fade out current BGM
        float startVolume = BGM.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            BGM.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        BGM.volume = 0;
        BGM.Stop();

        // Change to new BGM
        BGM.clip = GetAudioClip(newBgmName);
        BGM.Play();

        // Fade in new BGM
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            BGM.volume = Mathf.Lerp(0, startVolume, t / fadeDuration);
            yield return null;
        }
        BGM.volume = startVolume;
    }

    public void InitializeAudioFiles(List<AudioFile> files)
    {
        foreach (AudioFile audioFile in files)
        {
            audioFiles.Add(audioFile);
        }
    }

    private AudioClip GetAudioClip(string name)
    {
        foreach (AudioFile audioFile in audioFiles)
        {
            if (audioFile.Name == name)
            {
                return audioFile.Clip;
            }
        }
        return null;
    }

    // [System.Serializable]
    // public class AudioFile
    // {
    //     public string Name;
    //     public AudioClip Clip;

    //     public AudioFile(string name, AudioClip clip)
    //     {
    //         Name = name;
    //         Clip = clip;
    //     }
    // }
}
