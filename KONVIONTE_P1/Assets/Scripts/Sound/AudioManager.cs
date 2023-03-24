using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public UnityEvent PauseSound;
    public UnityEvent ResumeSound;

    static AudioManager _instance;
    static public AudioManager Instance
    {
        get { return _instance; }
    }
    public Sound[] sounds; 
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        PauseSound.AddListener(PauseSounds);
        ResumeSound.AddListener(ResumeSounds);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.group;
        }
    }
    /// <summary>
    /// Método para reproducir sonidos suscritos al AudioManager (Sonidos que no importe la tridimensionalidad)
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
    }
    public void PauseSounds()
    {
        foreach (Sound s in sounds)
        {
            if(s.pause) s.source.Pause();
        }
        //AudioSource[] Sounds = GetComponentsInChildren<AudioSource>();
        //foreach(AudioSource s in Sounds)
        //{
        //    s.Pause();
        //}
    }
    public void ResumeSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.UnPause();
        }
        //AudioSource[] Sounds = GetComponentsInChildren<AudioSource>();
        //foreach (AudioSource s in Sounds)
        //{
        //    s.UnPause();
        //}
    }
}
