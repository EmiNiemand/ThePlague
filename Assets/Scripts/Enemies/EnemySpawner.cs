using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;

    private GameObject EnemySpawned;

    // Start is called before the first frame update
    void Start()
    {
        EnemySpawned = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawned == null)
        {
            EnemySpawned = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
