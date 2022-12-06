using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewCorridor : MonoBehaviour
{
    private GameObject corridorPrefab;

    public GameObject GetNewCorridor(int minIndex, int maxIndex)
    {
        int prefabIndex = UnityEngine.Random.Range(minIndex, maxIndex);
        corridorPrefab = Resources.Load("Levels/Corridors/Corridor" + prefabIndex.ToString()) as GameObject;
        
        return corridorPrefab;
    }
}
