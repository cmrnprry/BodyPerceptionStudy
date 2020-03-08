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

    public string type;

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

        if(type == "fridge")
        {
            StatsManager.Instance.addFood(foodName.ToString(), StatsManager.orderType.FRIDGE, Int32.Parse(calories));
            meal.AfterChooseMeal(meal.parent, calories);
        }

        else if (type == "app")
        {
            StatsManager.Instance.addFood(foodName.ToString(), StatsManager.orderType.APP, Int32.Parse(calories));
            meal.AfterChooseMeal(meal.orderParent, calories);
        }

        else if (type == "pantry")
        {
            StatsManager.Instance.addFood(foodName.ToString(), StatsManager.orderType.PANTRY, Int32.Parse(calories));
            meal.AfterChooseMeal(meal.pantryParent, calories);
        }
        Debug.Log("Number of calories: " + calories);
        
    }
}
