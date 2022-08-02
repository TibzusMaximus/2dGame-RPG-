using UnityEngine;
public class PlayerHero : MonoBehaviour
{
    [SerializeField] public int maxHealthHero = 10;
    private int healthHero = 0;
    [SerializeField] private int moveSpeedHero = 3;
    [SerializeField] private float attackSpeedHero = 1f;
    private float timeToAttack = 1f;
    [SerializeField] private int attackDamageHero = 1;
    [SerializeField] private float attackRangeHero = 5f;
    [SerializeField] private Transform _AttackPoint;
    [SerializeField] private LayerMask _enemyLayers;
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
    void HeroAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_AttackPoint.position,
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_AttackPoint.position, attackRangeHero);
    }
    void HeroMove()
    {
        float XInput = Input.GetAxis("Horizontal");
        float YInput = Input.GetAxis("Vertical");
        transform.Translate(Vector2.right * Time.deltaTime * moveSpeedHero * XInput);
        transform.Translate(Vector2.up * Time.deltaTime * moveSpeedHero * YInput);
        if (XInput > 0)
        {
            _animator.SetBool("IsRun", true);
            _sprite.flipX = false;
        }
        else if (XInput < 0)
        {
            _animator.SetBool("IsRun", true);
            _sprite.flipX = true;
        }
        else _animator.SetBool("IsRun", false);
    }
}
/*private void OnTriggerStay2D(Collider2D collision)
{
    if (collision.CompareTag("Enemy"))
    {
        //HeroAttack();
    }
}

void DeathHero()
{
    if (lifeHero == 0)
    {
        Destroy(this.gameObject, 1);
        _animator.SetTrigger("Death");
    }
}
*/