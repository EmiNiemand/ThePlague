using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private GameObject enemySpawned;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawned = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawned == null)
        {
            enemySpawned = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
