using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class ButtonController : MonoBehaviour
{

    public GameManager gm;
    [SerializeField] private GameObject t;

    public void ClosePlanel()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        gm.player.enabled = true;
        t.SetActive(false);
    }

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

    public void StartGame()
    {
        SceneManager.LoadScene("Master Scene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ReturntoMain()
    {
        SceneManager.LoadScene("Start");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //screen that holds the games
    public void GoToGameScreen()
    {
        gm.gameScreen.SetActive(true);
    }

    public void SpaceInvaders()
    {
        gm.TeleportPlayer(1);
        gm.UI.SetActive(false);
        gm.StartSpaceInvaders();
    }

    //Exits the Phone App
    public void ExitPhone()
    {
        gm.phone.SetActive(false);
        gm.orderFoodScreen.SetActive(false);
        gm.gameScreen.SetActive(false);
        gm.resultsScreen.SetActive(false);
        gm.treadmillScreen.SetActive(false);
        gm.weightsScreen.SetActive(false);
        gm.exerciseScreen.SetActive(false);
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gm.orderFoodScreen.SetActive(false);
        gm.resultsScreen.SetActive(false);
        gm.treadmillScreen.SetActive(false);
        gm.weightsScreen.SetActive(false);
        gm.exerciseScreen.SetActive(false);
        gm.player.enabled = false;
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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

    public void ShowFoodResults()
    {
        if (gm.orderFoodScreen.activeSelf == false)
        {
            gm.phone.SetActive(false);
            gm.player.GetComponent<FirstPersonController>().enabled = true;
        }

        gm.foodResultsScreen.SetActive(false);
        gm.orderFoodScreen.SetActive(false);
    }
}
