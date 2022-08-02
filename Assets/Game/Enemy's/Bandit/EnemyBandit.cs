using UnityEngine;
using System;
public class EnemyBandit : MonoBehaviour
{
    [SerializeField] private int maxHealthBandit = 3;
    private int healthBandit = 0;
    [SerializeField] private float moveSpeedBandit = 3f;
    [SerializeField] private float attackSpeedBandit = 1f;
    private float timeAttackCounter = 1f;
    private Transform findPlayer;
    private Animator _animator;
    private SpriteRenderer _sprite;
    void Start()
    {
         findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        healthBandit = maxHealthBandit;
    }
    void Update()
    {
        BanditMove();
        //BanditAttack();
    }
    public void BanditTakeDamage(int damage)
    {
        healthBandit -= damage;
        Debug.Log("Damage = " + damage);
        _animator.SetTrigger("Hurt");
        if (healthBandit <= 0)
            BanditDeath();
    }
    private void BanditDeath()
    {
        _animator.SetBool("IsDeath", true);
        Destroy(this.gameObject, 1);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    private void BanditAttack()
    {
        timeAttackCounter += Time.deltaTime;
        if (timeAttackCounter > attackSpeedBandit)
        {
            timeAttackCounter = 0;
            _animator.SetTrigger("Attack");
            

        }
    }
    void BanditMove()
    {//если враг на расстоянии 5 по х и у, то он движется к игроку
        if ( Math.Abs(transform.position.x - findPlayer.position.x) < 5 
            && Math.Abs(transform.position.y - findPlayer.position.y) < 5)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                findPlayer.position + new Vector3(1,1,0), moveSpeedBandit * Time.deltaTime);
            if (transform.position.x < findPlayer.position.x) //игрок справа
             {
                 _animator.SetBool("IsRun", true);
                 _sprite.flipX = false;
             }
             else if (transform.position.x > findPlayer.position.x)//игрок слева
             {
                 _animator.SetBool("IsRun", true);
                 _sprite.flipX = true;
             }
        }
        else _animator.SetBool("IsRun", false);
    }
}