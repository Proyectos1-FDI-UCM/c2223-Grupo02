using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    AudioSource _myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _myAudioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PauseSound.AddListener(PauseSource);
        AudioManager.Instance.ResumeSound.AddListener(ResumeSource);
    }
    private void PauseSource()
    {
        _myAudioSource.Pause();
    }
    private void ResumeSource()
    {
        _myAudioSource.UnPause();
    }
}
