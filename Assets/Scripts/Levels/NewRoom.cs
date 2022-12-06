using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewRoom : MonoBehaviour
{
    private List<GameObject> prefabList = new List<GameObject>();
    private GameObject roomPrefab;
    
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo("Prefab/Room");
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info) 
            prefabList.Add(Resources.Load("Prefabs/" + f.ToString()) as GameObject);
        
        int prefabIndex = UnityEngine.Random.Range(0, prefabList.Count - 1);
        roomPrefab = Instantiate(prefabList[prefabIndex]);
    }
}
