using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    //скорость/высота прыжка персонажа
    [Range(0f, 10f)] public float jumpspeed;

    //маска, нужна, чтобы проверить, стоит ли персонаж на земле 
    [SerializeField] protected LayerMask mask;

    //направление персонажа
    protected float moving;

    //скорость движения персонажа
    protected float speed = 0.2f;

    //модификатор гравитации
    protected float fallMultiplier = 2.5f;

    //альтернативный модификатор графитации
    protected float lowFallMultiplier = 2f;
    
    protected float groundedSkin = 0.5f;

    //Положительное значение этой переменной активирует прыжок
    protected bool jumpRequest;

    protected bool grounded;

    protected Vector2 boxSize;

    protected Vector2 characterSize;

    protected Rigidbody2D body;

    protected SpriteRenderer sr;

    //геттеры
    public bool getJumpRequest => jumpRequest;
    public bool getGrounded => grounded;

    /*
     * функция движения
     */
    protected virtual void Moving(float move)
    {
        //Строка ниже перемещает персонажа в направлении move и со скоростью speed
        body.transform.Translate(move * speed, 0f, 0f, Space.Self);
        //инвертирует спрайт в зависимости от направления движения
        sr.flipX = move < 0.0f;
    }

    //функция прыжка
    protected virtual void Jumping()
    {
        //прыжок возможен, только если jumpRequest активен
        if (jumpRequest)
        {
            /*
             * Строка ниже запускает персонажа вверх при прыжке,
             * со скоростью jumpspeed.
             */
            body.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
            jumpRequest = false;
            grounded = false;
        }
        else
        {
            /*
             * строка ниже участвует в проверке касания земли,
             * определяет положение, из которого начинается проверка
             */
            Vector2 boxCenter = (Vector2) body.transform.position + 0.5f * (characterSize.y + boxSize.y) * Vector2.down;
            /*
             * Собственно проверка касания земли,
             * OverlapBox проверяет, есть ли коллайдеры в определенной области,
             * если есть, возвращает true.
             */
            grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask) != null);
        }

        /*
         * Увеличение гравитации при падении игрока,
         * нужно для более естественного прыжка
         */
        if (body.velocity.y < 0f)
        {
            body.gravityScale = fallMultiplier;
        }
        /*
         * Если игрок не зажимает клавишу прыжка, а лишь слегка нажал ее,
         * то персонаж будет прыгнет ниже
         */
        else if (body.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            body.gravityScale = lowFallMultiplier;
        }
        else
        {
            body.gravityScale = 1f;
        }
    }

    //вызывается в начале программы
    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        characterSize = GetComponent<BoxCollider2D>().size;
        sr = GetComponent<SpriteRenderer>();
        /*
         * Размер области для определения соприкосновения с землей,
         * groundedSkin - высота области.
         */
        boxSize = new Vector2(characterSize.x, groundedSkin);
    }

    //вызывается каждый фрейм
    protected virtual void Update()
    {
        /*
         * Возвращает 1 или -1 в зависимости от направления движения.
         * Нажатие A или D на клавиатуре определяет направление движения
         */
        moving = Input.GetAxis("Moving");
        Moving(moving);
        /*
         * Если нажата кнопка W, персонаж прыгнет,
         * он может прыгать только, если касается земли
         */
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumpRequest = true;
        }
    }

    //Эта функция как Update(), но нужна для вычисления физики
    protected virtual void FixedUpdate()
    {
        Jumping();
    }
}