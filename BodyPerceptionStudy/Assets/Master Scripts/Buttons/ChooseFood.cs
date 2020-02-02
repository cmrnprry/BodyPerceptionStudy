using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseFood : MonoBehaviour
{
    private Button b;
    private ChooseAMeal meal;

    void Awake()
    {
        Debug.Log("start");
        meal = FindObjectOfType<ChooseAMeal>();
        b = this.GetComponent<Button>();
        b.onClick.AddListener(AddFood);
        b.interactable = true;
        Debug.Log(meal.gameObject.name);
    }

    public void AddFood()
    {
        Debug.Log("Add Food");
        var text = GetComponentInChildren<TextMeshProUGUI>().text;
        string[] words = text.Split(' ');
        var foodName = words[0];
        var calories = words[2];

        Debug.Log("Number of calories: " + calories);
        StatsManager.Instance.addFood(foodName.ToString(), StatsManager.orderType.FRIDGE, Int32.Parse(calories));
        meal.AfterChooseMeal(meal.parent);
    }
}
