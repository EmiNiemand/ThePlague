using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesSpawns;
    [SerializeField] private List<GameObject> enemiesPrefabs;
    [SerializeField] private int minEnemiesCount;
    [SerializeField] private int maxEnemiesCount;
    private List<GameObject> _enemies;
    private GameObject _doors;

    void Start()
    {
        _doors = GameObject.FindGameObjectWithTag("Doors");
        if(_doors) _doors.SetActive(false);
        _enemies = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_doors != null)
            {
                _doors.SetActive(true);
                StartCoroutine(SpawnEnemies());
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(0.75f);
        int enemyAmount = UnityEngine.Random.Range(minEnemiesCount, maxEnemiesCount);
        int index;

        for (int i = 0; i < enemyAmount; i++)
        {
            index = UnityEngine.Random.Range(0, enemiesPrefabs.Count);
            var _enemyPrefab = enemiesPrefabs[index];
            
            index = UnityEngine.Random.Range(0, enemiesSpawns.Count);
            var _enemySpawn = enemiesSpawns[index];
                    
            GameObject enemy = Instantiate(_enemyPrefab, _enemySpawn.transform.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().onDie.AddListener(()=>OnEnemyDeath(enemy));
            
            _enemies.Add(enemy);
            enemiesSpawns.RemoveAt(index);
        }
    }

    void OnEnemyDeath(GameObject enemy)
    {
        _enemies.Remove(enemy);
        if(_enemies.Count <= 0)
            StartCoroutine(DestroySequence());
    }

    IEnumerator DestroySequence()
    {
        yield return new WaitForSeconds(0.5f);
        _doors.SetActive(false);
        Destroy(this);
    }    
}
