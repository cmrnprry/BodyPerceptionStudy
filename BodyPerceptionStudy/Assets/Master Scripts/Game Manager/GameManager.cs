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

        if (Input.GetKey(KeyCode.C))
        {
            //The player has checked the stats manager
            StatsManager.Instance.checkedCals();

            //Sets the calories to be displayed
            var curCal = "Calories: " + StatsManager.Instance.getCurCalories();
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

}
