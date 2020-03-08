using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    public GameObject playerObject;
    public GameObject phone;
    public GameObject exerciseScreen;
    public GameObject treadmillScreen;
    public GameObject weightsScreen;
    public GameObject resultsScreen;
    public GameObject foodResultsScreen;
    public TextMeshProUGUI foodResultsText;
    public GameObject orderFoodScreen;
    public GameObject gameScreen;
    public GameObject UI;
    public TMPro.TMP_Dropdown treadmillDropdown;
    public TMPro.TMP_Dropdown weightsDropdown;
    public TMPro.TextMeshProUGUI results;
    public int treadmillCaloriesPerHour;
    public int weightsCaloriesPerHour;
    public int numPlayMinutes;
    private float numSecondsLeft;
    private bool gameDone = false;

    public TextMeshProUGUI pressE;

    private GameObject book;

    private GameObject poster;

    public ChooseAMeal meal;

    public TextMeshProUGUI PublicCalories;
    //public int distance;

    public bool confirm = false;

    public GameObject si;
    [SerializeField] private float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.enabled = false;
        phone.SetActive(false);
        StartCoroutine(CheckForInput());
        numSecondsLeft = (float)(numPlayMinutes * 60);
    }

    void Update() {
        if(numSecondsLeft <= 0.0f && !gameDone) {
            gameDone = true;
            shutDown();
        }
        else
		{
            numSecondsLeft -= Time.deltaTime;
		}
    }

    public void shutDown()
	{
        string timestamp = System.DateTime.Today.ToShortDateString().Replace("/", "-");
        timestamp += " " + System.DateTime.Now.ToShortTimeString().Replace(":", "-");
        StatsManager.Instance.saveToCSV(timestamp);
        SceneManager.LoadScene("EndScene");
    }

    //checks for input
    public IEnumerator CheckForInput()
    {
        //Checks to see if the player wants to look at their calories
        PublicCalories.gameObject.SetActive(false);

        // To display the number of calories
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

        // To display the phone
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StatsManager.Instance.checkedPhone();
            phone.SetActive(true);
            
            player.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Debug.Log("phone");
        }


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxDistance))
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

                    //Stops courotine
                    StopCoroutine(CheckForInput());

                    //display text of that object
                    book = hit.transform.gameObject;
                    book.gameObject.GetComponent<TextTrigger>().TriggerDialogue();
                }
            }
            else if(hit.transform.tag == "Poster")
            {
                pressE.text = "Press E to read poster";
                pressE.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("The E Key was pressed: Posters");

                    //Stops courotine
                    StopCoroutine(CheckForInput());

                    //display text of that object
                    poster = hit.transform.gameObject;
                    poster.gameObject.GetComponent<TextTrigger>().TriggerDialogue();


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
            //Check for if the player is looking at the treadmill
            else if (hit.transform.tag == "Tread")
            {
                Debug.Log("Press E to read book");
                pressE.text = "Press E to run on the treadmill";
                pressE.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("The E Key was pressed: Treadmil");

                    // Tell stats the fridge has been opened.
                    //StatsManager.Instance.addExercise(string exerciseName, int calories)

                    //display text of that object
                    confirm = false;
                    StartCoroutine(ExersciseTreadMill());
                }
            }
            //Check for if the player is looking at the radio
            else if (hit.transform.tag == "Radio")
            {
                // Debug.Log("Press E to turn the radio on or off");
                pressE.text = "Press E to turn the radio on or off";
                pressE.text += "\nLeft/Right mouse to change stations";
                pressE.gameObject.SetActive(true);

                Radio_Script radio = hit.transform.gameObject.GetComponent<Radio_Script>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("The E Key was pressed: Radio");
                    radio.toggleRadio();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Change Station 1");
                    radio.changeStation(1);
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Change Station 2");
                    radio.changeStation(-1);
                }
            }


            else
            {
                pressE.gameObject.SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.I))
        {
            gameDone = true;
            shutDown();
        }

        yield return new WaitForSeconds(.01f);
        StartCoroutine(CheckForInput());
    }

    IEnumerator ChooseMeal()
    {
        StopCoroutine(CheckForInput());

        meal.PopulateList(meal.fridgeFoods, meal.parent);
        meal.BeforeChooseMeal(meal.parent);
        yield return new WaitForEndOfFrame();
    }

    public IEnumerator OrderMeal()
    {
        Debug.Log(meal.orderParent.name);
        meal.PopulateList(meal.orderableFoods, meal.orderParent);
        meal.BeforeChooseMeal(meal.orderParent);
        yield return new WaitForEndOfFrame();
    }

    //When interacting with the treadmill
    IEnumerator ExersciseTreadMill()
    {
        StopCoroutine(CheckForInput());
        phone.SetActive(true);
        treadmillScreen.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        yield return StartCoroutine(WaitUntilConfirm());

        player.GetComponent<FirstPersonController>().enabled = true;
        treadmillScreen.SetActive(false);
        resultsScreen.SetActive(false);
        phone.SetActive(false);
        
    }

    //waits until the player hits confirm
    public IEnumerator WaitUntilConfirm()
    {
        Debug.Log("wait");
        while (!confirm)
            yield return null;

        Debug.Log("wait end");
    }

    //waits until the player hits a specified key
    public IEnumerator WaitUntil(KeyCode code)
    {
        Debug.Log("wait");
        while (!Input.GetKeyDown(code))
            yield return null;

        Debug.Log("wait end");
    }

    public void StartSpaceInvaders()
    {
        TeleportPlayer(1);
        UI.SetActive(false);
        StatsManager.Instance.checkedGame();
        si.SetActive(true);
        si.GetComponentInChildren<SIManager>().StartGame();
    }

    //teleports the player to play phone games
    //0 is back to start
    //1 is space invaders
    public void TeleportPlayer(int game)
    {
        playerObject.transform.rotation = Quaternion.identity;
        playerObject.transform.GetChild(0).gameObject.transform.rotation = Quaternion.identity;
        playerObject.GetComponentInChildren<Transform>().rotation = Quaternion.identity;
        switch (game)
        {
            case 0:
                player.transform.position = new Vector3(0, 4.84f, -12.39f);
                UI.SetActive(true);
                break;
            case 1:
                player.transform.position = new Vector3(-87.8f, 4.4f, -11.8f);
                break;
        }
    }

}
