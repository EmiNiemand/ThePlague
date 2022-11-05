using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private GameObject enemySpawned;
    private bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawned = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawned == null && canSpawn)
        {
            StartCoroutine(SpawnCooldown());
        }
    }

    IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(1.0f);

        enemySpawned = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        canSpawn = true;
    }
}
