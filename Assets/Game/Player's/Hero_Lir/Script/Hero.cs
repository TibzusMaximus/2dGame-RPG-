using UnityEngine;
using System.Collections;
public class Hero : MonoBehaviour
{
    [SerializeField] private int moveSpeedHero = 3;
    [SerializeField] public int lifeHero = 10;
    [SerializeField] private float timeToAttackHero = 1f;
    [SerializeField] private float attackRangeHero = 5;
    [SerializeField] private float damageHero = 1;
    private float timeAttackCounter = 1;


    private Transform _Hero;
    private LayerMask _enemy;
    private Animator _animator;
    private SpriteRenderer _sprite;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        HeroMove();
        DeathHero();
    }
    private void OnTriggerStay2D(Collider2D collision)
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
    /*void HeroAttack()
    {
        
        timeAttackCounter += Time.deltaTime;
        if (timeAttackCounter > timeToAttackHero)
        {
            timeAttackCounter = 0;
            _animator.SetTrigger("Attack");            

        }
    }*/
    void HeroAttack()
    {
        timeAttackCounter += Time.deltaTime;
        if (timeAttackCounter > timeToAttackHero)
        {
            timeAttackCounter = 0f;
            _animator.SetTrigger("Attack");
            Collider2D[] enemies = Physics2D.OverlapCircleAll(_Hero.position, attackRangeHero, _enemy);
            for (int i = 0; i < enemies.Length; i++)
                enemies[i].GetComponent<EnemyBandit>().TakeDamage();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_Hero.position, attackRangeHero);
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