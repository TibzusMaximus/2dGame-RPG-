using UnityEngine;
using System;
public class EnemyBandit : MonoBehaviour
{
    //Здоровье и скорость
    [SerializeField] private int maxHealthBandit = 3;
    private int healthBandit = 0;
    [SerializeField] private float moveSpeedBandit = 3f;
    //Атака
    [SerializeField] private float attackSpeedBandit = 1f;
    private float timeToAttack = 1f;
    [SerializeField] private int attackDamageBandit = 1;
    [SerializeField] private float attackRangeBandit = 1f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _playerLayers;
    //Компоненты
    private Transform _findPlayer;
    private Animator _animator;
    private SpriteRenderer _sprite;
    void Start()
    {
        _findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        healthBandit = maxHealthBandit;
    }
    void Update()
    {
        BanditMove();
        BanditAttack();
    }
    private void OnDrawGizmosSelected()
    {//Отображение зоны атаки
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, attackRangeBandit);
    }
    public void BanditTakeDamage(int damage)
    {
        healthBandit -= damage;
        _animator.SetTrigger("Hurt");
        if (healthBandit <= 0)
            BanditDeath();
    }
    void BanditDeath()
    {
        _animator.SetBool("IsDeath", true);
        Destroy(this.gameObject, 1);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    void BanditAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position,
                attackRangeBandit, _playerLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            timeToAttack += Time.deltaTime;
            if (timeToAttack > attackSpeedBandit)
            {
                timeToAttack = 0;
                _animator.SetTrigger("Attack");
                enemy.GetComponent<PlayerHero>().HeroTakeDamage(attackDamageBandit);
            }
        }
    }
    void BanditMove()
    {//если враг на расстоянии 5 по х и у, то он движется к игроку
        if ( Math.Abs(transform.position.x - _findPlayer.position.x) < 5 
            && Math.Abs(transform.position.y - _findPlayer.position.y) < 5)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _findPlayer.position + new Vector3(1,1,0), moveSpeedBandit * Time.deltaTime);
            if (transform.position.x < _findPlayer.position.x) //игрок справа
             {
                if (_attackPoint.position.x < transform.position.x)
                {
                    _attackPoint.transform.position = new Vector2(gameObject.transform.position.x + 0.5f,
                        gameObject.transform.position.y);
                }
                _sprite.flipX = false;
                if (Math.Abs(transform.position.x - _findPlayer.position.x) < 1.5f)
                    _animator.SetBool("IsRun", false);
                else _animator.SetBool("IsRun", true);
             }
             else if (transform.position.x > _findPlayer.position.x)//игрок слева
             {
                if (_attackPoint.position.x > transform.position.x)
                {
                    _attackPoint.transform.position = new Vector2(gameObject.transform.position.x - 0.5f,
                        gameObject.transform.position.y);
                }
                _sprite.flipX = true;
                if (Math.Abs(transform.position.x - _findPlayer.position.x) < 1.5f)
                    _animator.SetBool("IsRun", false);
                else _animator.SetBool("IsRun", true);
             }
        }
        else _animator.SetBool("IsRun", false);
    }
}