using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio_Script : MonoBehaviour
{
    [SerializeField] bool isPlaying = true;
    [SerializeField] AudioSource radio;
    [SerializeField] int currentStation;
    [SerializeField] List<AudioClip> songs;

    void Start()
    {
        radio.clip = songs[currentStation];
        if (isPlaying) radio.Play();
    }

    // Toggles the radio on or off.
    public void toggleRadio()
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            radio.Play();
        }
        else
        {
            radio.Stop();
        }
    }

    // Changes the radio station by the given direction.
    public void changeStation(int direction)
    {
        if (isPlaying)
        {
            radio.Stop();
            currentStation += direction;
            if (currentStation < 0) currentStation = songs.Count - 1;
            if (currentStation == songs.Count) currentStation = 0;
            radio.clip = songs[currentStation];
            radio.Play();
        }
    }

}
