using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class GenerateLevel : MonoBehaviour
{
    private int minLevelDepth = 2;
    private GenerateGround _ground;
    private GameObject _go;
    private Rigidbody2D _player;
    
    [Range(2, 10)]
    [SerializeField] protected int maxLevelDepth;
    [Range(25, 60)]
    [SerializeField] protected int maxRoomHeight;
    [Range(25, 60)]
    [SerializeField] protected int maxRoomWidth;
    [SerializeField] protected Tilemap walls;
    [SerializeField] protected Tilemap ground;
    [SerializeField] protected Tilemap obstacles;
    [SerializeField] protected Tilemap traps;

    private void Start()
    {
        generateRoom();
    }

    void generateRoom()
    {
        _ground = gameObject.AddComponent<GenerateGround>();
        _ground.generateGround();
    }
}

public class GenerateGround : GenerateLevel {
    
    public void generateGround() 
    {
        for (int i = 1; i < maxRoomWidth + 1; i++)
        {
            var tilePos = ground.WorldToCell(new Vector3(0,0,0));
            ground.GetTile(new Vector3Int(0,0,0));
            ground.SetTile(tilePos, ScriptableObject.CreateInstance<Tile>());
        }
        
    }
}
