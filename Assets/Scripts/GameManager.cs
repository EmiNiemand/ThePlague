using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    private GameObject player;

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
            DontDestroyOnLoad(Camera.main);
        }
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (player == null)
        {
            player = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(player);
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
            Destroy(Camera.main);
            Destroy(GameObject.FindObjectOfType<GameUI>());
            SceneManager.LoadScene("Hub");
        }
    }
}
