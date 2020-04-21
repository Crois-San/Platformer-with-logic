using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalPiston : LogicalMechanism
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

    public bool isNotPlaying { get; set; } = true;
    //маска, нужна для поиска объекта, чтобы он не нашел сам себя
    [SerializeField]
    private LayerMask m;
    //расстояние, на которое поршень может двигать объект
    [Range(0,5)] [SerializeField]
    private float pushDistance;

    //объект для анимации поршня
    private Animator pistonAnim, pistonLightAnim;
    //позиция рукояти поршня
    private Transform pistonHead;
    private Vector2 pistonHeadSize, pistonHeadScale;
    private ISoundSystem ssPiston;
    
    
    
    //позиция объекта
    private Vector2 pulledObjPos;
    //начальная и конечная точка объекта
    private Vector2 startPos, endPos, pistonHeadStartPos, pistonHeadEndPos;
    private Vector2 pistonHeadPos;
    public Vector2 pistonDirection { get; set; }
    public Vector3 pistonRotation { get; set; }


    //геттер
    public bool getConnected => isConnected;

    public void PistonMovementCalculaton(Vector2 start, Vector2 end, Vector2 headStart, Vector2 headEnd)
    {
        //Math.lerp нужен для плавного перемещения объекта из точки А в точку Б
        pulledObject.transform.position = Vector2.Lerp(start, end, Time.deltaTime * 2.0f);
        pistonHead.position = Vector2.Lerp(headStart,headEnd,Time.deltaTime*2.0f);
    }

    public void PistonSoundStop(Vector2 end)
    {
        if (Math.Abs(((Vector2)pulledObject.transform.position - end).magnitude) < 0.01f)
        {
            ssPiston.StopSound();
            isNotPlaying = true;
        }
    }

    //функция движения поршня
    void PistonAction(LogicalElement A)
    {
        state = A.state;
        isConnected = Physics2D.Raycast(rayStart, pistonDirection,5f*transform.localScale.x,m);

        //движение происходит только если объект связан с поршнем
        if (isConnected)
        {
            
            if (state)
            {
                if (isNotPlaying)
                {
                    ssPiston.MakeSound();
                    isNotPlaying = false;
                }
                //отталкивание объекта
                startPos = pulledObject.transform.position;
                endPos = pulledObjPos + pistonDirection * pushDistance;
                pistonHeadStartPos = pistonHead.position;
                pistonHeadEndPos = pistonHeadPos + pistonDirection * pushDistance;
                
                PistonMovementCalculaton(startPos,endPos,pistonHeadStartPos,pistonHeadEndPos);
                PistonSoundStop(endPos);

            }
            else
            {
                if (isNotPlaying)
                {
                    ssPiston.MakeSound();
                    isNotPlaying = false;
                }
                //притягивание объекта
                startPos = pulledObject.transform.position;
                endPos = pulledObjPos;
                pistonHeadStartPos = pistonHead.position;
                pistonHeadEndPos = pistonHeadPos;

                PistonMovementCalculaton(startPos,endPos,pistonHeadStartPos,pistonHeadEndPos);
                PistonSoundStop(endPos);

            }
        }
    }
    
    protected void Start()
    {
        base.Start();
        pushDistance *= transform.localScale.x;
        //определение точки, из которой начинается поиск объекта 
        rayStart = gameObject.transform.position;
        pistonRotation = gameObject.transform.rotation.eulerAngles;
        pistonDirection = Quaternion.Euler(pistonRotation) * Vector2.down;
        //pushDistance = GetComponent<Collider2D>().bounds.size.y;
        pistonAnim = GetComponent<Animator>();
        pistonLightAnim = transform.Find("LightSource").gameObject.GetComponent<Animator>();
        pistonHead = transform.Find("pistonHead");
        pistonHeadSize = pistonHead.gameObject.GetComponent<SpriteRenderer>().size;
        pistonHeadScale = pistonHead.localScale;
        pistonHeadPos = pistonHead.position;
        ssPiston = new SoundSystemDefaultLooping(gameObject,Sounds.PistonMovement, 0.6f);
        /*
         * Собственно поиск объекта.
         * При коллизии с объектом, связывает его с поршнем.
         * Также возврщает true в isConnected.
         */
        isConnected = Physics2D.Raycast(rayStart,pistonDirection,5f*transform.localScale.x,m);
        if (isConnected)
        {
            //связь объекта с поршнем
            RaycastHit2D hit = Physics2D.Raycast(rayStart,pistonDirection,5f,m);
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
