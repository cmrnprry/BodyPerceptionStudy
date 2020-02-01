using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // TODO AFTER CAM FINISHES UI TASK
    // Do not want multiple canvases. 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
