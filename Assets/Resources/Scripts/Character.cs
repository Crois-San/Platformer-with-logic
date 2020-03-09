using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    //скорость/высота прыжка персонажа
    [SerializeField] [Range(0f, 10f)] protected float jumpspeed;

    //маска, нужна, чтобы проверить, стоит ли персонаж на земле 
    [SerializeField] protected LayerMask mask;

    //направление персонажа
    protected float moving;

    //скорость движения персонажа
    [SerializeField] [Range(0f, 1f)]
    protected float speed = 0.2f;

    //модификатор гравитации
    [SerializeField][Range(1,3)]
    protected float fallMultiplier = 1.1f;
    
    
    protected float groundedSkin = 0.5f;
    
    float fJumpPressedRemember;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;

    float fGroundedRemember;
    [SerializeField]
    float fGroundedRememberTime = 0.25f;
    
    [SerializeField][Range(0, 1)]
    float fCutJumpHeight = 0.5f;
    
    //Количество очков здоровья персонажа
    [SerializeField] 
    protected int healthPoints = 3;
    
    [SerializeField] 
    protected int damagePoints = 1;

    [SerializeField] 
    protected float knockbackStrength = 1f;

    //Положительное значение этой переменной активирует прыжок
    protected bool jumpRequest;

    protected bool grounded;

    protected Vector2 boxSize;

    protected Vector2 characterSize;

    protected Rigidbody2D body;

    protected SpriteRenderer sr;

    //геттеры/сеттеры
    public bool getJumpRequest => jumpRequest;
    public bool getGrounded => grounded;
    public int getHealthPoints => healthPoints;

    public int setHealthPoints
    {
        set => healthPoints = value;
    }
    public int getDamagePoints => damagePoints;
    public int setDamagePoints
    {
        set => damagePoints = value;
    }

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
            if ((fJumpPressedRemember > 0) && (fGroundedRemember > 0))
            {
                fJumpPressedRemember = 0;
                fGroundedRemember = 0;
                //body.velocity = new Vector2(body.velocity.x, jumpspeed);
                body.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
                jumpRequest = false;
                grounded = false;
            }
            
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
            grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask));
        }
        fGroundedRemember -= Time.deltaTime;
        if (grounded)
        {
            fGroundedRemember = fGroundedRememberTime;
        }

        fJumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (body.velocity.y > 0)
            {
                body.velocity = new Vector2(body.velocity.x, body.velocity.y * fCutJumpHeight);
            }
        }

        /*
         * Увеличение гравитации при падении игрока,
         * нужно для более естественного прыжка
         */
        if (body.velocity.y < 0f)
        {
            body.gravityScale = fallMultiplier;
        }
        else
        {
            body.gravityScale = 1f;
        }
    }

    //действия, происходящие при значении очков здоровья <= 0
    //TODO: анимация смерти, возможно шейдерами; частицы. 
    protected void NpcDeath()
    {
        Destroy(gameObject);
    }
    
    //действия, происходящие при нанесении урона
    protected virtual void DamageDealing(Collider2D collider)
    {
        if(!collider) return;
        var victimGameObject = collider.gameObject;
        var victimCharacter = victimGameObject.GetComponent<Character>();
        if(!victimCharacter) return;
        victimCharacter.setHealthPoints = victimCharacter.getHealthPoints - damagePoints;
        if(!victimGameObject.GetComponent<Rigidbody2D>()) return;
        var knockbackVector = new Vector2(victimCharacter.moving*-1,1);
        victimGameObject.GetComponent<Rigidbody2D>().AddForce(knockbackVector*knockbackStrength,ForceMode2D.Impulse);
        Debug.Log(victimCharacter.getHealthPoints);
    }

    //вызывается в начале программы
    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        characterSize = GetComponent<BoxCollider2D>().size;
        sr = GetComponent<SpriteRenderer>();
        body.gravityScale = 1f;
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
        if (healthPoints <= 0)
        {
            NpcDeath();
        }
    }

    //Эта функция как Update(), но нужна для вычисления физики
    protected virtual void FixedUpdate()
    {
        Jumping();
    }
}