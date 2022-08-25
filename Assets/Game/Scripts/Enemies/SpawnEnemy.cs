using System.Collections;
using UnityEngine;
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    //Компоненты
    //private Transform _findPlayer;

    void Start()
    {
        //_findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(EnemySpawn());
    }
    IEnumerator EnemySpawn()
    {
        while (true)
        {
            Instantiate(EnemyPrefab, new Vector2(Random.Range(-10,10), 
                Random.Range(-10, 10)), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }    
}//