using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Dictionary<SoundType, AudioClip> soundDict; // collection of game sounds
    [SerializeField] List<Sound> sounds;

    public static AudioManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

        InitSoundDictionary();
    }

    void InitSoundDictionary()
    {
        soundDict = new Dictionary<SoundType, AudioClip>();

        foreach (Sound sound in sounds)
        {
            soundDict.Add(sound.soundType, sound.audioClip);
        }
    }

    void PlaySound(SoundType soundType)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(soundDict[soundType]);
    }


    //------USING SOUNDS-------

    public void PlayCherryCollectedSound()
    {
        PlaySound(SoundType.CherryCollected);
    }

    public void PlayGemCollectedSound()
    {
        PlaySound(SoundType.GemCollected);
    }

    public void PlayHurtSound()
    {
        PlaySound(SoundType.Hurt);
    }

    public void PlayLevelUpSound()
    {
        PlaySound(SoundType.LevelUp);
    }
}
