using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] public Text text;

    public void TriggerDialogue()
    {

        Debug.Log("Trigger Text");
        FindObjectOfType<TextManager>().StartDialogue(text);
    }
}
