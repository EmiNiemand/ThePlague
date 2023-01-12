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
    private GameObject _enemySpawn;
    private GameObject _enemyPrefab;
    private GameObject _doors;

    void Start()
    {
        _doors = GameObject.FindGameObjectWithTag("Doors");
        _doors.SetActive(false);
        _enemies = new List<GameObject>();
    }

    private void Update()
    {
        StartCoroutine(EnemyCounter());
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
        yield return new WaitForSeconds(0.5f);
        int enemyAmount = UnityEngine.Random.Range(minEnemiesCount, maxEnemiesCount);
        int index;

        for (int i = 0; i < enemyAmount; i++)
        {
            index = UnityEngine.Random.Range(0, enemiesPrefabs.Count);
            _enemyPrefab = enemiesPrefabs[index];
            
            index = UnityEngine.Random.Range(0, enemiesSpawns.Count);
            _enemySpawn = enemiesSpawns[index];
                    
            GameObject enemy = Instantiate(_enemyPrefab, _enemySpawn.transform.position, Quaternion.identity);
            
            _enemies.Add(enemy);
            enemiesSpawns.RemoveAt(index);
        }
    }

    IEnumerator EnemyCounter()
    {
        if (_enemies.Count != 0)
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (_enemies[i] == null)
                {
                    _enemies.RemoveAt(i);
                }
            }

            if (_enemies.Count == 0)
            {
                yield return new WaitForSeconds(1.0f);
                _doors.SetActive(false);
                Destroy(this);
            }
        }

        yield return null;
    }
    
}
