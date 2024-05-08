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
    private int MoneyPlaceIndex = 0;
    public bool isWorking = false;
    public bool isHavingFood = false;
    
    void Update() {
        if (transform.childCount > 0) {
            isHavingFood = true;
        } else {
            isHavingFood = false;
        }
    }

    public void GenerateMoney()
    {
            // Remove one food item when money is generated
            Destroy(transform.GetChild(transform.childCount - 1).gameObject, 1f);

            // Get the next position to place the money
            var moneyCoordinate = moneyPlace.GetChild(MoneyPlaceIndex);
            GameObject NewDollar = Instantiate(money, new Vector3(moneyCoordinate.position.x,
                moneyCoordinate.position.y + YAxis, moneyCoordinate.position.z),
                moneyCoordinate.rotation);

            // Animate the money object
            NewDollar.transform.DOScale(new Vector3(0.4f, 0.4f, 0.6f), 0.5f).SetEase(Ease.OutElastic);

            // Update the index for the next money position
            if (MoneyPlaceIndex < moneyPlace.childCount - 1)
            {
                MoneyPlaceIndex++;
            }
            else
            {
                MoneyPlaceIndex = 0;
                YAxis += 0.05f;  // Adjust Y-axis to stack money if starting over
            }
    }

}