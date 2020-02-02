using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{

    public GameManager gm;

    public void Agree()
    {
        StatsManager.Instance.addBook("Name", StatsManager.questionResult.AGREE);
        print("Agree");
    }

    public void Disagree()
    {
        StatsManager.Instance.addBook("Name", StatsManager.questionResult.DISAGREE);
        print("Disagree");

    }

    public void Indifferent()
    {
        StatsManager.Instance.addBook("Name", StatsManager.questionResult.INDIFFERENT);
        print("Indifferent");

    }

    public void ChooseFood()
    {
        var text = GetComponentInChildren<TextMeshProUGUI>().text;
        string[] words = text.Split(' ');
        var foodName = text[0];
        var calories = text[2];
        
        StatsManager.Instance.addFood(foodName.ToString(), StatsManager.orderType.FRIDGE, calories);
    }

    void OnApplicationQuit()
    {
        StatsManager.Instance.saveToCSV("runResults");
    }

    //Exits the Phone App
    public void ExitPhone()
    {
        PhoneHome();
        gm.phone.SetActive(false);
        gm.player.enabled = true;
    }

    //Access the exercise screen for the Treadmill
    public void Treadmill()
    {
        gm.exerciseScreen.SetActive(false);
        gm.treadmillScreen.SetActive(true);
    }

    //Access the exercise screen for the Weights
    public void Weights()
    {
        gm.exerciseScreen.SetActive(false);
        gm.weightsScreen.SetActive(true);
    }

    public void PhoneHome()
    {
        gm.orderFoodScreen.SetActive(false);
        gm.resultsScreen.SetActive(false);
        gm.treadmillScreen.SetActive(false);
        gm.weightsScreen.SetActive(false);
        gm.exerciseScreen.SetActive(false);
    }

    public void turnOffOrder()
    {
        gm.orderFoodScreen.SetActive(false);
    }

    //Shows the exercise menu 
    public void ExerciseMainMenu()
    {
        gm.resultsScreen.SetActive(false);
        gm.treadmillScreen.SetActive(false);
        gm.weightsScreen.SetActive(false);
        gm.exerciseScreen.SetActive(true);
    }

    public void OrderMainMenu()
    {
        StartCoroutine(gm.OrderMeal());
        gm.resultsScreen.SetActive(false);
        gm.treadmillScreen.SetActive(false);
        gm.weightsScreen.SetActive(false);
        gm.orderFoodScreen.SetActive(true);
    }

    //Treadmill Exercise
    public void TreadmillExercise()
    {
        int v = gm.treadmillDropdown.value;
        float hours = 0f;

        //What shows up in the dropdown
        if (v == 0)
        {
            hours = 0.5f;
        }
        else if (v == 1)
        {
            hours = 1.0f;
        }
        else if (v == 2)
        {
            hours = 1.5f;
        }
        else if (v == 3)
        {
            hours = 2.0f;
        }

        int cals = (int)(hours * gm.treadmillCaloriesPerHour);
        int trueCals = cals * -1;
        gm.results.text = "You burned " + cals + " calories.";

        //sends data to the stats manager
        StatsManager.Instance.addExercise("treadmill", trueCals);
        gm.confirm = true;

        //shows the results screen
        ShowResults();
        Debug.Log(gm.results.text);
    }

    //Weights Exercise
    public void WeightsExercise()
    {
        int v = gm.weightsDropdown.value;
        float hours = 0f;

        //What shows up in the dropdown
        if (v == 0)
        {
            hours = 0.5f;
        }
        else if (v == 1)
        {
            hours = 1.0f;
        }
        else if (v == 2)
        {
            hours = 1.5f;
        }
        else if (v == 3)
        {
            hours = 2.0f;
        }

        int cals = (int)(hours * gm.weightsCaloriesPerHour);
        int trueCals = cals * -1;
        gm.results.text = "You burned " + cals + " calories.";

        //sends data to the stats manager
        StatsManager.Instance.addExercise("weights", trueCals);

        //shows the results screen
        ShowResults();
        Debug.Log(gm.results.text);
    }

    //Shoes the Results
    void ShowResults()
    {
        gm.treadmillScreen.SetActive(false);
        gm.weightsScreen.SetActive(false);
        gm.resultsScreen.SetActive(true);
    }
}
