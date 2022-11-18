using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private bool canRespawn;
    private bool canSpawn = true;
    private GameObject enemySpawned;
    
    // Start is called before the first frame update
    void Start()
    {
        enemySpawned = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawned == null && canSpawn && canRespawn)
        {
            StartCoroutine(SpawnCooldown());
        }
    }

    IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(1.0f);
        canSpawn = true;
        enemySpawned = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
