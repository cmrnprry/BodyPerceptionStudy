using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public class FoodItem
{
    public string name;
    public int calories;
}

public class ChooseAMeal : MonoBehaviour
{
    // List of foods serialized in the Inspector
    public List<FoodItem> foods;
    public GameObject button;
    public GameObject parent;

    // TODO AFTER CAM FINISHES UI TASK
    // Do not want multiple canvases. 
    void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        foreach (var food in foods)
        {
            var choice = Instantiate(button);

            var x = choice.GetComponent<Button>();
            x.GetComponentInChildren<TextMeshProUGUI>().text = food.name + " - " + food.calories;
            
            choice.transform.parent = parent.transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
