using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGameplayManager : MonoBehaviour
{
    public static ButtonGameplayManager Ins;

    private void Awake()
    {
        if (Ins)
        {
            Destroy(Ins);
        }
        Ins = this;
        DontDestroyOnLoad(Ins);
    }

    
}
