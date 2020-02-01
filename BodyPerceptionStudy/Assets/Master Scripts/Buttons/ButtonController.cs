using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{

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
}
