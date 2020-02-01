using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
