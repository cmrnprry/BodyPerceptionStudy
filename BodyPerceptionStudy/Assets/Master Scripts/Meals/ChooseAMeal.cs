using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

[System.Serializable]
public class FoodItem
{
    public string name;
    public int calories;
}



public class ChooseAMeal : MonoBehaviour
{
    // List of foods serialized in the Inspector
    public List<FoodItem> fridgeFoods;

    // List of food to order from
    public List<FoodItem> orderableFoods;
    public GameObject button;

    //The parents for the refridgerator food
    public GameObject parent;

    // The parent object for the Order Foods
    public GameObject orderParent; 
    public GameManager gm;

    // Do not want multiple canvases. 

    //Populates UI elements with the food options and their calories
    public void PopulateList(List<FoodItem> foods, GameObject parent)
    {
        foreach (var food in foods)
        {
            var choice = Instantiate(button);

            var x = choice.GetComponent<Button>();
            x.GetComponentInChildren<TextMeshProUGUI>().text = food.name + " - " + food.calories;
            
            choice.transform.parent = parent.transform;
        }
    }

    public void BeforeChooseMeal(GameObject parent)
    {
        parent.SetActive(true);
        gm.player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void AfterChooseMeal(GameObject parent)
    {
        if (parent.name == "Fridge Holder")
            gm.player.GetComponent<FirstPersonController>().enabled = true;
        DePopulateList(parent);
        parent.SetActive(false);
        gm.orderFoodScreen.SetActive(false);
    }

    public void DePopulateList(GameObject parent)
    {
        Button[] buttons = parent.GetComponentsInChildren<Button>(true);
        Debug.Log(buttons.Length);
        foreach (Button b in buttons)
        {
            Destroy(b.gameObject);
            Debug.Log("Destroy: " + b.name);
        }
    }


}
