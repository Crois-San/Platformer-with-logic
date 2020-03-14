using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//интерфейс очков здоровья
interface IHealthSystem
{
    void NpcDeath();
}

public class HealthSystem :  IHealthSystem
{
    public int HealthPoints { get; set; }
    public GameObject HealthyObject { get; set; }

    public HealthSystem(int healthPoints, GameObject healthyObject)
    {
        HealthPoints = healthPoints;
        HealthyObject = healthyObject;
    }

    public void setHealthPoints(int healthPoints)
    {
        HealthPoints = healthPoints;
    }

    public void NpcDeath()
    {
        setHealthPoints(HealthyObject.GetComponent<Character>().getHealthPoints);
        //действия, происходящие при значении очков здоровья <= 0
        //TODO: анимация смерти, возможно шейдерами; частицы. 
        if (HealthPoints <= 0)
        {
            GameObject.Destroy(HealthyObject);
        }
    }
}
public class HealthSystemWithShader :  IHealthSystem
{
    public int HealthPoints { get; set; }
    public GameObject HealthyObject { get; set; }
    public float Fade { get; set; }
    private Material shaderMaterial;
    private ISoundSystem ssDeath;
    private bool isNotPlaying = true;

    public HealthSystemWithShader(int healthPoints, GameObject healthyObject, float fade)
    {
        HealthPoints = healthPoints;
        HealthyObject = healthyObject;
        shaderMaterial = healthyObject.GetComponent<SpriteRenderer>().material;
        Fade = fade;
        ssDeath = new SoundSystemDefault(healthyObject,Sounds.DeathScore, 0.6f);
    }


    public void NpcDeath()
    {
        HealthPoints = HealthyObject.GetComponent<Character>().getHealthPoints;
        //действия, происходящие при значении очков здоровья <= 0
        //TODO: анимация смерти, возможно шейдерами; частицы. 
        if (HealthPoints <= 0)
        {
            if (isNotPlaying)
            {
                ssDeath.MakeSound();
                isNotPlaying = false;
            }
            if (Fade > 0)
            {
                
                Fade -= Time.deltaTime;
                shaderMaterial.SetFloat("_Fade",Fade);
            }
            else
            {
                
                GameObject.Destroy(HealthyObject);
                Fade = 0;
            }
        }
    }
}
