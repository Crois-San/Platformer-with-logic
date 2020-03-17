using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetSettingVolume(float volume)
    {
        mixer.SetFloat("Volume", volume);
    }

    public void MuteVolume(bool isMuted)
    {
        if (isMuted)
        {
            mixer.SetFloat("Volume", -80);
        }
        else
        {
            mixer.SetFloat("Volume", 0);
        }
    }
}
