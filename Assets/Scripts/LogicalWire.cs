using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalWire : LogicalElement
{
    private Vector2 startPoint;

    private Vector2 endPoint;

    private Vector2 resultVector;

    private float direction;

    private float distance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startPoint = le1.transform.position;
        endPoint = le2.transform.position;
        resultVector = endPoint - startPoint;
        distance = resultVector.magnitude;
        direction = Vector2.Angle(Vector2.right, resultVector);
        Vector3 r = transform.rotation.eulerAngles;
        if (endPoint.y >= startPoint.y)
        {
            r.z = direction;
        }
        else
        {
            r.z = -direction;
        }
        transform.rotation = Quaternion.Euler(r);
        Vector2 v = new Vector2(1+distance/10,1);
        transform.localScale = v;
        transform.position = startPoint+resultVector/2;
        
    }

    // Update is called once per frame
    void Update()
    {
        state=le1.state;
        SpriteChange();
        
    }
}
