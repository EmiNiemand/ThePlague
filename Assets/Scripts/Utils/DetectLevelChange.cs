using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectLevelChange : MonoBehaviour
{
    private Scene _level;
    
    [SerializeField] private string levelChange;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelChange);
        }
    }
}
