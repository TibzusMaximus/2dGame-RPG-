using UnityEngine;
using System;
public class EnemyBandit : MonoBehaviour
{
    [SerializeField] private int lifeEnemy = 2;
    [SerializeField] private float speedEnemy = 3f;
    [SerializeField] private float timeToAttack = 1f;
    private float timeAttackCounter = 1f;
    private Transform findPlayer;
    private Animator _animator;
    private SpriteRenderer _sprite;
    void Start()
    {
         findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        EnemyMove();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnemyAttack();
        }
    }
    public void TakeDamage()
    {

    }
    private void EnemyAttack()
    {
        timeAttackCounter += Time.deltaTime;
        if (timeAttackCounter > timeToAttack)
        {
            timeAttackCounter = 0;
            _animator.SetTrigger("Attack");
            

        }
    }
    void EnemyMove()
    {//если враг на расстоянии 5 по х и у, то он движется к игроку
        if ( Math.Abs(transform.position.x - findPlayer.position.x) < 5 
            && Math.Abs(transform.position.y - findPlayer.position.y) < 5)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                findPlayer.position + new Vector3(1,1,0), speedEnemy * Time.deltaTime);
            if (transform.position.x < findPlayer.position.x) //игрок справа
             {
                 _animator.SetBool("Run", true);
                 _sprite.flipX = false;
             }
             else if (transform.position.x > findPlayer.position.x)//игрок слева
             {
                 _animator.SetBool("Run", true);
                 _sprite.flipX = true;
             }
        }
        else _animator.SetBool("Run", false);
    }
    
}
