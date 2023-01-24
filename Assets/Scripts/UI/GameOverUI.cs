using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floorCounter;
    [SerializeField] private TextMeshProUGUI highScore;
    public UnityEvent onRestart;
    public UnityEvent onQuit;

    public void SetFloorCounter(int floorCount)
    {
        floorCounter.text = "Floor reached: "+floorCount;
    }

    public void HighScore(int highScore, bool newHighScore)
    {
        if(newHighScore)
            this.highScore.text = "NEW HIGH SCORE!";
        else
            this.highScore.text = "High Score: "+highScore;
    }

    // These are meant to invoke Game Manager events
    // ---------------------------------------------
    public void OnRestart() { onRestart.Invoke(); }
    public void OnQuit() { onRestart.Invoke(); }
}
