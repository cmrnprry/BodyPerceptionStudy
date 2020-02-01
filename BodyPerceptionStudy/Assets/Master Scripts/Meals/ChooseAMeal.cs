﻿using System.Collections;
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
    public TextMeshProUGUI pressE;
    public GameObject player;

    // Do not want multiple canvases. 
    void Start()
    {
        PopulateList();

        StartCoroutine(FindFridge());
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

    public IEnumerator FindFridge()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Fridge" || hit.transform.tag == "Book")
            {
                Debug.Log("Press E to open fridge");
                pressE.text = "Press E to open fridge";
                pressE.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("The E Key was pressed");
                    

                    // Tell stats the fridge has been opened.
                    StatsManager.Instance.openedFridge();

                    //display text of that object
                    StartCoroutine(ChooseMeal());
                }
            }
            else
            {
                pressE.gameObject.SetActive(false);
                
            }

        }


        yield return new WaitForSeconds(.01f);
        StartCoroutine(FindFridge());
    }

    IEnumerator WaitUntil(KeyCode code)
    {
        Debug.Log("wait");
        while (!Input.GetKeyDown(code))
            yield return null;

        Debug.Log("wait end");
    }

    IEnumerator ChooseMeal()
    {
        BeforeChooseMeal();
        yield return StartCoroutine(WaitUntil(KeyCode.Mouse0));
        AfterChooseMeal();
    }

    void BeforeChooseMeal()
    {
        parent.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void AfterChooseMeal()
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        parent.SetActive(false);
    }


}
