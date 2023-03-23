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
        GameManager.Instance._pauseSound.AddListener(PauseSource);
        GameManager.Instance._resumeSound.AddListener(ResumeSource);
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
