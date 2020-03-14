using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalWire : LogicalElement
{
    /*
     * Элемент провода в логической схеме чисто декоративный, и не имеет влияния на геймплей или работу всей схемы.
     * Провода расставляются автоматически, для этого в инспекторе нужно выбрать начальний и конечный элемент.
     * Провода приклепляются к определенным точкам на элементах. 
     */
    
    //начальная точка провода
    private Vector2 startPoint;

    //конечная точка провода
    private Vector2 endPoint;
    
    private Vector2 resultVector;

    private float direction;

    private float distance;
    private Vector2 scale;
    private Transform inp;
    private Transform outp;

    //сеттеры
    public Transform SetInp
    {
        set => inp = value;
    }
    public Transform SetOutp
    {
        set => outp = value;
    }
    
    //геттеры
    public LogicalElement GetLe1 => le1;
    public LogicalElement GetLe2 => le2;

    //В данной функции определяется положение проводов в пространстве
    void wirePositionArrangement()
    {
        
        /*
         * В этой функции мы берем вектор (resultVector), который мы получаем, вычитая конечную точку из начальной,
         * получаем его угол поворота и длину и применяем к объекту, то есть к проводу.
         */
        scale = transform.localScale;
        startPoint = inp.position;
        endPoint = outp.position;
        resultVector = endPoint - startPoint;
        //нужно немного увеличить длину, чтобы выглядело красиво
        distance = resultVector.magnitude*1.2f;
        direction = Vector2.Angle(Vector2.right, resultVector);
        /*
         * Здесь прикол в том, что transform.rotation имеет тип Quaternion, из-за чего мы не можем просто так взять
         * и применить угол поворота вектора к проводу,
         * нужно перевести его в нужный тип, что и происходит в строке ниже.
         */
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
        /*
         * Применяем длину вектора к объекту,
         * проблема в том, что размер объекта указывается множителем оригинального спрайта,
         * так что нужно переводить длину вектора в этот множитель.
         * (с этим связан баг, в котором провод начинает бесконечно удлиняться при угле поворота равным 90/-90 градусам)
         */
        transform.localScale = new Vector2(distance*scale.x/GetComponent<Renderer>().bounds.size.x,1);
        // Также нужно расположить провод в нужном месте
        transform.position = startPoint+resultVector/2;
    }
    // Start is called before the first frame update
    void Awake()
    {
        //берет координаты точки выхода в элементе выхода
        inp = le1.gameObject.transform.Find("outPoint");
    }

    // Update is called once per frame
    protected override void Update()
    {
        wirePositionArrangement();
        state=le1.state;
        base.Update();
    }
}
