using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<Sound> sounds; // is used to choose music inside the editor
    Dictionary<SoundType, AudioClip> soundDict; // collection of game sounds

    [SerializeField] AudioSource audioSource;


    //------------SINGLETON----------

    public static AudioManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    //------------SETUP----------

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
