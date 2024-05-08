using DG.Tweening;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnlockBurger : MonoBehaviour
{
    [SerializeField] private GameObject theRestaurant;
    [SerializeField] private GameObject exampleObject;
    [SerializeField] private GameObject unlockProgressObj;
    [SerializeField] private GameObject newDesk;
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI dollarAmount;
    [SerializeField] private int deskPrice;
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
                GameObject desk = Instantiate(newDesk, new Vector3(exampleObject.transform.position.x, 0f - 0.3f, exampleObject.transform.position.z)
                    , exampleObject.transform.rotation);
                desk.transform.parent = theRestaurant.transform;
                desk.transform.DOScale(0.6f, 0.5f).SetEase(Ease.OutElastic);
                desk.transform.DOScale(0.5f, 0.5f).SetDelay(0.6f).SetEase(Ease.OutElastic);
                
                unlockProgressObj.SetActive(false);
                
                buildNavMesh.BuildNavMesh();
            }

        }
    }

    private float CalculateMoney()
    {
        return deskRemainPrice - PlayerPrefs.GetInt("dollar");
    }
}
