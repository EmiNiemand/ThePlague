using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaretakerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;
    private bool canSpawn = false;
    private GameObject NPCSpawned;

    private void Update()
    {
        canSpawn = FindObjectOfType<GameManager>().canSpawn;
        if(NPCSpawned == null && canSpawn) NPCSpawned = Instantiate(NPCPrefab, transform.position, Quaternion.identity);
    }

    public void SetCanSpawn(bool spawn)
    {
        canSpawn = spawn;
    }
}
