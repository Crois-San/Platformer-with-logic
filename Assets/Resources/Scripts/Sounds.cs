using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Звуки ходьбы
 * Звук прыжка
 * Звук при получении урона
 * Звук при смерти персонажа
 * Звук переключения входов
 * Звук включенных элементов
 * Звук движения поршня
 * Звук эмбиента
 * Звук победы
 */
public class Sounds
{
    public AudioClip Sound { get; set; }
    public Sounds(AudioClip sound)
    {
        Sound = sound;
    }
    public static Sounds Footstep
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/Footstep")); }
    }
    public static Sounds DamageHit
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/DamageHit")); }
    }
    public static Sounds DarkSoundScape1
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/DarkSoundScape1")); }
    }
    public static Sounds DarkSoundScape2
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/DarkSoundScape2")); }
    }
    public static Sounds BossFootsteps
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/BossFootsteps")); }
    }
    public static Sounds DeathScore
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/DeathScore")); }
    }
    public static Sounds InputChange
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/InputChange")); }
    }
    public static Sounds LogicalElementBuzz
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/LogicalElementBuzz")); }
    }
    public static Sounds PistonMovement
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/PistonMovement")); }
    }
    public static Sounds VictoryScore
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/VictoryScore")); }
    }
    public static Sounds JumpSound
    {
        get { return new Sounds(Resources.Load<AudioClip>("Sounds/JumpSound")); }
    }

}
