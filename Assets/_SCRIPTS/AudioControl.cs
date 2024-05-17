using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    public AudioSource backgroundMusic; // Drag your background music AudioSource here
    public AudioManager audioManager;   // Reference to your AudioManager

    public GameObject musicOnImage;     // Reference to the Image for music on state
    public GameObject musicOffImage;    // Reference to the Image for music off state

    public GameObject soundsOnImage;    // Reference to the Image for sounds on state
    public GameObject soundsOffImage;   // Reference to the Image for sounds off state

    //public GameObject particleEffect;   // Reference to the particle effect GameObject
    public GameObject particleEffectOnImage; // Image for particle effect on state
    public GameObject particleEffectOffImage; // Image for particle effect off state

    private bool isMusicMuted = false;
    private bool areSoundsMuted = false;
    [HideInInspector]
    public bool isParticleEffectMuted = false;

    private const string MusicMutedKey = "MusicMuted";
    private const string SoundsMutedKey = "SoundsMuted";
    private const string ParticleEffectMutedKey = "ParticleEffectMuted";

    void Start()
    {
        // Load saved states
        isMusicMuted = PlayerPrefs.GetInt(MusicMutedKey, 0) == 1;
        areSoundsMuted = PlayerPrefs.GetInt(SoundsMutedKey, 0) == 1;
        isParticleEffectMuted = PlayerPrefs.GetInt(ParticleEffectMutedKey, 0) == 1;

        // Apply loaded states
        if (isMusicMuted)
        {
            StopMusic();
        }
        else
        {
            PlayMusic();
        }
        audioManager.MuteAllSounds(areSoundsMuted);

        // Initialize button images
        UpdateMusicButtonImage();
        UpdateSoundsButtonImage();
        UpdateParticleEffectButtonImage();
    }

    public void ToggleMusicMute()
    {
        isMusicMuted = !isMusicMuted; // Toggle the state
        if (isMusicMuted)
        {
            StopMusic();
        }
        else
        {
            PlayMusic();
        }
        UpdateMusicButtonImage(); // Update the button image

        // Save state
        PlayerPrefs.SetInt(MusicMutedKey, isMusicMuted ? 1 : 0);
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

    public void PlayMusic()
    {
        backgroundMusic.Play();
    }

    public void ToggleSoundsMute()
    {
        areSoundsMuted = !areSoundsMuted; // Toggle the state
        audioManager.MuteAllSounds(areSoundsMuted); // Mute all sounds
        UpdateSoundsButtonImage(); // Update the button image

        // Save state
        PlayerPrefs.SetInt(SoundsMutedKey, areSoundsMuted ? 1 : 0);
    }

    public void ToggleParticleEffectMute()
    {
        isParticleEffectMuted = !isParticleEffectMuted; // Toggle the state
        UpdateParticleEffectButtonImage(); // Update the button image

        // Save state
        PlayerPrefs.SetInt(ParticleEffectMutedKey, isParticleEffectMuted ? 1 : 0);
    }

    private void UpdateMusicButtonImage()
    {
        musicOnImage.SetActive(!isMusicMuted);
        musicOffImage.SetActive(isMusicMuted);
    }

    private void UpdateSoundsButtonImage()
    {
        soundsOnImage.SetActive(!areSoundsMuted);
        soundsOffImage.SetActive(areSoundsMuted);
    }

    private void UpdateParticleEffectButtonImage()
    {
        particleEffectOnImage.SetActive(!isParticleEffectMuted);
        particleEffectOffImage.SetActive(isParticleEffectMuted);
    }
}
