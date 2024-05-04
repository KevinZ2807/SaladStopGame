using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuffs : MonoBehaviour
{
    private bool shouldDestroy = true;

    void Start()
    {
        StartCoroutine(DestroyWithCheck(30f));
    }

    IEnumerator DestroyWithCheck(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
    }

    public void CancelDestruction()
    {
        shouldDestroy = false;
    }
}
