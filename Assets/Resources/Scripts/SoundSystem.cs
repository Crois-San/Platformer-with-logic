using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//интерфейс управления звуком
public interface ISoundSystem
{
    void MakeSound();
    void StopSound();
    void ChangeSound(Sounds sounds);
}
public class SoundSystemDefault : ISoundSystem
 {
     public GameObject SourceObject { get; set; }
     public Character ObjectScript { get; set; }
     public AudioSource SoundSource { get; set; } 
     
     public SoundSystemDefault(GameObject sourceObject, Sounds sounds, float volume)
     {
         SourceObject = sourceObject;
         SoundSource = SourceObject.AddComponent<AudioSource>();
         SoundSource.clip = sounds.Sound;
         SoundSource.volume = volume;
         SoundSource.spatialize = true;
         SoundSource.spatialBlend = 0.5f;
         SoundSource.maxDistance = 16;
         SoundSource.outputAudioMixerGroup = Resources.Load<AudioMixer>("MasterMixer").FindMatchingGroups("Master")[0];

     }
     public void MakeSound()
     {
         SoundSource.Play();
     }
     public void StopSound()
     {
         SoundSource.Stop();
     }

     public void ChangeSound(Sounds sounds)
     {
         throw new System.NotImplementedException();
     }
 }
public class SoundSystemDefaultLooping : ISoundSystem
{
    public GameObject SourceObject { get; set; }
    public Character ObjectScript { get; set; }
    public AudioSource SoundSource { get; set; } 
     
    public SoundSystemDefaultLooping(GameObject sourceObject, Sounds sounds, float volume)
    {
        SourceObject = sourceObject;
        SoundSource = SourceObject.AddComponent<AudioSource>();
        SoundSource.clip = sounds.Sound;
        SoundSource.volume = volume;
        SoundSource.spatialize = true;
        SoundSource.spatialBlend = 0.75f;
        SoundSource.maxDistance = 32;
        SoundSource.loop = true;
        SoundSource.outputAudioMixerGroup = Resources.Load<AudioMixer>("MasterMixer").FindMatchingGroups("Master")[0];

    }
    public void MakeSound()
    {
        SoundSource.Play();
    }
    public void StopSound()
    {
        SoundSource.Stop();
    }

    public void ChangeSound(Sounds sounds)
    {
        throw new System.NotImplementedException();
    }
    
}

public class SoundSystemWalking : ISoundSystem
{
    public GameObject SourceObject { get; set; }
    public Character ObjectScript { get; set; }
    public AudioSource SoundSource { get; set; } 
    public float ObjectSpeed { get; set; }
    private bool isPlaying;

    public SoundSystemWalking(GameObject sourceObject)
    {
        SourceObject = sourceObject;
        ObjectScript = SourceObject.GetComponent<Character>();
        ObjectSpeed = ObjectScript.speed;
        SoundSource = SourceObject.AddComponent<AudioSource>();
        SoundSource.clip = Resources.Load<AudioClip>("Sounds/Footstep");
        SoundSource.volume = 0.6f;
        SoundSource.loop = true;
        SoundSource.outputAudioMixerGroup = Resources.Load<AudioMixer>("MasterMixer").FindMatchingGroups("Master")[0];
        SoundSource.Play();
    }
    public void MakeSound()
    {
        var SoundSpeed = 1+ObjectScript.speed;
        SoundSource.pitch = Mathf.Clamp(SoundSpeed,0.9f,1.5f) ;
        isPlaying = Mathf.Abs(ObjectScript.getMoving) > 0 && ObjectScript.getGrounded;
        if (isPlaying)
        {
            SoundSource.UnPause();
        }
        else
        {
            SoundSource.Pause();
        }
    }
    public void StopSound()
    {
        SoundSource.Stop();
    }

    public void ChangeSound(Sounds sounds)
    {
        throw new System.NotImplementedException();
    }
}
public class SoundSystemAmbient : ISoundSystem
{
    public GameObject SourceObject { get; set; }
    public Character ObjectScript { get; set; }
    public AudioSource SoundSource { get; set; } 
     
    public SoundSystemAmbient(GameObject sourceObject, float volume)
    {
        SourceObject = sourceObject;
        SoundSource = SourceObject.AddComponent<AudioSource>();
        SoundSource.volume = volume;
        SoundSource.outputAudioMixerGroup = Resources.Load<AudioMixer>("MasterMixer").FindMatchingGroups("Master")[0];
         
    }
    public void MakeSound()
    {
        
        //SoundSource.Play();
        SoundSource.PlayOneShot(SoundSource.clip);
    }

    public void StopSound()
    {
        SoundSource.Stop();
    }

    public void ChangeSound(Sounds sounds)
    {
        SoundSource.clip = sounds.Sound;
    }
}
