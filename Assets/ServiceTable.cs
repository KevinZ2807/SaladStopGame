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
    private float cooldown = 0f;
    private bool isWorking = false;

    private void Start()
    {
        makeMoneyIE = MakeMoney();
    }

    public void Work()
    {
        //female_anim.SetBool("work",true);
        
        InvokeRepeating("DOSubmitPapers",3f,1f);
        
        StartCoroutine(makeMoneyIE);
    }

    void Update() {
        cooldown -= Time.deltaTime;
        if (transform.childCount != 0 && cooldown <= 0 && !isWorking) {
            Work();
            Debug.Log("Update work()");
            cooldown = 1f;
        }
    }

    private IEnumerator MakeMoney()
    {
        Debug.Log("Making money");
        var counter = 0;
        var MoneyPlaceIndex = 0;
        isWorking = true;
        yield return new WaitForSecondsRealtime(1f);

        while (counter < transform.childCount)
        {
            if (transform.childCount > 0) {
                Destroy(transform.GetChild(transform.childCount - 1).gameObject,1f);
                Debug.Log("Enter while loop");
                var moneyCoordinate = moneyPlace.GetChild(MoneyPlaceIndex);
                GameObject NewDollar = Instantiate(money, new Vector3(moneyCoordinate.position.x,
                        moneyCoordinate.position.y + YAxis, moneyCoordinate.position.z),
                    moneyPlace.GetChild(MoneyPlaceIndex).rotation);

                NewDollar.transform.DOScale(new Vector3(0.4f, 0.4f, 0.6f), 0.5f).SetEase(Ease.OutElastic);

                if (MoneyPlaceIndex < moneyPlace.childCount - 1)
                {
                    MoneyPlaceIndex++;
                }
                else
                {
                    MoneyPlaceIndex = 0;
                    YAxis += 0.05f;
                }
                yield return new WaitForSecondsRealtime(2f);
            }
        }
        
        isWorking = false;
        yield break;
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