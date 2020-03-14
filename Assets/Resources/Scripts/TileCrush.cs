using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCrush : MonoBehaviour
{
    private Vector2 characterSize, boxSize;
    [SerializeField]
    private LayerMask targetMask;
    IDamageDealer dd = new DamageSystemPush();

    protected void Awake()
    {
        characterSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(characterSize.x*0.99f,0.5f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 boxCenter = (Vector2) transform.position + 0.5f * (characterSize.y + boxSize.y) * Vector2.down;
        if (Physics2D.OverlapBox(boxCenter, boxSize, 0f, targetMask))
        {
            Debug.Log("yes");
            var collision = other; 
            dd.DamageDealing(collision);
        }
    }
}
