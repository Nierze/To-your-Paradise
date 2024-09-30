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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    [System.Serializable]
    public class AudioFile
    {
        public string Name;
        public AudioClip Clip;

        public AudioFile(string name, AudioClip clip)
        {
            Name = name;
            Clip = clip;
        }
    }
}
