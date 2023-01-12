using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void OnPlay()
        {
            SceneManager.LoadScene("Hub");
        }
    
        public void OnQuit()
        {
            Application.Quit();
        }
    }
}

