using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CountText : MonoBehaviour
{  
    [SerializeField] private GameObject countObject;
    private TextMeshProUGUI inputText;
    private int count = 0;

    void Start() {
        inputText = GetComponent<TextMeshProUGUI>();
    }

    void Update() {

        CountChild();
    }

    private void CountChild() {
        count = countObject.transform.childCount;
        inputText.text = count.ToString();
    }
}
