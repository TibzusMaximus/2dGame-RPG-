using System.Collections;
using UnityEngine;
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    //Компоненты
    private Transform _findPlayer;
    void Start()
    {
        _findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(EnemySpawn());
    }
    IEnumerator EnemySpawn()
    {
        while (true)
        {
            Instantiate(EnemyPrefab, new Vector2(_findPlayer.position.x + Random.Range(5,10),
                _findPlayer.position.y + Random.Range(-5, 10)), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }    
}//