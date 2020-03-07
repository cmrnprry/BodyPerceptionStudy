using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int health = 3;
    public int scoreVal = 0;

    private Rigidbody2D rb;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private List<Image> arr;
     

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

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
        if (health < 0)
        {
            //TODO: Game Over
            return;
        }

        arr[health - 1].gameObject.SetActive(false);
        health--;
    }

    public void KeepScore(int value)
    {
        scoreVal += value;
        scoreText.text = "Score: " + scoreVal;
    }
}
