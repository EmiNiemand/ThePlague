using UnityEngine;

public class NewSpawn : MonoBehaviour
{
    private GameObject entryPrefab;

    public GameObject GetNewSpawn(int minIndex, int maxIndex)
    {
        int prefabIndex = UnityEngine.Random.Range(minIndex, maxIndex);
        entryPrefab = Resources.Load("Levels/Spawns/Spawn" + prefabIndex.ToString()) as GameObject;
        
        return entryPrefab;
    }
}
