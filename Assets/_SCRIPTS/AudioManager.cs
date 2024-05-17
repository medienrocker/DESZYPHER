using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundClip
    {
        public string name;
        public AudioClip clip;
    }

    public SoundClip[] sounds;

    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();
    private AudioSource audioSource;
    private bool isMuted = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        foreach (SoundClip sound in sounds)
        {
            soundDictionary.Add(sound.name, sound.clip);
        }
    }

    public void PlaySound(string name)
    {
        if (!isMuted && soundDictionary.ContainsKey(name))
        {
            audioSource.PlayOneShot(soundDictionary[name]);
        }
        else if (!soundDictionary.ContainsKey(name))
        {
            Debug.LogWarning("Sound name not found in dictionary: " + name);
        }
    }

    public void MuteAllSounds(bool mute)
    {
        isMuted = mute;
        audioSource.mute = mute;
    }
}
