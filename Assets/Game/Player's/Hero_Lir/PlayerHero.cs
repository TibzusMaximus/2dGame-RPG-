using UnityEngine;
public class PlayerHero : MonoBehaviour
{
    //Здоровье и скорость
    [SerializeField] public int maxHealthHero = 10;
    private int healthHero = 0;
    [SerializeField] private int moveSpeedHero = 3;
    //Атака
    [SerializeField] private float attackSpeedHero = 1f;
    private float timeToAttack = 1f;
    [SerializeField] private int attackDamageHero = 1;
    [SerializeField] private float attackRangeHero = 1.2f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;
    //Компоненты
    private Animator _animator;
    private SpriteRenderer _sprite;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        healthHero = maxHealthHero;
    }
    void Update()
    {
        HeroMove();
        HeroAttack();
    }
    private void OnDrawGizmosSelected()
    {//Отображение зоны атаки
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, attackRangeHero);
    }
    public void HeroTakeDamage(int damage)
    {
        healthHero -= damage;
        _animator.SetTrigger("Hurt");
        if (healthHero <= 0)
            HeroDeath();
    }
    void HeroDeath()
    {
        _animator.SetBool("IsDeath", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    void HeroAttack()
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
                Debug.Log("Hero hit enemy");
            }
        }
    }
    void HeroMove()
    {
        float XInput = Input.GetAxis("Horizontal");
        float YInput = Input.GetAxis("Vertical");
        transform.Translate(Vector2.right * Time.deltaTime * moveSpeedHero * XInput);
        transform.Translate(Vector2.up * Time.deltaTime * moveSpeedHero * YInput);
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