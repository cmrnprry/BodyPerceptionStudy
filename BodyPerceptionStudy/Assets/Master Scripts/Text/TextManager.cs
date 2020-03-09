using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{

    public GameObject textBox;
    public GameObject answers;
    public GameManager gm;
    public TextMeshProUGUI t;
    private Queue<string> sentences;
    public static string readableName;

    public GameObject x;

    void Start()
    {
        sentences = new Queue<string>();
        t.text = "";
    }

    public void setCurrentName(string name)
    {
        readableName = name;
    }

    public void StartDialogue(Text text)
    {
        Debug.Log("start text: " + text.sentences.Length);
        gm.player.GetComponent<FirstPersonController>().enabled = false;

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
        Debug.Log("Sentences: " + sentences.Count);

        textBox.SetActive(true);

        // if there are no more sentences in the queue
        if (sentences.Count == 0)
        {
            StartCoroutine(EndDialogue());
            yield break; 
        }
        if (sentences.Count == 43)
        {
            Debug.Log("hello");
            StartCoroutine(EndDialogueFake());
            yield break;
        }

        //the next sentence to be displayed (removed from queue)
        string sentence = sentences.Dequeue();
        yield return new WaitForEndOfFrame();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        string[] words = sentence.Split(' ');  // splits the sentences by word
        t.text = "";
        // displays each word one by one
        foreach (string word in words)
        {
            t.text += word + " ";
            yield return new WaitForEndOfFrame(); //waits a single frame
        }

        yield return StartCoroutine(gm.WaitUntil(KeyCode.Return));
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
        yield return StartCoroutine(gm.WaitUntil(KeyCode.Mouse0));
        yield return new WaitForSeconds(.25f);
        answers.SetActive(false);
        StartCoroutine(gm.CheckForInput());
        gm.player.GetComponent<FirstPersonController>().enabled = true;
    }

    IEnumerator EndDialogueFake()
    {
        //Allows answers to be displayed
        t.text = "";
        textBox.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(gm.CheckForInput());
        gm.player.GetComponent<FirstPersonController>().enabled = false;
        x.SetActive(false);
        yield return null;
    }
}
