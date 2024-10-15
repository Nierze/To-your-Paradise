using UnityEngine;

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