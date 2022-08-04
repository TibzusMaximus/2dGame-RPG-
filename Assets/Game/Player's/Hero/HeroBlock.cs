using UnityEngine;
public class HeroBlock : MonoBehaviour
{//При нажатии E отключается скрипт Hero
    [Header("Защита")]
    [SerializeField] private float blockSpeedHero = 1f;
    private float timeToBlock = 1f;
    private Animator _animator;
    void Start()
    {
        //Hero = gameObject.GetComponent<PlayerHero>();
        //Hero.enabled = false;
    }
    void Update()
    {
        HeroBlockAttack();
    }
    void HeroBlockAttack()
    {
        timeToBlock += Time.deltaTime;
        if ((timeToBlock > blockSpeedHero) && Input.GetKey(KeyCode.E))
        {
            timeToBlock = 0;
            _animator.SetTrigger("BlockActive");
        }
    }
}
