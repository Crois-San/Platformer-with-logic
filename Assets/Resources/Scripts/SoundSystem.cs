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
