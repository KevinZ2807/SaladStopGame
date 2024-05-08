using DG.Tweening;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unlock : MonoBehaviour
{
    [SerializeField] private GameObject theRestaurant;
    [SerializeField] private GameObject exampleObject;
    [SerializeField] private GameObject unlockProgressObj;
    [SerializeField] private GameObject newDesk;
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI dollarAmount;
    [SerializeField] private int deskPrice;
    [SerializeField] private float rotation = 0f;
    private int deskRemainPrice;
    private float ProgressValue;
    public NavMeshSurface buildNavMesh;

    void Start()
    {
        dollarAmount.text = deskPrice.ToString("C0"); // C0: Currency choice
        deskRemainPrice = deskPrice;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt("dollar") > 0)
        {
            ProgressValue = Mathf.Abs(1f - CalculateMoney() / deskPrice) ;

            if (PlayerPrefs.GetInt("dollar") >= deskPrice)
            {
                PlayerPrefs.SetInt("dollar", PlayerPrefs.GetInt("dollar") - deskPrice);

                deskRemainPrice = 0;
            }
            else
            {
                deskRemainPrice -= PlayerPrefs.GetInt("dollar");
                PlayerPrefs.SetInt("dollar", 0);
            }

            progressBar.fillAmount = ProgressValue;

            //PlayerController.PlayerManagerInstance.MoneyCounter.text = PlayerPrefs.GetInt("dollar").ToString("C0");
            GameManager.instance.SetMoneyUI();
            dollarAmount.text = deskRemainPrice.ToString("C0");

            if (deskRemainPrice <= 0)
            {
                Vector3 currentRotation = exampleObject.transform.rotation.eulerAngles;

                GameObject desk = Instantiate(newDesk, new Vector3(transform.position.x, -1.068235f - 0.3f, transform.position.z)
                    , Quaternion.Euler(currentRotation.x, currentRotation.y + rotation, currentRotation.z));
                desk.transform.parent = theRestaurant.transform;
                desk.transform.DOScale(1.1f, 1f).SetEase(Ease.OutElastic);
                desk.transform.DOScale(1f, 1f).SetDelay(1.1f).SetEase(Ease.OutElastic);
                
                unlockProgressObj.SetActive(false);
                
                buildNavMesh.BuildNavMesh();
                GameManager.instance.EnableSpawn("Spawn2");
            }

        }
    }

    private float CalculateMoney()
    {
        return deskRemainPrice - PlayerPrefs.GetInt("dollar");
    }
}
