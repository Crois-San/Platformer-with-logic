using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalMechanism : LogicalElement
{
    // Start is called before the first frame update
    protected override void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        //SpriteChange();
    }
}
