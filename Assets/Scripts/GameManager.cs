using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int money = 0;
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
    public void UpdateMoney(int earnAmount) {
        PlayerPrefs.SetInt("dollar",PlayerPrefs.GetInt("dollar") + earnAmount);
        moneyText.text = PlayerPrefs.GetInt("dollar").ToString("C0");
        //moneyText.text = money.ToString() + "$";
    }
}
