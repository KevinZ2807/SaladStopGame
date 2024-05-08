using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ServiceTable : MonoBehaviour
{
    //public Animator female_anim;
    [SerializeField] private Transform moneyPlace;
    [SerializeField] private GameObject money;
    private float YAxis = 0f;
    private IEnumerator makeMoneyIE;

    private void Start()
    {
        makeMoneyIE = MakeMoney();
    }

    public void Work()
    {
        //female_anim.SetBool("work",true);
        
        InvokeRepeating("DOSubmitPapers",2f,1f);

        StartCoroutine(makeMoneyIE);
    }

    private IEnumerator MakeMoney()
    {
        Debug.Log("Making money");
        var counter = 0;
        var MoneyPlaceIndex = 0;
        
        yield return new WaitForSecondsRealtime(2);

        while (counter < transform.childCount)
        {
            Debug.Log("Enter while loop");
            GameObject NewDollar = Instantiate(money, new Vector3(moneyPlace.GetChild(MoneyPlaceIndex).position.x,
                    YAxis, moneyPlace.GetChild(MoneyPlaceIndex).position.z),
                moneyPlace.GetChild(MoneyPlaceIndex).rotation);

            NewDollar.transform.DOScale(new Vector3(0.4f, 0.4f, 0.6f), 0.5f).SetEase(Ease.OutElastic);

            if (MoneyPlaceIndex < moneyPlace.childCount - 1)
            {
                MoneyPlaceIndex++;
            }
            else
            {
                MoneyPlaceIndex = 0;
                YAxis += 0.01f;
            }
            
            yield return new WaitForSecondsRealtime(3f);
        }
    }

    void DOSubmitPapers()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(transform.childCount - 1).gameObject,1f);
        }
        else
        {
            //female_anim.SetBool("work",false);
            Debug.Log("Stop making money");
            StopCoroutine(makeMoneyIE);

            YAxis = 0f;
        }
    }
}