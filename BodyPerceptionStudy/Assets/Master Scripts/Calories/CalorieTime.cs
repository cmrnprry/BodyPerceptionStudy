using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// Script that keeps track of calories. Have this attached
/// to a game object. 
/// </summary>
public class CalorieTime : MonoBehaviour
{

    /// <summary>
    /// The current calories that the person has.
    /// </summary>
    public int curCalories;

    /// <summary>
    /// The text that displays the calories
    /// </summary>
    public TextMeshProUGUI PublicCalories;


    /// <summary>
    /// Text to be displayed if calires get dangerously low
    /// </summary>
    public TextMeshProUGUI tooLowCalories;

    private void Start()
    {
        curCalories = 2000;
        StartCoroutine(checkKeys());
    }
    /// <summary>
    /// Subtracks calories based on how many
    /// the player has burned in one sitting.
    /// </summary>
    /// <param name="calToBurn">The amount of calories they have burned</param>
    public void burnCalories(int calToBurn)
    {

        curCalories -= calToBurn;
    }
    /// <summary>
    /// Adds calories based on what the player ate in one sitting.
    /// </summary>
    /// <param name="calToGain"> THe amount of calories they have eaten</param>
    public void gainCalories(int calToGain)
    {
        curCalories += calToGain;

    }

    /// <summary>
    /// A coroutine to check and display the calories. 
    /// This currently displays when "E" is clicked. 
    /// Otherwise, the calories are not displayed. 
    /// </summary>
    /// <returns></returns>
    public IEnumerator checkKeys()
    {
                PublicCalories.text = "Calories " + curCalories;
        PublicCalories.gameObject.SetActive(false);

        if (Input.GetKey(KeyCode.E))
        {
            PublicCalories.gameObject.SetActive(true);
        }

        if (curCalories <= 500)
        {

            tooLowCalories.gameObject.SetActive(true);
        }
        else { tooLowCalories.gameObject.SetActive(false); }

        yield return new WaitForSeconds(.01f);

        StartCoroutine(checkKeys());

    }
}
