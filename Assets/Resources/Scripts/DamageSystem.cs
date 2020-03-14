using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

//интерфейс нанесения урона
interface IDamageDealer
{
    void DamageDealing(Collision2D collider);
}
//система нанесения урона, применяемая по умолчанию
public class DamageSystem : IDamageDealer
{
    public int DamagePoints { get; set; }
    public float KnockbackStrength { get; set; }
    public float DamageDelay { get; set; }
    public int CollisionCount { get; set; }
    public GameObject DamagingObject { get; set; }
    public ISoundSystem ssHit;
    private float lastDamage = -10f;
    private bool isKnockedBack;
    


    public DamageSystem(GameObject damagingObject,int damagePoints, float knockbackStrength,float damageDelay)
    {
        DamagingObject = damagingObject;
        DamagePoints = damagePoints;
        KnockbackStrength = knockbackStrength;
        DamageDelay = damageDelay;
        ssHit = new SoundSystemDefault(damagingObject,Sounds.DamageHit, 0.6f);
    }

    //система нанесения урона, применяемая блоком, передвигаемым поршнем
    public void DamageDealing(Collision2D collision)
    {
        if(collision == null) return;
        CollisionCount = DamagingObject.GetComponent<Enemy>().CollisionCount;
        var victimGameObject = collision.collider.gameObject;
        var victimCharacter = victimGameObject.GetComponent<Character>();
        if(!victimCharacter) return;
        if((Time.time < DamageDelay+lastDamage) || CollisionCount <= 1) return;
        victimCharacter.setHealthPoints = victimCharacter.getHealthPoints - DamagePoints;
        if(!victimGameObject.GetComponent<Rigidbody2D>()) return;
        var knockbackVector = new Vector2(victimCharacter.getMoving*-1,0.6f);
        victimGameObject.GetComponent<Rigidbody2D>().velocity = knockbackVector*KnockbackStrength;
        isKnockedBack = true;
        if (victimCharacter.getGrounded && !isKnockedBack)
        { 
            victimGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            isKnockedBack = false;    
        }
        ssHit.MakeSound();
        lastDamage = Time.time;
        Debug.Log(victimCharacter.getHealthPoints);
    }
}

public class DamageSystemPush : IDamageDealer
{
    public void DamageDealing(Collision2D collision)
    {
        if(collision == null) return;
        var victimGameObject = collision.collider.gameObject;
        GameObject.Destroy(victimGameObject);
    }
}
