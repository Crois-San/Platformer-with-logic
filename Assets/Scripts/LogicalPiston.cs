using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalPiston : LogicalElement
{
    private SpriteRenderer sr;

    [SerializeField]
    private Sprite on;

    [SerializeField]
    private Sprite off;

    [SerializeField]
    LogicalElement le;

    private GameObject pulledObject;
    private Vector2 rayStart;
    private bool isConnected;
    [SerializeField]
    private LayerMask m;
    [Range(1,5)] [SerializeField]
    private float pushDistance;

    private Vector2 pulledObjPos;
    
    void PistonAction(LogicalElement A)
    {
        state = A.state;
        isConnected = Physics2D.Raycast(rayStart,Vector2.down,5f,m);

        if (isConnected)
        {
            
            if (state)
            {
                pulledObject.transform.position = new Vector2(pulledObjPos.x,pulledObjPos.y-pushDistance);
            }
            else
            {
                pulledObject.transform.position = new Vector2(pulledObjPos.x,pulledObjPos.y);
            }
        }
    }
    
    private void Start()
    {
        rayStart = this.gameObject.transform.position;
        isConnected = Physics2D.Raycast(rayStart,Vector2.down,5f,m);
        if (isConnected)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayStart,Vector2.down,5f,m);
            pulledObjPos = hit.transform.position;
            pulledObject = hit.collider.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PistonAction(le);
//        sr = GetComponent<SpriteRenderer>();
//        SpriteChange(off, on, sr);
    }
    
    
}
