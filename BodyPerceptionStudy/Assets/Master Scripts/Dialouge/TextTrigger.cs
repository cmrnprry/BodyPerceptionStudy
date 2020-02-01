using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] public Text text;
    public Books b;

    public void TriggerDialogue()
    {

        Debug.Log("Trigger Text");
        StopCoroutine(b.FindBook());
        FindObjectOfType<TextManager>().StartDialogue(text);
    }
}
