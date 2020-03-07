using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int health = 3;
    public int scoreVal = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private List<Image> arr;
     

    void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;

        if (tag == "EB")
        {
            other.gameObject.SetActive(false);
            DecreaseHealth();
        }
    }

    public void DecreaseHealth()
    {
        if (health <= 0)
        {
            return;
        }

        arr[health - 1].gameObject.SetActive(false);
        health--;
    }

    public void Reset()
    {
        health = 3;
        arr[0].gameObject.SetActive(true);
        arr[1].gameObject.SetActive(true);
        arr[2].gameObject.SetActive(true);
        scoreText.text = "Score: 0";
    }

    public void KeepScore(int value)
    {
        scoreVal += value;
        scoreText.text = "Score: " + scoreVal;
    }
}
