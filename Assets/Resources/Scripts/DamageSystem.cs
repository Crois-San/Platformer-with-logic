using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

//интерфейс нанесения урона
interface IDamageDealer
{
    void DamageDealing(Collider2D collider);
}
//система нанесения урона, применяемая по умолчанию
public class DamageSystem : IDamageDealer
{
    private int DamagePoints { get; set; }
    private float KnockbackStrength { get; set; }
    private float DamageDelay { get; set; }
    private float lastDamage = -10f;


    public DamageSystem(int damagePoints, float knockbackStrength,float damageDelay)
    {
        DamagePoints = damagePoints;
        KnockbackStrength = knockbackStrength;
        DamageDelay = damageDelay;
    }

    //система нанесения урона, применяемая блоком, передвигаемым поршнем
    public void DamageDealing(Collider2D collider)
    {
        if(!collider) return;
        var victimGameObject = collider.gameObject;
        var victimCharacter = victimGameObject.GetComponent<Character>();
        if(!victimCharacter) return;
        if(Time.time < DamageDelay+lastDamage) return;
        victimCharacter.setHealthPoints = victimCharacter.getHealthPoints - DamagePoints;
        if(!victimGameObject.GetComponent<Rigidbody2D>()) return;
        var knockbackVector = new Vector2(victimCharacter.getMoving*-1,1);
        victimGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackVector*KnockbackStrength,ForceMode2D.Impulse);
        lastDamage = Time.time;
        Debug.Log(victimCharacter.getHealthPoints);
    }
}

public class DamageSystemPush : IDamageDealer
{
    public void DamageDealing(Collider2D collider)
    {
        if(!collider) return;
        var victimGameObject = collider.gameObject;
        GameObject.Destroy(victimGameObject);
    }
}
