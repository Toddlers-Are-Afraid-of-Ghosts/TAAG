using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using Random = System.Random;

public class ManagerSfx : MonoBehaviour
{
    public List<AudioClip> playerHitClips;

    public AudioClip stickDeath,
        ghostDeath,
        mickeyDeath,
        pickupCoin,
        pickupItem,
        walk,
        lowHealth,
        bossDeath,
        dreapDeath;

    public GameObject original, parent;
    private static AudioSource audioSrc;
    public static List<AudioClip> playerHitClipsst;
    private static List<GameObject> allaudio;
    public static GameObject originalst, parentst;

    public static AudioClip stickDeathst,
        ghostDeathst,
        mickeyDeathst,
        pickupCoinst,
        pickupItemst,
        walkst,
        lowHealthst,
        bossDeathst,
        dreapDeathst;

    // Start is called before the first frame update
    void Start()
    {
        allaudio = new List<GameObject>();
        audioSrc = GetComponent<AudioSource>();
        playerHitClipsst = new List<AudioClip>();
        foreach (var audio in playerHitClips)
        {
            playerHitClipsst.Add(audio);
        }

        stickDeathst = stickDeath;
        ghostDeathst = ghostDeath;
        mickeyDeathst = mickeyDeath;
        pickupCoinst = pickupCoin;
        pickupItem = pickupItemst;
        walkst = walk;
        lowHealthst = lowHealth;
        bossDeathst = bossDeath;
        dreapDeathst = dreapDeath;
        originalst = original;
        parentst = parent;
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < allaudio.Count)
        {
            var sfx = allaudio[i];
            var comp = sfx.GetComponent<CreateSfx>();

            if (comp.AudioSource.time >= comp.AudioSource.clip.length)
            {
                allaudio.Remove(sfx);
                Destroy(sfx);
                Debug.Log("Destroy");
            }
            else
            {
                i++;
            }
        }
    }


    public static void PlaySound(string clip)
    {
        GameObject obj;
        CreateSfx sfx;
        AudioSource source;
        switch (clip)
        {
            case "playerHit":

                obj = Instantiate(originalst, parentst.transform);
                obj.name = clip;
                obj.AddComponent<CreateSfx>();
                sfx = obj.GetComponent<CreateSfx>();
                source = obj.AddComponent<AudioSource>();
                sfx.Setup(source, playerHitClipsst[new Random().Next(0, playerHitClipsst.Count)], "playerHit",
                    audioSrc.outputAudioMixerGroup);
                sfx.Play();
                allaudio.Add(obj);
                break;
            case "stick":
                obj = Instantiate(originalst, parentst.transform);
                obj.name = clip;

                obj.AddComponent<CreateSfx>();
                sfx = obj.GetComponent<CreateSfx>();
                source = obj.AddComponent<AudioSource>();
                sfx.Setup(source, stickDeathst, clip,
                    audioSrc.outputAudioMixerGroup);
                sfx.Play();
                allaudio.Add(obj);
                break;
            case "ghost":
                obj = Instantiate(originalst, parentst.transform);
                obj.name = clip;
                obj.AddComponent<CreateSfx>();
                sfx = obj.GetComponent<CreateSfx>();
                source = obj.AddComponent<AudioSource>();
                sfx.Setup(source, ghostDeathst, clip,
                    audioSrc.outputAudioMixerGroup);
                sfx.Play();
                allaudio.Add(obj);
                break;
            case "mickey":
                obj = Instantiate(originalst, parentst.transform);
                obj.name = clip;
                obj.AddComponent<CreateSfx>();
                sfx = obj.GetComponent<CreateSfx>();
                source = obj.AddComponent<AudioSource>();
                sfx.Setup(source, mickeyDeathst, clip,
                    audioSrc.outputAudioMixerGroup);
                sfx.Play();
                allaudio.Add(obj);
                break;
            case "coin":
                obj = Instantiate(originalst, parentst.transform);
                obj.name = clip;
                obj.AddComponent<CreateSfx>();
                sfx = obj.GetComponent<CreateSfx>();
                source = obj.AddComponent<AudioSource>();
                sfx.Setup(source, pickupCoinst, clip,
                    audioSrc.outputAudioMixerGroup);
                sfx.Play();
                allaudio.Add(obj);
                break;
            case "item":
                obj = Instantiate(originalst, parentst.transform);
                obj.name = clip;
                obj.AddComponent<CreateSfx>();
                sfx = obj.GetComponent<CreateSfx>();
                source = obj.AddComponent<AudioSource>();
                sfx.Setup(source, pickupItemst, clip,
                    audioSrc.outputAudioMixerGroup);
                sfx.Play();
                allaudio.Add(obj);
                break;
            case "playerWalk":
                if (!AllReadyPlay("playerWalk"))
                {
                    obj = Instantiate(originalst, parentst.transform);
                    obj.name = clip;
                    obj.AddComponent<CreateSfx>();
                    source = obj.AddComponent<AudioSource>();
                    sfx = obj.GetComponent<CreateSfx>();
                    sfx.Setup(source, walkst, clip, audioSrc.outputAudioMixerGroup);
                    sfx.Play();
                    allaudio.Add(obj);
                }

                break;
            case "Health":
                if (!AllReadyPlay("Health"))
                {
                    obj = Instantiate(originalst, parentst.transform);
                    obj.AddComponent<CreateSfx>();
                    obj.name = clip;
                    source = obj.AddComponent<AudioSource>();
                    sfx = obj.GetComponent<CreateSfx>();
                    sfx.Setup(source, lowHealthst, clip, audioSrc.outputAudioMixerGroup);
                    sfx.Play();
                    allaudio.Add(obj);
                }

                break;
            case "boss":
                obj = Instantiate(originalst, parentst.transform);
                obj.name = clip;
                obj.AddComponent<CreateSfx>();
                sfx = obj.GetComponent<CreateSfx>();
                source = obj.AddComponent<AudioSource>();
                sfx.Setup(source, bossDeathst, clip,
                    audioSrc.outputAudioMixerGroup);
                sfx.Play();
                allaudio.Add(obj);
                break;
            default:
                audioSrc.Stop();
                break;
        }
    }

    public static bool AllReadyPlay(string name)
    {
        foreach (var sfx in allaudio)
        {
            if (sfx.GetComponent<CreateSfx>().Name == name)
                return true;
        }

        return false;
    }
}

public class CreateSfx : MonoBehaviour
{
    protected string name;
    protected AudioSource _audioSource;
    public string Name => name;


    public AudioSource AudioSource => _audioSource;


    public void Setup(AudioSource audioSource, AudioClip audioClip, string name, AudioMixerGroup audioMixerGroup)
    {
        this.name = name;
        _audioSource = audioSource;
        _audioSource.clip = audioClip;
        _audioSource.outputAudioMixerGroup = audioMixerGroup;
    }

    private void Update()
    {
        // Debug.Log($"{this.Name}: {this.AudioSource.time}/{this.AudioSource.clip.length}");
    }

    public void Play()
    {
        _audioSource.Play();
    }
}