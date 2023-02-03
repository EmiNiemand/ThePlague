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
    private GameOverUI gameOverScreen;
    private GameUI playerUI;
    [SerializeField] private List<String> tags;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject gameOverScreenPrefab;
    private int sceneCounter = 0;
    private int highScore = 0;
    public bool canSpawn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // Check for duplicate GameManagers
        // --------------------------------
        if(GameObject.FindObjectsOfType<GameManager>().Length > 1)
            Destroy(gameObject);
        
        // Set up first GameManager
        // ------------------------
        else
        {
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoad;
            ResetGame();
        }
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        sceneCounter++;

        // Game over continuation
        // ----------------------
        if (!player)
        {
            sceneCounter = 0;
            ResetGame();
        }

        // Do not spawn caretaker on the first floor
        // -----------------------------------------
        canSpawn = (sceneCounter > 1);
        playerUI.UpdateFloorCounter(sceneCounter);

        player.transform.position = Vector3.zero;
    }

    public void LoadScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        Destroy(player);
        Destroy(GameObject.FindObjectOfType<GameUI>().gameObject);
        
        gameOverScreen = GameObject.
            Instantiate(gameOverScreenPrefab, transform.position, Quaternion.identity).
            GetComponent<GameOverUI>();
        gameOverScreen.onRestart.AddListener(() => {
            Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
            LoadScene("Hub");
        });
        gameOverScreen.onQuit.AddListener(Quit);

        gameOverScreen.SetFloorCounter(sceneCounter);
        gameOverScreen.HighScore(highScore, sceneCounter > highScore);
        if(sceneCounter > highScore) highScore = sceneCounter;
    }

    // Update is called once per frame
    void Update()
    {
        OrderLayers();
    }

    private void ResetGame()
    {
        player = GameObject.Instantiate(playerPrefab, GameObject.Find("PlayerSpawn").transform.position, Quaternion.identity);
        playerUI = GameObject.FindObjectOfType<GameUI>();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(playerUI);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCamera"));
        player.GetComponent<PlayerEvents>().onDie.AddListener(GameOver);
    }

    private void OrderLayers()
    {
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
                renderer = obj.GetComponentInChildren<SpriteRenderer>();

            renderer.sortingOrder = 1000 - layer;
            layer++;
        }
    }
    
    private float GetDistance(GameObject obj)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (!renderer)
            renderer = obj.GetComponentInChildren<SpriteRenderer>();
        
        return obj.transform.position.y - renderer.bounds.size.y/2;
    }
    
}
