using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PanelManager : MonoBehaviour
{
    public List<string> sceneList = new List<string>();

    // Menu
    public void TurnOffPanel() {
        AudioManager.instance.PlayTapSound();
        gameObject.SetActive(false);
    }
    public void TurnOnPanel() {
        AudioManager.instance.PlayTapSound();
        gameObject.SetActive(true);
    }

    // In-game:
    public void TurnOnPanelIngame() {
        AudioManager.instance.PlayTapSound();
        gameObject.SetActive(true);
        GameManager.instance.PauseGame();
    }
    
    public void TurnOffPanelIngame() {
        AudioManager.instance.PlayTapSound();
        gameObject.SetActive(false);
        GameManager.instance.ResumeGame();
    }

    public void ChangeToScene(string sceneName) {
        AudioManager.instance.PlayTapSound();
        StartCoroutine(MakeTranstition(sceneName));
    }
    private IEnumerator MakeTranstition(string sceneName) {
        if (sceneList.Contains(sceneName))
        {
            SceneLoader.instance.StartTranstition();
            if (Time.timeScale == 0) Time.timeScale = 1;
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name not found in the list!");
        }
    }
    public void TurnOffTheGame() {
        AudioManager.instance.PlayTapSound();
        Application.Quit();
    }
}
