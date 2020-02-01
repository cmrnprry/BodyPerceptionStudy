using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
public class TextManager : MonoBehaviour
{

    public GameObject textBox;
    public GameObject answers;
    public Books b;
    public TextMeshProUGUI t;

    public GameObject player;
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        t.text = "";
    }


    public void StartDialogue(Text text)
    {
        Debug.Log("start text: " + text.sentences.Length);
        player.GetComponent<FirstPersonController>().enabled = false;

        foreach (string sentence in text.sentences)
        {
            sentences.Enqueue(sentence);
        }

        
        StartCoroutine(DisplayNextSentence());
        Debug.Log("end text");
    }

    public IEnumerator DisplayNextSentence()
    {
        Debug.Log("Display Next Sentence");

        textBox.SetActive(true);

        // if there are no more sentences in the queue
        if (sentences.Count == 0)
        {
            StartCoroutine(EndDialogue());
            yield break;
        }

        //the next sentence to be displayed (removed from queue)
        string sentence = sentences.Dequeue();
        yield return new WaitForEndOfFrame();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator WaitUntil(KeyCode code)
    {
        Debug.Log("wait");
        while (!Input.GetKeyDown(code))
            yield return null;

        Debug.Log("wait end");
        //yield return new WaitForFixedUpdate();
    }

    IEnumerator TypeSentence(string sentence)
    {
        string[] words = sentence.Split(' ');  // splits the sentences by word
        t.text = "";
        // displays each word one by one
        foreach (string word in words)
        {
            t.text += word;
            yield return new WaitForEndOfFrame(); //waits a single frame
        }

        yield return StartCoroutine(WaitUntil(KeyCode.Return));
        StartCoroutine(DisplayNextSentence());
    }

    IEnumerator EndDialogue()
    {
        //Allows answers to be displayed
        t.text = "";
        textBox.SetActive(false);
        answers.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        yield return StartCoroutine(WaitUntil(KeyCode.Mouse0));

        answers.SetActive(false);
        StartCoroutine(b.FindBook());
        player.GetComponent<FirstPersonController>().enabled = true;
    }
}
