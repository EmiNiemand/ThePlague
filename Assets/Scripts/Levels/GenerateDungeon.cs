using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class GenerateDungeon : MonoBehaviour
{
    private NewSpawn _spawn;
    private NewCorridor _corridor;
    private NewRoom _room;
    
    [Range(2, 10)]
    [SerializeField] private int levelDepth = 6;
    [Range(1, 5)]
    [SerializeField] private int minSpawnIndex = 1;
    [Range(1, 5)]
    [SerializeField] private int maxSpawnIndex = 5;
    [Range(1, 5)]
    [SerializeField] private int minCorridorIndex = 1;
    [Range(1, 5)]
    [SerializeField] private int maxCorridorIndex = 5;
    [Range(1, 5)]
    [SerializeField] private int minRoomIndex = 1;
    [Range(1, 5)]
    [SerializeField] private int maxRoomIndex = 5;
    

    private void OnEnable()
    {
        _spawn = gameObject.AddComponent<NewSpawn>();
        _corridor = gameObject.AddComponent<NewCorridor>();
        // _room = new NewRoom();
        
        this.Generate();
    }

    private void Generate()
    {
        GameObject spawn = _spawn.GetNewSpawn(minSpawnIndex, maxSpawnIndex);
        GameObject corridor = _corridor.GetNewCorridor(minCorridorIndex, maxCorridorIndex);

        Instantiate(spawn, new Vector3(0, 0, -1), Quaternion.identity);
        Instantiate(corridor, new Vector3(0, 0, -1), Quaternion.identity);
    }

}
