using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackSphere : MonoBehaviour
{
    [SerializeField] private float timeToAttackHero = 1f;
    [SerializeField] private float attackRangeHero = 5;
    [SerializeField] private float damageHero = 1;

    private float timeAttackCounter = 1;
    private Transform _Hero;
    private LayerMask _enemy;
    private Animator _animator;

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
}
