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
    public List<FoodItem> foods;
    public GameObject button;
    public GameObject parent;
    public GameManager gm;

    // Do not want multiple canvases. 
    void Start()
    {
        PopulateList();
    }

    //Populates UI elements with the food options and their calories
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

    public void BeforeChooseMeal()
    {
        parent.SetActive(true);
        gm.player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void AfterChooseMeal()
    {
        gm.player.GetComponent<FirstPersonController>().enabled = true;
        parent.SetActive(false);
    }


}
