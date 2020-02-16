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
    [SerializeField]
    private Transform inp;
    [SerializeField]
    private Transform outp;

    void wirePositionArrangement()
    {
        
        scale = transform.localScale;
        //startPoint = le1.transform.position;
        startPoint = inp.position;
        //endPoint = le2.transform.position;
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
        //inp = le1.gameObject.transform.Find("outPoint");
        //outp = le2.gameObject.transform.Find("inPoint 1");

    }

    // Update is called once per frame
    void Update()
    {
        wirePositionArrangement();
        state=le1.state;
        SpriteChange();
        
    }
}
