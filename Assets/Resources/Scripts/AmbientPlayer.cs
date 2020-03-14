using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmbientPlayer : MonoBehaviour
{
    private ISoundSystem ssAmbient;
    private Sounds ambient;
    
    public void AmbientPlay()
    {
        if (Random.Range(0f, 500f) <= 0.5f)
        {
            if (Random.Range(0f, 1f) >= 0.5f)
            {
                ambient = Sounds.DarkSoundScape1;
            }
            else
            {
                ambient = Sounds.DarkSoundScape2;
            }
            ssAmbient.ChangeSound(ambient);
            ssAmbient.MakeSound();
        }

    }

    public void Start()
    {
        ssAmbient = new SoundSystemAmbient(gameObject,0.1f);
    }

    public void Update()
    {
        AmbientPlay();
    }
}
