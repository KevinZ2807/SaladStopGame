using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject NPC_Spawner;
    public GameObject leavingPosition;
    [SerializeField] private int money = 0;
    private bool isPaused = false;
    void Start() {
        instance = this;
        money = PlayerPrefs.GetInt("dollar");
        moneyText.text = PlayerPrefs.GetInt("dollar").ToString("C0");
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
    public void SetMoneyUI() {
        moneyText.text = PlayerPrefs.GetInt("dollar").ToString("C0");
    }

    public void EnableSpawn(string spawnName) {
        NPC_Spawner.transform.Find(spawnName).gameObject.SetActive(true);
    }
}
