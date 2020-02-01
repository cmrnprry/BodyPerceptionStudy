using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        phone.SetActive(false);
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

    void OnApplicationQuit()
    {
        StatsManager.Instance.saveToCSV("runResults");
    }

    public void ExitPhone()
    {
        ExerciseMainMenu();
        phone.SetActive(false);
        player.enabled = true;
    }

    public void Treadmill()
    {
        exerciseScreen.SetActive(false);
        treadmillScreen.SetActive(true);
    }

    public void Weights()
    {
        exerciseScreen.SetActive(false);
        weightsScreen.SetActive(true);
    }

    public void ExerciseMainMenu()
    {
        resultsScreen.SetActive(false);
        treadmillScreen.SetActive(false);
        weightsScreen.SetActive(false);
        exerciseScreen.SetActive(true);
    }

    public void TreadmillExercise()
    {
        int v = treadmillDropdown.value;
        float hours = 0f;
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
        StatsManager.Instance.addExercise("treadmill", trueCals);
        ShowResults();
        Debug.Log(results.text);
    }

    public void WeightsExercise()
    {
        int v = weightsDropdown.value;
        float hours = 0f;
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
        StatsManager.Instance.addExercise("weights", trueCals);
        ShowResults();
        Debug.Log(results.text);
    }

    void ShowResults()
    {
        treadmillScreen.SetActive(false);
        weightsScreen.SetActive(false);
        resultsScreen.SetActive(true);
    }
}
