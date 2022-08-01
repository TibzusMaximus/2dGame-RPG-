using System.Collections;
using UnityEngine;
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;

    private Transform findPlayer;
    void Start()
    {
        findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(EnemySpawn());
    }
    IEnumerator EnemySpawn()
    {
        while (true)
        {
            Instantiate(EnemyPrefab, new Vector2(findPlayer.position.x + Random.Range(5,10),
                findPlayer.position.y + Random.Range(-5, 10)), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }    
}