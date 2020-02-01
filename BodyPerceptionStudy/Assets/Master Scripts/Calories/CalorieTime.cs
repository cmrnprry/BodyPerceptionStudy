using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalorieTime : MonoBehaviour
{


    public int curCalories;

    public TextMeshProUGUI PublicCalories;

    private void Start()
    {
        curCalories = 1200;
        StartCoroutine(checkKeys());
    }

    public void burnCalories(int calToBurn)
    {

        curCalories -= calToBurn;
    }

    public void gainCalories(int calToGain)
    {
        curCalories += calToGain;

    }

    public IEnumerator checkKeys()
    {
        Debug.Log("POOOP");
        PublicCalories.text = "Calories " + curCalories;
        PublicCalories.gameObject.SetActive(false);

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("BLOOP");
            //show te calories
            PublicCalories.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(.01f);

        StartCoroutine(checkKeys());

    }
}
