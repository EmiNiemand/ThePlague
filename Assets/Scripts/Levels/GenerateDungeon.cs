using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class GenerateDungeon : MonoBehaviour
{
    [Range(2, 10)]
    [SerializeField] private int levelDepth = 6;
    
    [SerializeField] private List<GameObject> spawnPrefabs;
    [Range(0, 4)]
    [SerializeField] private int minSpawnIndex = 0;
    [Range(0, 4)]
    [SerializeField] private int maxSpawnIndex = 4;
    
    [SerializeField] private List<GameObject> corridorPrefabs;
    [Range(0, 4)]
    [SerializeField] private int minCorridorIndex = 0;
    [Range(0, 4)]
    [SerializeField] private int maxCorridorIndex = 4;
    
    [SerializeField] private List<GameObject> roomPrefabs;
    [Range(0, 4)]
    [SerializeField] private int minRoomIndex = 0;
    [Range(0, 4)]
    [SerializeField] private int maxRoomIndex = 4;
    
    private void OnEnable()
    {
        this.Generate();
    }

    private void Generate()
    {
        GameObject spawn = GetRandomPrefab(minSpawnIndex, maxSpawnIndex, spawnPrefabs);
        GameObject corridor = GetRandomPrefab(minCorridorIndex, maxCorridorIndex, corridorPrefabs);
        GameObject room = GetRandomPrefab(minRoomIndex, maxRoomIndex, roomPrefabs);

        Instantiate(spawn, spawn.transform.position, Quaternion.identity);
        
        
        corridor.transform.Find("Entry").position = spawn.transform.Find("Exit").position;
        corridor.transform.position = corridor.transform.Find("Entry").localPosition;
        Instantiate(corridor, corridor.transform.position, Quaternion.identity);
           /* 
        vect = room.transform.Find("Entry").position - corridor.transform.Find("Exit").position;
        room.transform.position += vect;
        room.transform.Find("Exit").position += vect;
        room.transform.Find("Entry").position = corridor.transform.Find("Exit").position;
        Instantiate(room, room.transform.position, Quaternion.identity);
        */
    }

    private GameObject GetRandomPrefab(int min, int max, List<GameObject> prefabs)
    {
        return prefabs[UnityEngine.Random.Range(min, max)];
    }
    
}
