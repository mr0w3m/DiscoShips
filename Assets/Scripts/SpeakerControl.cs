using UnityEngine;
using System.Collections;

public class SpeakerControl : MonoBehaviour {

    public ParticleSystem[] waves;
    public ParticleSystem[] notes;
    public Animator[] animators;

    public bool mapSelectScreen;

    void Start()
    {
        if (mapSelectScreen)
        {
            TurnOnSpeakers();
        }
    }
    
    public void TurnOnSpeakers()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].Play();
        }

        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].Play();
        }

        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].enabled = true;
        }
    }
}
