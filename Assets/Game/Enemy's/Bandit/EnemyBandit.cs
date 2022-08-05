using UnityEngine;
using System;
public class EnemyBandit : MonoBehaviour
{
    [Header("Здоровье")]
    [SerializeField] private int maxHealthBandit = 3;
    private int healthBandit = 0;

    [Header("Скорость")]
    [SerializeField] private float moveSpeedBanditMax = 3f;
    private float moveSpeedBandit;

    [Header("Атака")]
    [SerializeField] private float attackSpeedBandit = 1f;
    private float timeToAttack = 1f;
    [SerializeField] private int attackDamageBandit = 1;
    [SerializeField] private float attackRangeBandit = 1f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _playerLayers;
    private bool isSoundSword = false;

    [Header("Звуки")]
    private bool isSoundWalk = false;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _stepClip;
    [SerializeField] private AudioClip _attackClip;

    //Компоненты
    private Transform _findPlayer;
    private Animator _animator;
    private SpriteRenderer _sprite;
    void Start()
    {
        _findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        moveSpeedBandit = moveSpeedBanditMax;
        healthBandit = maxHealthBandit;
    }
    void Update()
    {        
        BanditAttack();
    }
    private void FixedUpdate()
    {
        BanditMove();
    }
    private void OnDrawGizmosSelected()
    {//Отображение зоны атаки
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, attackRangeBandit);
    }
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy enter");
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }*/
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
                SoundSwordStart();
                _animator.SetTrigger("Attack");
                enemy.GetComponent<PlayerHero>().HeroTakeDamage(attackDamageBandit);
            }
            else SoundSwordStop();
        }
    }
    void BanditMove()
    {//если враг на расстоянии 5 по х и у, то он движется к игроку
        if (Math.Abs(transform.position.x - _findPlayer.position.x) < 5
            && Math.Abs(transform.position.y - _findPlayer.position.y) < 5)
        {
            //SoundWalkStart();
            transform.position = Vector2.MoveTowards(transform.position,
                _findPlayer.position, moveSpeedBandit * Time.deltaTime);
            if (transform.position.x < _findPlayer.position.x)
            {//игрок справа
                MoveAttackPointRight();
                EnemyNearPlayerStop();
                _sprite.flipX = false;
            }
             else if (transform.position.x > _findPlayer.position.x)
             {//игрок слева
                MoveAttackPointLeft();
                EnemyNearPlayerStop();
                _sprite.flipX = true;
             }
        }
        else
        {
            _animator.SetBool("IsRun", false);
            //SoundWalkStop();
        }
    }
    void EnemyNearPlayerStop()
    {//остановка анимации бега рядом с игроком
        if (Math.Abs(transform.position.x - _findPlayer.position.x) < 1.5f 
            && Math.Abs(transform.position.y - _findPlayer.position.y) < 1.5f)
        {
            moveSpeedBandit = 0;
            _animator.SetBool("IsRun", false);
        }            
        else
        {
            moveSpeedBandit = moveSpeedBanditMax;
            _animator.SetBool("IsRun", true);
        }
    }
    void MoveAttackPointRight()
    {
        if (_attackPoint.position.x < transform.position.x)
        {//Поворот зоны атаки вправо
            _attackPoint.transform.position = new Vector2(gameObject.transform.position.x + 0.5f,
                gameObject.transform.position.y);
        }
    }
    void MoveAttackPointLeft()
    {
        if (_attackPoint.position.x > transform.position.x)
        {//Поворот зоны атаки влево
            _attackPoint.transform.position = new Vector2(gameObject.transform.position.x - 0.5f,
                gameObject.transform.position.y);
        }
    }
    void SoundWalkStart()
    {
        if (!isSoundWalk)
        {
            _audioSource.clip = _stepClip;
            _audioSource.loop = true;
            _audioSource.pitch = 1.1f;
            _audioSource.Play();
            isSoundWalk = true;
        }
    }
    void SoundWalkStop()
    {
        if (isSoundWalk)
        {
            _audioSource.Stop();
            _audioSource.clip = null;
            _audioSource.loop = false;
            isSoundWalk = false;
        }
    }
    void SoundSwordStart()
    {
        if (!isSoundSword)
        {
            _audioSource.clip = _attackClip;
            _audioSource.Play();
            isSoundSword = true;
            //возможно добавить питч
        }
    }
    void SoundSwordStop()
    {
        isSoundSword = false;
        _audioSource.clip = null;
        _audioSource.loop = false;
    }
}