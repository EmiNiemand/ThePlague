using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class GenerateDungeon : MonoBehaviour
{
    private GameObject _spawn;
    private GameObject _corridor;
    private GameObject _room;
    private GameObject _bossRoom;
    private Vector3 _vect;

    [Range(2, 8)]
    [SerializeField] private int levelDepth = 4;
    
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
    [Range(0, 7)]
    [SerializeField] private int minRoomIndex = 0;
    [Range(0, 7)]
    [SerializeField] private int maxRoomIndex = 6;
    
    [SerializeField] private List<GameObject> bossRoomPrefabs;
    [Range(0, 3)]
    [SerializeField] private int minBossRoomIndex = 0;
    [Range(0, 3)]
    [SerializeField] private int maxBossRoomIndex = 3;
    
    private void OnEnable()
    {
        _spawn = spawnPrefabs[UnityEngine.Random.Range(minSpawnIndex, maxSpawnIndex)];
        _corridor = corridorPrefabs[UnityEngine.Random.Range(minCorridorIndex, maxCorridorIndex)];
        
        Vector3 corridorPos = _corridor.transform.position;
        Vector3 corridorExit = _corridor.transform.Find("Exit").position;
        Vector3 corridorEntry = _corridor.transform.Find("Entry").position;
        
        Instantiate(_spawn, _spawn.transform.position, Quaternion.identity);
        _vect = corridorEntry - _spawn.transform.Find("Exit").position;
        corridorPos -= _vect;
        Instantiate(_corridor, corridorPos, Quaternion.identity);
        corridorEntry -= _vect;
        corridorExit -= _vect;

        for (int i = 0; i < levelDepth; i++)
        {
            _room = roomPrefabs[UnityEngine.Random.Range(minRoomIndex, maxRoomIndex)];

            _vect = _room.transform.Find("Entry").position - corridorExit;
            _room.transform.position -= _vect;
            Instantiate(_room, _room.transform.position, Quaternion.identity);

            _vect = _room.transform.Find("Exit").position - corridorEntry;
            corridorPos += _vect;
            Instantiate(_corridor, corridorPos, Quaternion.identity);
            corridorEntry += _vect;
            corridorExit += _vect;
        }
        
        _bossRoom = bossRoomPrefabs[UnityEngine.Random.Range(minBossRoomIndex, maxBossRoomIndex)];
            
        _vect = _bossRoom.transform.Find("Entry").position - corridorExit;
        _bossRoom.transform.position -= _vect;
        Instantiate(_bossRoom, _bossRoom.transform.position, Quaternion.identity);
    }

}
