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
    private int HealthPoints { get; set; }
    private GameObject HealthyObject { get; set; } 

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
