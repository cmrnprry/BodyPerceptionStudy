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
    public List<FoodItem> pantryFoods;
    // List of food to order from
    public List<FoodItem> orderableFoods;
    public GameObject button;

    //The parents for the refridgerator food
    public GameObject parent;
    public GameObject pantryParent;

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
            if (parent.name == "Fridge Holder")
                choice.GetComponent<ChooseFood>().type = "fridge";
            if (parent.name == "OrderHolder")
                choice.GetComponent<ChooseFood>().type = "app";
            if (parent.name == "Pantry Holder")
                choice.GetComponent<ChooseFood>().type = "pantry";

            Debug.Log(choice.GetComponent<ChooseFood>().type);

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

    public void AfterChooseMeal(GameObject parent, string cal)
    {
        Debug.Log("after: " + parent.name);
        if (parent.name == "Fridge Holder" || parent.name == "Pantry Holder")
        {
            gm.phone.SetActive(true);
            gm.orderFoodScreen.SetActive(false);
        }


        DePopulateList(parent);
        parent.SetActive(false);

        gm.foodResultsText.text = "You ate " + cal + " calories.";
        gm.foodResultsScreen.SetActive(true);

    }

    public void DePopulateList(GameObject parent)
    {
        Debug.Log("depop: " + parent.name);

        parent.SetActive(false);
        Button[] buttons = parent.GetComponentsInChildren<Button>(true);
        Debug.Log(buttons.Length);
        foreach (Button b in buttons)
        {
            Destroy(b.gameObject);
            Debug.Log("Destroy: " + b.name);
        }
    }


}
