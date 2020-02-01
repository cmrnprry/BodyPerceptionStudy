using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalorieCount : MonoBehaviour
{


    public int curCalories;

    private void Start()
    {
        curCalories = 1200;
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

        if (Input.GetKeyDown(KeyCode.E))
        {

            //show te calories
        }

        yield return new WaitForSeconds(3f);

        StartCoroutine(checkKeys());

    }
}
