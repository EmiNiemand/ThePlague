using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class GenerateDungeon : MonoBehaviour
{
    private GameObject _spawn;
    private GameObject _corridor;
    private GameObject _room;
    private GameObject _bossRoom;
    private Vector3 _transVector;
    
    // CheckBox for random level depth
    [SerializeField]  private bool randomLevelDepth = true;

    // Maximum level depth with boss room
    // (eg. for levelDepth = 2 there is 1 room and 1 boss room)
    [Range(2, 8)]
    [SerializeField] private int levelDepth = 4;
    
    // Settings for randomizing spawn prefabs
    [SerializeField] private List<GameObject> spawnPrefabs;
    [Range(0, 4)]
    [SerializeField] private int minSpawnIndex = 0;
    [Range(0, 4)]
    [SerializeField] private int maxSpawnIndex = 4;
    
    // Settings for randomizing corridor prefabs
    [SerializeField] private List<GameObject> corridorPrefabs;
    [Range(0, 4)]
    [SerializeField] private int minCorridorIndex = 0;
    [Range(0, 4)]
    [SerializeField] private int maxCorridorIndex = 4;
    
    // Settings for randomizing room prefabs
    [SerializeField] private List<GameObject> roomPrefabs;
    [Range(0, 6)]
    [SerializeField] private int minRoomIndex = 0;
    [Range(0, 6)]
    [SerializeField] private int maxRoomIndex = 6;
    
    // Settings for randomizing boss room prefabs
    [SerializeField] private List<GameObject> bossRoomPrefabs;
    [Range(0, 2)]
    [SerializeField] private int minBossRoomIndex = 0;
    [Range(0, 2)]
    [SerializeField] private int maxBossRoomIndex = 2;

    private void OnEnable()
    {
        if (randomLevelDepth)
        {
            levelDepth = UnityEngine.Random.Range(2, 6);
        }
        
        // Randomize Spawn and Corridor prefab
        _spawn = spawnPrefabs[UnityEngine.Random.Range(minSpawnIndex, maxSpawnIndex)];
        _corridor = corridorPrefabs[UnityEngine.Random.Range(minCorridorIndex, maxCorridorIndex)];
        
        // Move player to the PlayerSpawn element position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = _spawn.transform.Find("PlayerSpawn").position;
        
        // Storing corridor elements positions to decrease 
        // total amount of calls to the object
        Vector3 corridorPos = _corridor.transform.position;
        Vector3 corridorExit = _corridor.transform.Find("Exit").position;
        Vector3 corridorEntry = _corridor.transform.Find("Entry").position;
        
        // Insantiating spawn at approximetely [0, 0]
        Instantiate(_spawn, _spawn.transform.position, Quaternion.identity);
        
        // Calculating translation vector between Exit point of spawn
        // and Entry point of Corridor
        _transVector = corridorEntry - _spawn.transform.Find("Exit").position;
        
        // Substracting trans. vector from Corridor position
        corridorPos -= _transVector;
        Instantiate(_corridor, corridorPos, Quaternion.identity);
        
        // Substracting trans. vector from Corridor Entry point and Exit point 
        corridorEntry -= _transVector;
        corridorExit -= _transVector;

        for (int i = 0; i < levelDepth - 1; i++)
        {
            // Randomize Room prefab
            _room = roomPrefabs[UnityEngine.Random.Range(minRoomIndex, maxRoomIndex)];

            // Calculating translation vector between Exit point of Corridor
            // and Entry point of Room
            _transVector = _room.transform.Find("Entry").position - corridorExit;
            
            // Substracting trans. vector from Room position
            _room.transform.position -= _transVector;
            Instantiate(_room, _room.transform.position, Quaternion.identity);

            // Calculating translation vector between Exit point of Room
            // and Entry point of (next) Corridor
            _transVector = _room.transform.Find("Exit").position - corridorEntry;
            
            // Adding trans. vector to corridor position 
            // (Exit point of the room is above "axis", so
            // we have to add the vector instead of substracting it)
            corridorPos += _transVector;
            Instantiate(_corridor, corridorPos, Quaternion.identity);
            
            // Adding trans. vector to Corridor Entry point and Exit point 
            corridorEntry += _transVector;
            corridorExit += _transVector;
        }
        
        // Randomize Boss Room prefab
        _bossRoom = bossRoomPrefabs[UnityEngine.Random.Range(minBossRoomIndex, maxBossRoomIndex)];
            
        // Calculating translation vector between Entry point of Boss Room
        // and Exit point of Corridor
        _transVector = _bossRoom.transform.Find("Entry").position - corridorExit;
        _bossRoom.transform.position -= _transVector;
        Instantiate(_bossRoom, _bossRoom.transform.position, Quaternion.identity);
    }

}
