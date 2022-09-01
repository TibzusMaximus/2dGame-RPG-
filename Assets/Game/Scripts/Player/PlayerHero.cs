using UnityEngine;

public class PlayerHero : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private int maxHealthHero = 5;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private PauseMenu _deathMenu;
    private int healthHero = 0;

    [Header("Передвижение")]
    [SerializeField] private float moveSpeedHero = 4f;

    [Header("Атаки")]
    [SerializeField] private float attackSpeedHero = 1f;
    [SerializeField] private int attackDamageHero = 1;
    [SerializeField] private float attackRangeHero = 1.2f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayers;
    private float timeToAttack = 1f;

    [Header("Audio Source")]
    [SerializeField] private AudioSource _audioStepsOrBlock;
    [SerializeField] private AudioSource _audioSwordAttack;
    [SerializeField] private AudioSource _audioHurtOrDeath;

    //[Header("������")]
    //[SerializeField] private float blockRetention = 0f;//����� ��������� �����
    //static private float timeToBlock = 1.1f; // ����� ���������
    // Debug.Log("Time:" + timeToBlock);
    //����������
    private Animator _animator;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        

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
            _audioStepsOrBlock.GetComponent<AStepsOrBlock>().SoundWalkStop();
        }
    }
    public void HeroTakeDamage(int damage)
    {
        if (!Input.GetKey(KeyCode.E))
        {//���� ���� �� �������
            healthHero -= damage;
            _healthBar.SetCurrentHealth(healthHero);
            _animator.SetTrigger("Hurt");
            _audioHurtOrDeath.GetComponent<AHurtOrDeath>().SoundHurtStart(0);
            if (healthHero <= 0)
                HeroDeath();
        }
        else if (damage > 0)
        {//���� ���� �������
            _audioStepsOrBlock.GetComponent<AStepsOrBlock>().SoundBlockStart();
            //_animator.SetTrigger("BlockFlash");
        } 
    }
    void HeroAttack()
    {
        timeToAttack += Time.deltaTime;
        if (!Input.GetKey(KeyCode.E))
        {//���� ���� �� �������
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position,
                            attackRangeHero, _enemyLayers);
            
            
            //��������� �� ������������, �������� ������� ����
                if (timeToAttack > attackSpeedHero && Input.GetKey(KeyCode.Space))
                {
                    timeToAttack = 0;
                    _audioSwordAttack.GetComponent<ASwordAttack>().SoundSwordStart();
                    _animator.SetTrigger("Attack");
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.GetComponent<EnemyBandit>().BanditTakeDamage(attackDamageHero);
                    }
                }
                else _audioSwordAttack.GetComponent<ASwordAttack>().SoundSwordStop();
            
            

        }
    }
    void HeroMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if (!Input.GetKey(KeyCode.E))
        {//���� ���� �� �������
            _rb.velocity = new Vector2(inputX * moveSpeedHero, inputY * moveSpeedHero);
            if (inputX > 0)
            {//�������� ������
                MoveAttackPointRight();
                _audioStepsOrBlock.GetComponent<AStepsOrBlock>().SoundWalkStart();
                _animator.SetBool("IsRun", true);
                _sprite.flipX = false;
            }
            else if (inputX < 0)
            {//�������� �����
                _audioStepsOrBlock.GetComponent<AStepsOrBlock>().SoundWalkStart();
                MoveAttackPointLeft();
                _animator.SetBool("IsRun", true);
                _sprite.flipX = true;
            }
            else if (inputY != 0)
            {//�������� ����� � ����
                _animator.SetBool("IsRun", true);
                _audioStepsOrBlock.GetComponent<AStepsOrBlock>().SoundWalkStart();
            }
            else
            {
                _animator.SetBool("IsRun", false);
                _audioStepsOrBlock.GetComponent<AStepsOrBlock>().SoundWalkStop();
            }
        }
    }
    void HeroDeath()
    {
        _animator.SetBool("IsDeath", true);
        _deathMenu.GetComponent<PauseMenu>().DeathMenuActivate();
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
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
//�������� ��������
//1
//moveInput = new Vector2(inputX, inputY);
//moveVelocity = moveInput.normalized * moveSpeedHero;
//rb.MovePosition(rb2d.position + moveVelocity * Time.deltaTime);
//2
//transform.Translate(moveSpeedHero * Time.deltaTime * inputX * Vector2.right);
//transform.Translate(moveSpeedHero * Time.deltaTime * inputY * Vector2.up);