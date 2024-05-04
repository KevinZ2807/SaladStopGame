using UnityEngine;
using System.Collections.Generic;  
public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    public int maxItems = 10;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Items item = other.gameObject.GetComponent<Items>();
            if (item != null)
            {
                if (!inventory.ContainsKey(item.itemType))
                {
                    inventory[item.itemType] = 0;  // Initialize count if not in dictionary
                }

                if (inventory[item.itemType] < maxItems)
                {
                    inventory[item.itemType]++;  // Increment the item count
                    item.PickUp();
                    Destroy(other.gameObject);  // Destroys the item GameObject after picking it up
                }
                else
                {
                    Debug.Log("Cannot pick up more of " + item.itemType);
                }
            }
        }
    }
}
