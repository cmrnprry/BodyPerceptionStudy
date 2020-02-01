using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    public GameObject phone;
    public GameObject exerciseScreen;
    public GameObject treadmillScreen;
    public GameObject weightsScreen;
    public GameObject resultsScreen;
    public TMPro.TMP_Dropdown treadmillDropdown;
    public TMPro.TMP_Dropdown weightsDropdown;
    public TMPro.TextMeshProUGUI results;
    public int treadmillCaloriesPerHour;
    public int weightsCaloriesPerHour;

    public TextMeshProUGUI pressE;

    private GameObject book;

    public ChooseAMeal meal;

    public TextMeshProUGUI PublicCalories;

    // Start is called before the first frame update
    void Start()
    {
        phone.SetActive(false);
        StartCoroutine(CheckForInput());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StatsManager.Instance.checkedPhone();
            phone.SetActive(true);
            player.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //checks for input
    public IEnumerator CheckForInput()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //Check for if the player is looking at readable books
            if (hit.transform.tag == "Book")
            {
                Debug.Log("Press E to read book");
                pressE.text = "Press E to read book";
                pressE.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("The E Key was pressed: Books");

                    //display text of that object
                    book = hit.transform.gameObject;
                    book.gameObject.GetComponent<TextTrigger>().TriggerDialogue();
                }
            }
            //Check to see if the player is looking at the fridge
            else if (hit.transform.tag == "Fridge")
            {
                Debug.Log("Press E to open fridge");
                pressE.text = "Press E to open fridge";
                pressE.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("The E Key was pressed: Fridge");


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

        //Checks to see if the player wants to look at their calories
        PublicCalories.gameObject.SetActive(false);

        if (Input.GetKey(KeyCode.E))
        {
            //The player has checked the stats manager
            StatsManager.Instance.checkedCals();

            //Sets the calories to be displayed
            var curCal = StatsManager.Instance.getCurCalories();
            PublicCalories.text = curCal.ToString();

            //Shows the calories
            PublicCalories.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(.01f);
        StartCoroutine(CheckForInput());
    }

    public IEnumerator WaitUntil(KeyCode code)
    {
        Debug.Log("wait");
        while (!Input.GetKeyDown(code))
            yield return null;

        Debug.Log("wait end");
    }

    IEnumerator ChooseMeal()
    {
        meal.BeforeChooseMeal();
        yield return StartCoroutine(WaitUntil(KeyCode.Mouse0));
        meal.AfterChooseMeal();
    }



////////////////////////////////////// BUTTON CONTROLLER THINGS ////////////////////////////////////////////////////////

    void OnApplicationQuit()
    {
        StatsManager.Instance.saveToCSV("runResults");
    }

    //Exits the Phone App
    public void ExitPhone()
    {
        ExerciseMainMenu();
        phone.SetActive(false);
        player.enabled = true;
    }

    //Access the exercise screen for the Treadmill
    public void Treadmill()
    {
        exerciseScreen.SetActive(false);
        treadmillScreen.SetActive(true);
    }

    //Access the exercise screen for the Weights
    public void Weights()
    {
        exerciseScreen.SetActive(false);
        weightsScreen.SetActive(true);
    }

    //Shows the exercise menu 
    public void ExerciseMainMenu()
    {
        resultsScreen.SetActive(false);
        treadmillScreen.SetActive(false);
        weightsScreen.SetActive(false);
        exerciseScreen.SetActive(true);
    }

    //Treadmill Exercise
    public void TreadmillExercise()
    {
        int v = treadmillDropdown.value;
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

        int cals = (int)(hours * treadmillCaloriesPerHour);
        int trueCals = cals * -1;
        results.text = "You burned " + cals + " calories.";

        //sends data to the stats manager
        StatsManager.Instance.addExercise("treadmill", trueCals);

        //shows the results screen
        ShowResults();
        Debug.Log(results.text);
    }

    //Weights Exercise
    public void WeightsExercise()
    {
        int v = weightsDropdown.value;
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

        int cals = (int)(hours * weightsCaloriesPerHour);
        int trueCals = cals * -1;
        results.text = "You burned " + cals + " calories.";

        //sends data to the stats manager
        StatsManager.Instance.addExercise("weights", trueCals);

        //shows the results screen
        ShowResults();
        Debug.Log(results.text);
    }

    //Shoes the Results
    void ShowResults()
    {
        treadmillScreen.SetActive(false);
        weightsScreen.SetActive(false);
        resultsScreen.SetActive(true);
    }
}
