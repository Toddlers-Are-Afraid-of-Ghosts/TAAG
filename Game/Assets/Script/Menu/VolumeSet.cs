using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSet : MonoBehaviour
{
  
    public AudioMixer master;
    




    // Update is called once per frame
    public void SetVolume(float volume)
    {
        master.SetFloat("Master", volume);
    }

    public void SetMusic(float volume)
    {
        master.SetFloat("Music", volume);
    }
    
    public void SetSfx(float volume)
    {
        master.SetFloat("SFX", volume);
    }
}