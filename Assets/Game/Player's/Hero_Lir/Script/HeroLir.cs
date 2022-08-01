using UnityEngine;
public class HeroLir : MonoBehaviour
{
    [SerializeField] int speedPlayer = 3;
    [SerializeField] private float timeToAttack = 1f;
    private float timeAttackCounter = 1f;
    private Animator _animator;
    private SpriteRenderer _sprite;
    //private CircleCollider2D _colliderAttack;
    
    //private int comboCounter = 0;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        //_colliderAttack = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        HeroMove();
        //HeroAttack();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            HeroAttack();
        }
    }
    private void HeroAttack()
    {
        
        timeAttackCounter += Time.deltaTime;
        if (timeAttackCounter > timeToAttack)
        {
            //comboCounter++;
            timeAttackCounter = 0;
            _animator.SetTrigger("Attack");            
        }
        /*if (comboCounter == 3)
        {
            comboCounter = 0;
            timeAttackCounter = 0;
            _animator.SetTrigger("Attack3");
        }*/
    }
    void HeroMove()
    {
        float XInput = Input.GetAxis("Horizontal");
        float YInput = Input.GetAxis("Vertical");
        transform.Translate(Vector2.right * Time.deltaTime * speedPlayer * XInput);
        transform.Translate(Vector2.up * Time.deltaTime * speedPlayer * YInput);
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