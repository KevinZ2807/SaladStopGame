using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool isPaused = false;
    void Start() {
        instance = this;
    }


    public void PauseGame() {
        Time.timeScale = 0; // Stops the time
        isPaused = true;
    }
    public void ResumeGame() {
        Time.timeScale = 1; // Restarts the time
        isPaused = false;
    }
}
