using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlate : MonoBehaviour
{
    public ServiceTable main;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC")) // Ensure your NPC GameObject has a tag "NPC"
        {
            if (!main.isWorking && main.isHavingFood)
            {
                main.isWorking = true;
                main.GenerateMoney();
                Debug.Log("NPC entered, starting work.");
                other.GetComponent<NPC_script>().ReceiveFood();
            } else {
                Debug.Log("There is no food");
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("NPC")) // Ensure your NPC GameObject has a tag "NPC"
        {
            main.isWorking = false;
        }
    }
}
