using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalPiston : LogicalElement
{
    /*
     * Поршень - на данный момент самый сложный логический элемент.
     * Он проверяет, есть ли под ним объект для отталкивания.
     * Если он есть, он приклепляется и применяется в работе элемента.
     * Если сигнал положительный, он отталкивает объект от себя,
     * если сигнал отрицательный - притягивает к себе.
     */
    
    //объект, который поршень отталкивает
    private GameObject pulledObject;
    //нужна для поиска объекта для отталкивания
    private Vector2 rayStart;
    //переменная положительна, когда найден объект для отталкивания
    private bool isConnected;
    //маска, нужна для поиска объекта, чтобы он не нашел сам себя
    [SerializeField]
    private LayerMask m;
    //расстояние, на которое поршень может двигать объект
    [Range(1,5)] [SerializeField]
    private float pushDistance;

    //объект для анимации поршня
    private Animator pistonAnim, pistonLightAnim;
    //позиция рукояти поршня
    private Transform pistonHead;
    private Vector2 pistonHeadSize, pistonHeadScale;
    
    
    
    //позиция объекта
    private Vector2 pulledObjPos;
    //начальная и конечная точка объекта
    private float startPos, endPos, pistonHeadStartPos, pistonHeadEndPos;
    private Vector2 pistonHeadPos;

    //геттер
    public bool getConnected => isConnected;

    //функция движения поршня
    void PistonAction(LogicalElement A)
    {
        state = A.state;
        isConnected = Physics2D.Raycast(rayStart,Vector2.down,5f,m);

        //движение происходит только если объект связан с поршнем
        if (isConnected)
        {
            
            if (state)
            {
                //отталкивание объекта
                startPos = pulledObject.transform.position.y;
                endPos = pulledObjPos.y-pushDistance;
                pistonHeadStartPos = pistonHead.position.y;
                pistonHeadEndPos = pistonHeadPos.y - pushDistance;
                //Math.lerp нужен для плавного перемещения объекта из точки А в точку Б
                pulledObject.transform.position = new Vector2(pulledObjPos.x,Mathf.Lerp(startPos,endPos,Time.deltaTime*2.0f));
                pistonHead.position = new Vector2(pistonHeadPos.x,Mathf.Lerp(pistonHeadStartPos,pistonHeadEndPos,Time.deltaTime*2.0f));
                
            }
            else
            {
                //притягивание объекта
                startPos = pulledObject.transform.position.y;
                endPos = pulledObjPos.y;
                pistonHeadStartPos = pistonHead.position.y;
                pistonHeadEndPos = pistonHeadPos.y;
                pulledObject.transform.position = new Vector2(pulledObjPos.x,Mathf.Lerp(startPos,endPos,Time.deltaTime*2.0f));
                pistonHead.position = new Vector2(pistonHeadPos.x,Mathf.Lerp(pistonHeadStartPos,pistonHeadEndPos,Time.deltaTime*2.0f));
            }
        }
    }
    
    private void Start()
    {
        //определение точки, из которой начинается поиск объекта 
        rayStart = this.gameObject.transform.position;
        pistonAnim = GetComponent<Animator>();
        pistonLightAnim = transform.Find("LightSource").gameObject.GetComponent<Animator>();
        pistonHead = transform.Find("pistonHead");
        pistonHeadSize = pistonHead.gameObject.GetComponent<SpriteRenderer>().size;
        pistonHeadScale = pistonHead.localScale;
        pistonHeadPos = pistonHead.position;
        /*
         * Собственно поиск объекта.
         * При коллизии с объектом, связывает его с поршнем.
         * Также возврщает true в isConnected.
         */
        isConnected = Physics2D.Raycast(rayStart,Vector2.down,5f,m);
        if (isConnected)
        {
            //связь объекта с поршнем
            RaycastHit2D hit = Physics2D.Raycast(rayStart,Vector2.down,5f,m);
            pulledObjPos = hit.transform.position;
            pulledObject = hit.collider.gameObject;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        PistonAction(le1);
//        SpriteChange();
    }

    private void LateUpdate()
    {
        //анимация поршня
        pistonAnim.SetBool("State",state);
        //анимированный свет на поршне
        pistonLightAnim.SetBool("State",state);
    }
}
