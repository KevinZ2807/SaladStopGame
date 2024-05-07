using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ServiceTable : MonoBehaviour
{
    //public Animator female_anim;
    [SerializeField] private Transform FoodPlace;
    [SerializeField] private GameObject Food;
    private float YAxis;
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
        var counter = 0;
        var FoodPlaceIndex = 0;
        
        yield return new WaitForSecondsRealtime(2);

        while (counter < transform.childCount)
        {
            GameObject NewDollar = Instantiate(Food, new Vector3(FoodPlace.GetChild(FoodPlaceIndex).position.x,
                    YAxis, FoodPlace.GetChild(FoodPlaceIndex).position.z),
                FoodPlace.GetChild(FoodPlaceIndex).rotation);

            NewDollar.transform.DOScale(new Vector3(0.4f, 0.4f, 0.6f), 0.5f).SetEase(Ease.OutElastic);

            if (FoodPlaceIndex < FoodPlace.childCount - 1)
            {
                FoodPlaceIndex++;
            }
            else
            {
                FoodPlaceIndex = 0;
                YAxis += 0.5f;
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

            var Desk = transform.parent;

            Desk.GetChild(Desk.childCount - 1).GetComponent<Renderer>().enabled = true;
            
            StopCoroutine(makeMoneyIE);

            YAxis = 0f;
        }
    }
}