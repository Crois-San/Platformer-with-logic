using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCrush : Character
{
    private Vector2 characterSize, boxSize;
    protected override void DamageDealing(Collider2D collider)
    {
        if(!collider) return;
        var victimGameObject = collider.gameObject;
        var victimCharacter = victimGameObject.GetComponent<Character>();
        damagePoints = victimCharacter.getHealthPoints;
        if(!victimCharacter) return;
        victimCharacter.setHealthPoints = victimCharacter.getHealthPoints - damagePoints;
        Debug.Log(victimCharacter.getHealthPoints);
    }

    protected override void Awake()
    {
        characterSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(characterSize.x*0.99f,0.5f);
    }

    protected override void Update()
    {
    }

    protected override void FixedUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 boxCenter = (Vector2) transform.position + 0.5f * (characterSize.y + boxSize.y) * Vector2.down;
        if (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask))
        {
            Debug.Log("yes");
            var collider = other.collider; 
            DamageDealing(collider);
        }
    }
}
