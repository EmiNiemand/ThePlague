using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private List<String> tags;
    [SerializeField] private GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        var objs = GameObject.FindObjectsOfType<GameManager>();
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoad;
            player = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(GameObject.FindObjectOfType<GameUI>());
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        }
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (player == null)
        {
            player = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
            DontDestroyOnLoad(GameObject.FindObjectOfType<GameUI>());
        }
        player.transform.position = Vector3.zero;
    }

    public void LoadScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerCombat>().GetHP() <= 0)
        {
            Destroy(player);
            Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
            Destroy(GameObject.Find("_GameUI"));
            SceneManager.LoadScene("Hub");
        }
        
        List<GameObject> layerOrderingList = new List<GameObject>();
        
        foreach (var tag in tags)
        {
            layerOrderingList.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }

        layerOrderingList = layerOrderingList.OrderBy(x => GetDistance(x)).ToList();
        int layer = 1;
        foreach (var obj in layerOrderingList)
        {
            SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
            if (!renderer)
            {
                renderer = obj.GetComponentInChildren<SpriteRenderer>();
            }

            renderer.sortingOrder = 1000 - layer;
            layer++;

        }
    }
    
    private float GetDistance(GameObject obj)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            renderer = obj.GetComponentInChildren<SpriteRenderer>();
        }
        
        return obj.transform.position.y - renderer.bounds.size.y/2;
    }
    
}
