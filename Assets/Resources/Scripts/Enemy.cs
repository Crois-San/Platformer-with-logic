using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    /*
     * У врага есть такие базовые функции, как движение и прыжок.
     * Также враг может находиться в трех состояниях:
     * спокойном, преследования игрока и поиска логических вводов.
     * Во время движения враг определяет, есть ли перед ним препятствие, есть ли перед ним земля для движения,
     * и есть ли под этой землей ландшафт.
     * В зависитмости от результатов, враг может запрыгнуть на препятствие, развернуться, или сойти с платформы.
     * Далее, в спокойном состоянии враг спокойно идет по платформам, иногда разворачиваясь.
     * В состоянии преследования, враг будет преследовать игрока какое-то время, даже тот покинет поле зрения монстра.
     * В состоянии поиска ввода, враг будет пытаться изменить состояние логических элементов схеме, мешая игроку.
     */
    //коллайдеры, нужный для проверки при движении
    private Collider2D[] collidersNextToEntity, collidersAboveEntity, collidersUnderEntity, collidersUnderPlatform;
    //коллайдер, для поиска ввода 
    private Collider2D input;
    //основной таймер
    private float timer;
    //таймер, по истечению которого враг перестает преследовать игрока
    private float attentionTimerPlayer;
    //по этому таймеру, каждые 10 секунд враг переходит в состояние поиска ввода
    private float lookingForInputTimer;
    //таймер, считающий время, за которое враг должен успеть изменить элемент
    private float attentionTimerInput;
    
    //скорость движения в спокойном состоянии
    [SerializeField]
    private float speedIdle = 0.05f;
    //скорость движения в состоянии преследования
    [SerializeField]
    private float speedChasing = 0.1f;
    private float groundHeight;
    RaycastHit2D raycastResult;
    private Vector3 rayOriginPoint;
    [SerializeField]
    //маски для поиска игрока и ввода соответственно
    private LayerMask targetPlayerMask,targetInputMask;

    //объекты игрока и ввода соответственно
    private GameObject targetPlayer, targetInput;

    //геттеры
    public GameObject getTargetPlayer => targetPlayer;
    public GameObject getTargetInput => targetInput;
    public Collider2D[] getCollidersNextToEntity => collidersNextToEntity;


    protected override void Moving(float move)
    {
        //поиск препятствий перед врогом
        collidersNextToEntity =
            Physics2D.OverlapCircleAll(transform.position + move * 2.0f * Vector3.right, 0.2f, mask);
        //поиск пропастей перед врагом
        collidersUnderEntity =
            Physics2D.OverlapCircleAll(
                transform.position + (characterSize.y / 2 + 0.5f) * Vector3.down + move * (characterSize.x/2 + 0.2f) * Vector3.right, 0.2f,
                mask);
        //поиск препятствий над врогом
        collidersAboveEntity =
            Physics2D.OverlapCircleAll(transform.position + move * 2.0f * Vector3.right + (characterSize.y / 2 + 0.5f) * Vector3.up, 0.2f, mask);
        /*
         * поиск  платформы под найденной платформой,
         * таким образом враг может спрыгнуть при небольшой высоте
         */ 
        if (collidersUnderEntity.Length !=0)
        {
            groundHeight = collidersUnderEntity[0].bounds.size.y;
            collidersUnderPlatform = Physics2D.OverlapCircleAll(
                transform.position + (characterSize.y / 2 + 0.5f+groundHeight) * Vector3.down + move * (characterSize.x/2 + 0.2f) * Vector3.right, 0.2f,
                mask);
        }
        
        //если есть препятсвие, враг запрыгнет на него, если впереди стена, то вместо прыжка развернется
        if (collidersNextToEntity.Length > 0 && collidersAboveEntity.Length == 0)
        {
            jumpRequest = true;
        } else if (collidersAboveEntity.Length > 0)
        {
            moving *= -1;
        }

        Jumping(); 
        // если есть пропасть впереди, враг развернется
        if (collidersUnderPlatform != null) 
        {
            if (collidersUnderEntity.Length == 0 && collidersUnderPlatform.Length == 0 &&!jumpRequest)
            {
                moving *= -1;
            } 
        }
        

        base.Moving(move);
    }

    protected override void Jumping()
    {
        if (grounded && jumpRequest)
        {
            body.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
            jumpRequest = false;
            grounded = false;
        }
        else
        {
            //проверка касанния с платформой, подробнее в комментариях в Character.cs
            Vector2 boxCenter = (Vector2) body.transform.position + 0.5f * (characterSize.y + boxSize.y) * Vector2.down;
            grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask) != null);
        }

        if (body.velocity.y < 0f)
        {
            body.gravityScale = fallMultiplier;
        }
    }


    //спокойное состояние
    void StateIdle()
    {
        
        speed = speedIdle;
        //каждые десять секунд враг разворвчивается в обратную сторону
        if (timer >= 10)
        {
            moving *= -1;
            timer = 0.0f;
        }

        Moving(moving);
    }

    //состояние преследования
    void StatePlayerChasing()
    {
        //изменяет направление движения, если игрок прошел мимо врага
        moving =Mathf.Sign((targetPlayer.transform.position - transform.position).x) ;
        attentionTimerPlayer += Time.deltaTime % 60;
        speed = speedChasing;
        Moving(moving);
        //по истечении таймера враг теряет внимание и возращается в спокойное состояние
        if (attentionTimerPlayer >= 3)
        {
            targetPlayer = null;
            attentionTimerPlayer = 0;
        }

    }

    //состояние поиска элементов ввода
    void StateInputChanging()
    {
        // каждые десять секунд враг переходит в состояние поиска ввода
        int foo = (int) (lookingForInputTimer % 10);
        if (foo == 0 && lookingForInputTimer > 1) //lookingForInputTimer > 1 нужно, чтобы враг не перешел в это состояние со старта игры
        {
            attentionTimerInput += Time.deltaTime % 60;
            //по истечении таймера враг вернется в спокойное состояние
            if (attentionTimerInput >= 2)
            {
                return;
            }
        }
        else
        {
            attentionTimerInput = 0;
            return;
        }
        //поиск элемента ввода, при провале враг возвращается в спокойное состояние
        input = Physics2D.OverlapCircle(transform.position, 5f, targetInputMask);
        if (!input) return;
        targetInput = input.gameObject;
        /*
         * Определение расстояния до элемента.
         * При приближении к элементу, начинает прыжок
         */
        if (Mathf.Abs(targetInput.transform.position.x - transform.position.x) <= 0.1f)
        {
            jumpRequest = true;
            targetInput = null;

        }
        else
        {
            //движение по направлению к элементу
            moving = Mathf.Sign((targetInput.transform.position - transform.position).x);
            Moving(moving);
        }
        Jumping();
    }



    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        moving = 1;
        speed = 0.05f;
        grounded = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (healthPoints <= 0)
        {
            NpcDeath();
        }
    }

    protected override void FixedUpdate()
    {
        timer += Time.deltaTime % 60;
        lookingForInputTimer += Time.deltaTime % 60;
        //начальная точка для поиска персонажа
        rayOriginPoint = transform.position + moving * characterSize.x * Vector3.right;
        /*
         * Поиск персонажа. Если персонаж попадает в поле зрения врага, он переходит в состояние преследования.
         * Луч для поиска разворачивается вместе с персонажем.
         */
        raycastResult = Physics2D.Raycast(rayOriginPoint, moving*Vector3.right,15f, targetPlayerMask);
        if (raycastResult)
        {
            //запоминает персонажа
            targetPlayer = raycastResult.collider.gameObject;
        }
        if (raycastResult || targetPlayer)
        {
            StatePlayerChasing();
        }
        else
        {
            StateInputChanging();
            StateIdle();
        }
            
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var collider = other.collider;
        DamageDealing(collider);
    }
}