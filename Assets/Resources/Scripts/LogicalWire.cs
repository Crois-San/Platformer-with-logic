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
    private Vector2 scale;
    private Transform inp;
    private Transform outp;

    public Transform SetInp
    {
        set => inp = value;
    }
    public Transform SetOutp
    {
        set => outp = value;
    }
    public LogicalElement GetLe1 => le1;
    public LogicalElement GetLe2 => le2;

    void wirePositionArrangement()
    {
        
        scale = transform.localScale;
        startPoint = inp.position;
        endPoint = outp.position;
        resultVector = endPoint - startPoint;
        distance = resultVector.magnitude*1.2f;
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
        transform.localScale = new Vector2(distance*scale.x/GetComponent<Renderer>().bounds.size.x,1);
        transform.position = startPoint+resultVector/2;
    }
    // Start is called before the first frame update
    void Awake()
    {
        inp = le1.gameObject.transform.Find("outPoint");

    }

    // Update is called once per frame
    void Update()
    {
        wirePositionArrangement();
        state=le1.state;
        SpriteChange();

    }
}
