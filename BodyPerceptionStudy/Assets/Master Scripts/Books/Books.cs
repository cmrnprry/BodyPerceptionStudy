using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Books : MonoBehaviour
{
    public TextMeshProUGUI pressE;

    private GameObject book;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindBook());

        Debug.Log("Coroutine started");
    }

    public IEnumerator FindBook()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Book")
            {
                Debug.Log("Press E to read book");
                pressE.text = "Press E to read book";
                pressE.gameObject.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("The E Key was pressed");
                    
                    //display text of that object
                    book = hit.transform.gameObject;
                    
                    book.gameObject.GetComponent<TextTrigger>().TriggerDialogue();
                }
            }
            else 
            {
                pressE.gameObject.SetActive(false);
            }
                
        }


        yield return new WaitForSeconds(.01f);
        StartCoroutine(FindBook());
    }
}
