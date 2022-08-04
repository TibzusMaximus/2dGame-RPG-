using UnityEngine;
public class PlayerHero : MonoBehaviour
{
    [Header("Здоровье")]
    [SerializeField] private int maxHealthHero = 5;
    private int healthHero = 0;
    [Header("Скорость")]
    [SerializeField] private int moveSpeedHero = 3;
    [Header("Атака")]
    [SerializeField] private float attackSpeedHero = 1f;
    private float timeToAttack = 1f;
    [SerializeField] private int attackDamageHero = 1;
    [SerializeField] private float attackRangeHero = 1.2f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;
    //[Header("Защита")]
    //[SerializeField] private float blockRetention = 0f;//время удержания блока
    static private float timeToBlock = 1.1f; // время ненажатия
    //Компоненты
    private Animator _animator;
    private SpriteRenderer _sprite;
    [Header("Звуки")]
    public AudioSource _step;
    public AudioSource _attack;

    void Start()
    {
        // Debug.Log("Time:" + timeToBlock);
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        healthHero = maxHealthHero;
    }
    void Update()
    {
        HeroMove();
        HeroAttack();
        HeroBlockAttack();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, attackRangeHero);
    }
    void HeroBlockAttack()
    {
        if (Input.GetKey(KeyCode.E))//
        {
            timeToBlock = 0;
            _animator.SetTrigger("BlockActive");
        }
        else
        {
            timeToBlock += Time.deltaTime;
           // _animator.SetTrigger("BlockActive", false);
        }
    }
    public void HeroTakeDamage(int damage)
    {
        if (!Input.GetKey(KeyCode.E) && (timeToBlock > 0.1))
        {
            healthHero -= damage;
            _animator.SetTrigger("Hurt");
            if (healthHero <= 0)
                HeroDeath();
        }
    }
    void HeroDeath()
    {
        _animator.SetBool("IsDeath", true);
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }
    void HeroAttack()
    {
        if (!Input.GetKey(KeyCode.E) && (timeToBlock > 0.1))
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position,
                            attackRangeHero, _enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                timeToAttack += Time.deltaTime;
                if (timeToAttack > attackSpeedHero)
                {
                    timeToAttack = 0;
                    _animator.SetTrigger("Attack");
                    enemy.GetComponent<EnemyBandit>().BanditTakeDamage(attackDamageHero);
                }
            }
        }
    }
    void HeroMove()
    {
        //Debug.Log("Time:" + timeToBlock);
        float XInput = Input.GetAxis("Horizontal");
            float YInput = Input.GetAxis("Vertical");
        if (!Input.GetKey(KeyCode.E) && (timeToBlock > 0.1))
        {
            transform.Translate(moveSpeedHero * Time.deltaTime * XInput * Vector2.right);
            transform.Translate(moveSpeedHero * Time.deltaTime * YInput * Vector2.up);
            if (XInput > 0)
            {
                
                if (_attackPoint.position.x < transform.position.x)
                {
                    _attackPoint.transform.position = new Vector2(gameObject.transform.position.x + 1,
                        gameObject.transform.position.y + 1);
                }
                _animator.SetBool("IsRun", true);
                _sprite.flipX = false;
            }
            else if (XInput < 0)
            {
                if (_attackPoint.position.x > transform.position.x)
                {
                    _attackPoint.transform.position = new Vector2(gameObject.transform.position.x - 1,
                        gameObject.transform.position.y + 1);
                }
                _animator.SetBool("IsRun", true);
                _sprite.flipX = true;
            }
            else _animator.SetBool("IsRun", false);
        }
    }        
}