using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PanelManager : MonoBehaviour
{
    public List<string> sceneList = new List<string>();

    public void TurnOffPanel() {
        gameObject.SetActive(false);
    }
    public void TurnOnPanel() {
        gameObject.SetActive(true);
    }

    
    public void ChangeToScene(string sceneName) {
        if (sceneList.Contains(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name not found in the list!");
        }
    }
    public void TurnOffTheGame() {
        Application.Quit();
    }
}
