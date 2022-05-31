using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class MusicSelecteur : MonoBehaviour
{
    public AudioSource actualClip;
    public List<AudioClip> allMusic = new List<AudioClip>();
    private int nextClip;

    private AudioClip actualMusic;

    // Start is called before the first frame update
    void Start()
    {
        nextClip = Random.Range(0, allMusic.Count);
        actualClip.clip = allMusic[nextClip];
        actualClip.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{actualClip.time} / {actualClip.clip.length}");
        if (actualClip.time >= actualClip.clip.length)
        {
            var music = allMusic[nextClip];
            while (music == actualClip.clip)
            {
                nextClip = Random.Range(0, allMusic.Count);
                music = allMusic[nextClip];
            }

            ChangeMusic(music);
            nextClip = Random.Range(0, allMusic.Count);
        }
    }

    public void ChangeMusic(AudioClip change)
    {
        actualClip.Stop();
        actualClip.clip = change;
        actualClip.Play();
    }
}