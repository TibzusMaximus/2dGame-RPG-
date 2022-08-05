using UnityEngine;
public class PlayerHero : MonoBehaviour
{
    [Header("Здоровье")]
    [SerializeField] private int maxHealthHero = 5;
    private int healthHero = 0;
    [SerializeField] private HealthBar _healthBar;

    [Header("Скорость")]
    [SerializeField] private float moveSpeedHero = 4f;
    //private Vector2 moveInput;
    //private Vector2 moveVelocity;

    [Header("Атака")]
    [SerializeField] private float attackSpeedHero = 1f;
    private float timeToAttack = 1f;
    private bool isSoundSword = false;
    [SerializeField] private int attackDamageHero = 1;
    [SerializeField] private float attackRangeHero = 1.2f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;

    [Header("Звуки")]
    private bool isSoundWalk = false;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _stepClip;
    [SerializeField] private AudioClip _attackClip;

    //[Header("Защита")]
    //[SerializeField] private float blockRetention = 0f;//время удержания блока
    //static private float timeToBlock = 1.1f; // время ненажатия

    //Компоненты
    private Animator _animator;
    private SpriteRenderer _sprite;
    
    //private Rigidbody2D rb2d;


    void Start()
    {// Debug.Log("Time:" + timeToBlock);
        //Time.timeScale = 1;
        //rb2d = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        
        healthHero = maxHealthHero;
        _healthBar.SetMaxHealth(maxHealthHero);
    }
    void Update()
    {
        HeroMove();
        HeroAttack();
        HeroBlockAttack();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, attackRangeHero);
    }
    void HeroBlockAttack()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _animator.SetTrigger("BlockActive");
            SoundWalkStop();
        }
    }
    public void HeroTakeDamage(int damage)
    {
        if (!Input.GetKey(KeyCode.E))
        {//Если блок не активен
            healthHero -= damage;
            _healthBar.SetCurrentHealth(healthHero);
            _animator.SetTrigger("Hurt");
            if (healthHero <= 0)
                HeroDeath();
        }
    }
    void HeroAttack()
    {
        if (!Input.GetKey(KeyCode.E))
        {//Если блок не активен
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position,
                            attackRangeHero, _enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                timeToAttack += Time.deltaTime;
                if (timeToAttack > attackSpeedHero)
                {
                    timeToAttack = 0;
                    SoundSwordStart();
                    _animator.SetTrigger("Attack");
                    enemy.GetComponent<EnemyBandit>().BanditTakeDamage(attackDamageHero);
                }
                else SoundSwordStop();
            }
        }
    }
    void HeroMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if (!Input.GetKey(KeyCode.E))
        {//Если блок не активен
            //1
            //moveInput = new Vector2(inputX, inputY);
            //moveVelocity = moveInput.normalized * moveSpeedHero;
            //rb2d.MovePosition(rb2d.position + moveVelocity * Time.deltaTime);
            //2
            //rb2d.velocity = Vector2.up * moveSpeedHero * inputX;
            //3
            //rb2d.velocity = new Vector2(inputX * moveSpeedHero, inputY * moveSpeedHero);
            //4
            transform.Translate(moveSpeedHero * Time.deltaTime * inputX * Vector2.right);
            transform.Translate(moveSpeedHero * Time.deltaTime * inputY * Vector2.up);
            if (inputX > 0)
            {//движение вправо
                MoveAttackPointRight();
                SoundWalkStart();
                _animator.SetBool("IsRun", true);
                _sprite.flipX = false;
            }
            else if (inputX < 0)
            {//движение влево
                SoundWalkStart();
                MoveAttackPointLeft();
                _animator.SetBool("IsRun", true);
                _sprite.flipX = true;
            }
            else if (inputY != 0)
            {//движение вверх и вниз
                _animator.SetBool("IsRun", true);
                SoundWalkStart();
            }
            else
            {
                _animator.SetBool("IsRun", false);
                SoundWalkStop();
            }
        }
    }
    void HeroDeath()
    {
        _animator.SetBool("IsDeath", true);
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }
    void SoundSwordStart()
    {
        if (!isSoundSword)
        {
            _audioSource.clip = _attackClip;            
            _audioSource.Play();
            isSoundSword = true;
        }
    }
    void SoundSwordStop()
    {
        isSoundSword = false;
        _audioSource.loop = false;
    }
    void SoundWalkStart()
    {
        if (!isSoundWalk)
        {
            _audioSource.clip = _stepClip;
            _audioSource.loop = true;
            _audioSource.pitch = 0.85f;
            _audioSource.volume = 0.5f;
            _audioSource.Play();
            isSoundWalk = true;
        }
    }
    void SoundWalkStop()
    {
        if (isSoundWalk)
        {
            _audioSource.Stop();
            //_audioSource.clip = null;
            _audioSource.loop = false;
            isSoundWalk = false;
        }
    }
    void MoveAttackPointRight()
    {
        if (_attackPoint.position.x < transform.position.x)
        {
            _attackPoint.transform.position = new Vector2(transform.position.x + 1,
                transform.position.y);
        }
    }
    void MoveAttackPointLeft()
    {
        if (_attackPoint.position.x > transform.position.x)
        {
            _attackPoint.transform.position = new Vector2(transform.position.x - 1,
                transform.position.y);
        }
    }
}