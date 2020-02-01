using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] public Text text;
    public GameManager gm;

    public void TriggerDialogue()
    {

        Debug.Log("Trigger Text");
        StopCoroutine(gm.CheckForInput());
        FindObjectOfType<TextManager>().StartDialogue(text);
    }
}
