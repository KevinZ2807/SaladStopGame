
using UnityEngine;

public class Items : MonoBehaviour
{
    public string itemType = "GenericItem";  // Use different strings for different item types

    public void PickUp()
    {
        Debug.Log(itemType + " picked up!");
    }
}
