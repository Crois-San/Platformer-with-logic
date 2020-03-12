using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//интерфейс управления звуком
interface ISoundSystem
{
    void MakeSound();
}
/*
 * Звуки ходьбы
 * Звуки при получении урона
 * Звуки при смерти персонажа
 * Звуки переключения входов
 * Звуки включенных элементов
 * Звук движения поршня
 * Звук эмбиента
 */
public class SoundSystemDefault : ISoundSystem
 {
     public void MakeSound()
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
    public IEnumerator fadeSound { get; set; }

    public SoundSystemWalking(GameObject sourceObject)
    {
        SourceObject = sourceObject;
        ObjectScript = SourceObject.GetComponent<Character>();
        ObjectSpeed = ObjectScript.speed;
        SoundSource = SourceObject.AddComponent<AudioSource>();
        SoundSource.clip = Resources.Load<AudioClip>("Sounds/Footstep");
        SoundSource.volume = 0.6f;
        SoundSource.loop = true;
        SoundSource.Play();
    }
    public void MakeSound()
    {
        var SoundSpeed = 1+ObjectScript.speed;
        SoundSource.pitch = Mathf.Clamp(SoundSpeed,0.9f,1.5f) ;
        if (Mathf.Abs(ObjectScript.getMoving) > 0 && ObjectScript.getGrounded)
        {
            SoundSource.UnPause();
        }
        else
        {
            SoundSource.Pause();
            //fadeSound = FadeOut(SoundSource, 0.5f);
            //StartCoroutine (fadeSound);
            //StopCoroutine (fadeSound);
            
        }
    }

    private static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Pause();
        audioSource.volume = startVolume;
    }
}
